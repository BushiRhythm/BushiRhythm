using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour {

	protected GameObject emitModel;

	public GameObject EmitModel
	{
		get{
			return emitModel;
		}
	}

	[SerializeField]
	protected bool IsRight;

	protected Transform firePoint;

	protected Transform weakPoint;

	bool IsInit;

	public Transform WeakPoint
	{
		get
		{
			return weakPoint;
		}
	
	}

	public Transform FirePoint
	{
		get
		{
			return firePoint;
		}
	}

	Animator _animator;

	protected Animator animator
	{
		get
		{
			if (!_animator)
				_animator = emitModel .GetComponent<Animator>();
			return _animator;
		}
	}

	StaticScript _staticScript;

	protected StaticScript staticScript
	{
		get
		{
			if (!_staticScript)
				_staticScript = GameObject .FindGameObjectWithTag( "StaticScript" ) .GetComponent<StaticScript>();
			return _staticScript;
		}
	}
	


	EnemyAttackAlg _enemyAttackAlg;

	EnemyAttackAlg enemyAttackAlg
	{
		get
		{
			if (!_enemyAttackAlg)
				_enemyAttackAlg = GetComponent<EnemyAttackAlg>();
			return _enemyAttackAlg;
		}
	}

	EnemySpawnAlg _enemySpawnAlg;

	protected EnemySpawnAlg enemySpawnAlg
	{
		get
		{
			if (!_enemySpawnAlg)
				_enemySpawnAlg = GetComponent<EnemySpawnAlg>();
			return _enemySpawnAlg;
		}
	}



	EnemySpeedControl _enemySpeedControl;

	protected EnemySpeedControl enemySpeedControl
	{
		get
		{
			if (!_enemySpeedControl)
				_enemySpeedControl = emitModel .GetComponent<EnemySpeedControl>();
			return _enemySpeedControl;
		}

	}

	protected int Step = 0;


	[SerializeField]
	int MaxHp = 1;	//初期時に設定するHP

	int CurHp;		//現在のHP
	int VirtualHp;	//仮想のHP(弾き返す弾専用)

	protected bool IsShocked;
	protected int ShockTime;

	protected bool DeathAction;

	public void Damage(int damage)
	{
		CurHp -= damage;
	}

	public void VirtualDamage( int damage )
	{
		VirtualHp -= damage;
	}


	public bool IsDeathAction
	{
		get
		{
			return DeathAction;
		}
	}

	public bool IsDeath
	{
		get
		{
			return CurHp <= 0;
		}
	}

	public bool IsVirtualDeath
	{
		get
		{
			return VirtualHp <= 0;
		}
	}

	[SerializeField]
	protected int StepPos;

	// Use this for initialization
 protected	void Start () {
		CurHp = MaxHp;
		VirtualHp = MaxHp;
		}
	
	// Update is called once per frame
	void Update () {
		if (!IsInit)
		{
			StepPos += staticScript .enemyAdjustStep .Step;
			IsInit = true;
		}
		transform .position = staticScript .frontMoverManager .GetPosFromStep( StepPos ) + staticScript .staticEnemyAlg .GetWidth( IsRight ) + staticScript .staticEnemyAlg .AdjustHeight;

		//驚いている間の処理
		if (ShockTime > 0)
		{
			if (staticScript .rhythmManager .DynamicRhythm .Tyming)
			{
				ShockTime--;
			}
			if(ShockTime == 0)
			{
				enemySpeedControl .Play();
			}
		}

		//敵がおどろいたら止まる
		if (staticScript .enemyStanManager .IsStan && !IsShocked)
		{
			if(Step ==2 || Step == 1)
			{
				Step = 1;
				IsShocked = true;
				staticScript .enemyAttackManager .RemoveFromEnemy( this );
				enemyAttackAlg .StopAttack();
				ShockTime = staticScript .enemyStanManager .MaxStan;
				enemySpeedControl .Stop();
			}
		}
		//タイミング時のみ行動分岐
		if (staticScript .rhythmManager .DynamicRhythm .Tyming)
		{
			if(!IsDeath)
			{
				int PlayerStep = staticScript .frontMoverManager .StepCount - 1 + (int)staticScript .frontMoverManager .GetProgress;
				int distance = StepPos - PlayerStep;
				if(ShockTime == 0)
				{
					bool IsChange = true;
					while (IsChange)
					{
						IsChange = false;
						switch(Step)
						{
							case 0:
							if (staticScript .staticEnemyAlg .IsEnemySpawn( distance ))
							{
								IsChange = true;
								int id;
								if(IsRight)
									id = 1;
								else
									id = 0;
								staticScript .enemyLines[id] .AddList( this );
								emitModel = enemySpawnAlg .EnemySpawn();
								emitModel .transform.parent = transform;
								staticScript .spawnParticle .Emit( transform .position );
								firePoint = emitModel .GetComponent<EnemyFirePoint>() .firePoint;
								weakPoint = emitModel .GetComponent<EnemyWeakPoint>() .weakPoint;
								Step++;
							}
							break;

							case 1:
							if (staticScript .staticEnemyAlg .IsCanAttack( distance ))
							{
								int id;

								if(IsRight)
									id = 1;
								else
									id = 0;

								Enemy TopEnemy = staticScript .enemyLines[id].GetActiveFrontEnemy();
								if(TopEnemy == this)
								{
									IsChange = true;
									enemyAttackAlg.StartAttack();
									Step++;
								}
							}
							break;

							case 2:
							if (staticScript .staticEnemyAlg .IsEnemyEscape( distance ) /* || enemyAttackAlg .loopTime >= 1*/ )
							{
								IsChange = true;
								staticScript .enemyAttackManager .RemoveFromEnemy( this );
								staticScript .spawnParticle .Emit( transform .position );
								Destroy( this .gameObject );
								return;
							}
							break;
						}

					}

				}

			}

		}
		if (IsDeath && !DeathAction)
		{
			DeathAction = true;
			staticScript .enemyAttackManager .RemoveFromEnemy( this );
			enemyAttackAlg .StopAttack();
			//staticScript .enemySEManager .ShooterDamageSE();
			staticScript .enemySEManager .EnemyVoices();
			DeathEnemy();
			DeathMotionStart();
		}

		if (DeathAction)
		{
			if (IsDeathAnimationEnd())
				Destroy( this .gameObject );
			return;
		}

	}

	virtual public void MarkerDestroyAction()
	{
	
	}

	virtual public void MarkerEmitAction()
	{
		staticScript .enemySEManager .SyurikenThrow();
	}

	protected void DeathEnemy()
	{
		staticScript.enemySEManager .Damage();
		staticScript .bulletResultManager .AddEnemyKill();
	}


	public void SingleAttackMotionStart()
	{
		animator .SetTrigger( "SingleAttack" );
	}

	public void DoubleAttackMotionStart()
	{
		animator .SetTrigger( "DoubleAttack" );
	}

	public void DeathMotionStart()
	{
		animator .SetInteger( "Random",Random.Range(0,int.MaxValue) % 2 );
		animator .SetTrigger( "deathflag" );
	}
	public bool IsWaitMotion()
	{
		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		return stateInfo.IsName("Wait");
	}
	public bool IsDeathAnimationEnd()
	{
		AnimatorStateInfo stateInfo = animator .GetCurrentAnimatorStateInfo( 0 );
		return stateInfo .IsName( "Delete" );
	}
}
