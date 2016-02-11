using UnityEngine;
using System.Collections;

public class SwordEnemy : Enemy {

	enum STATE
	{
		NO_RESPAWN = 0 ,
		RESPAWN,
		STEP,
		STOP,
		ATTACKREADY,
		ATTACKMOTION,
		JUMPATTACK,
	};


	bool Stepping;

	Vector3 JumpLen;

	Transform JumpEndPos;

	int JumpStartRhythm;

	int WaitStep;

	bool IsInit;

	[SerializeField]
	AnimationCurve JumpHeight;


	float SteppingStep = .0f;
	// Use this for initialization
	void Start () {
		base .Start();
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsInit)
		{
			StepPos += staticScript .enemyAdjustStep .Step;
			IsInit = true;
		}
		bool StepState = false;

		bool PreJumpAttackState = false;

		if ((STATE)Step == STATE.JUMPATTACK)
			StepState = true;

		if ((STATE)Step == STATE.ATTACKMOTION)
			PreJumpAttackState = true;
	
		
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
			if (Step == (int)STATE .STEP || Step == (int)STATE .RESPAWN)
			{
				Step = (int)STATE.RESPAWN;
				IsShocked = true;
				staticScript .enemyAttackManager .RemoveFromEnemy( this );

				ShockTime = staticScript .enemyStanManager .MaxStan;
				enemySpeedControl .Stop();
			}
		}
		//タイミング時のみ行動分岐
		if (staticScript .rhythmManager .DynamicRhythm .Tyming)
		{
			Stepping = false;
			if(!IsDeath)
			{
				int PlayerStep = staticScript .frontMoverManager .StepCount - 1 + (int)staticScript .frontMoverManager .GetProgress;
				int distance = StepPos - PlayerStep;
				if(ShockTime == 0)
				{
					bool IsChange = true;
					int id;
					while (IsChange)
					{
						IsChange = false;
						switch ((STATE)Step)
						{
							case STATE.NO_RESPAWN:
							if (staticScript .staticEnemyAlg .IsEnemySpawn( distance ))
							{
								IsChange = true;
								emitModel = enemySpawnAlg .EnemySpawn();
								emitModel .transform.parent = transform;
								staticScript .spawnParticle .Emit( transform .position );
								weakPoint = emitModel .GetComponent<EnemyWeakPoint>() .weakPoint;
								Step++;
							}
							break;

							case STATE .RESPAWN:
							if (staticScript .staticSwordEnemyAlg .IsCanMove( distance ))
							{
								animator .SetBool( "Walking" , true );
								IsChange = true;
								Step++;
							}
							break;

							case STATE .STEP:

							if (staticScript .staticSwordEnemyAlg .IsCanAttack( distance ) /* || enemyAttackAlg .loopTime >= 1*/ )
							{
								IsChange = true;
								animator .SetBool( "Walking" , false );
								Step++;
							}
							else
							{
								StepPos--;
								Stepping = true;
							}
							break;
							case STATE .STOP:
							Vector2 ScrPos = staticScript.staticSwordEnemyAlg.RightMarkerPos;
							if(!IsRight)
								ScrPos = -ScrPos;
								if(IsRight)
									id = 1;
								else
									id = 0;
							if (staticScript .enemyAttackManager .EntrySword( ScrPos , 2 + (int)staticScript .rhythmManager .DynamicRhythm .Pos , staticScript .staticSwordEnemyAlg .Marker , staticScript .enemyLines .GetSwordEnemyGoalPos( id ) , this ))
							{
								WaitStep = 2;

								Step++;
							}

						
							break;

							case STATE .ATTACKREADY:
							WaitStep--;

							if (WaitStep == 1)
							{
								DoubleAttackMotionStart();
								animator .SetBool( "IsRight" , IsRight );
							}
							if(WaitStep <= 0)
							{
								Step++;
								staticScript .enemySEManager .SwordAttackSE();
								JumpStartRhythm = (int)staticScript.rhythmManager.DynamicRhythm.Pos + 1;
							}
							break;

							case STATE .ATTACKMOTION:
							if (staticScript .staticSwordEnemyAlg.IsEnemyEscape( distance ) )
							{
								staticScript .enemyAttackManager .RemoveFromEnemy( this );
								staticScript .spawnParticle .Emit( transform .position );
								Destroy( this .gameObject );
								return;
							}
								if(IsRight)
									id = 1;
								else
									id = 0;

							JumpEndPos = staticScript .enemyLines .GetSwordEnemyGoalPos( id );
							JumpLen = transform.position - JumpEndPos .position;
							//JumpStartRhythm = (int)staticScript .rhythmManager .DynamicRhythm .Pos;
							staticScript .enemySEManager .SwordAttackSE2();
								Step++;
							//ジャンプ処理
							break;

						}

					}

				}

			}

		}
		if (!StepState && !IsDeath)
		{
			float SteppingStep = .0f;
			if (Stepping)
			{
				SteppingStep = ( staticScript .staticSwordEnemyAlg .StepTime - (staticScript .rhythmManager .DynamicRhythm .Pos - (int)staticScript .rhythmManager .DynamicRhythm .Pos) ) / staticScript.staticSwordEnemyAlg.StepTime;
				if (SteppingStep < .0f)
					SteppingStep = .0f;
			}

			float RetStep = SteppingStep + StepPos;
			transform .position = staticScript .frontMoverManager .GetPosFromStep( RetStep ) + staticScript .staticSwordEnemyAlg .GetWidth( IsRight ) + staticScript .staticEnemyAlg .AdjustHeight;
			if(PreJumpAttackState)
			{
				transform.position = transform.position + Vector3.up * JumpHeight.Evaluate(staticScript.rhythmManager.DynamicRhythm.Pos - JumpStartRhythm);
            }
		
		}
		if (StepState && !IsDeath)
		{
			transform .position = JumpEndPos .transform .position + JumpLen * ( 1.0f - ( staticScript .rhythmManager .DynamicRhythm .Pos - JumpStartRhythm ) ) + Vector3 .up * JumpHeight .Evaluate( ( staticScript .rhythmManager .DynamicRhythm .Pos - JumpStartRhythm )  );
		}
		if (IsDeath && !DeathAction)
		{
			DeathAction = true;
			staticScript .enemyAttackManager .RemoveFromEnemy( this );
			//staticScript .enemySEManager .SwordEnemyDeath();
			staticScript .enemySEManager .EnemyVoices();
			DeathMotionStart();
			DeathEnemy();

		}

		if (DeathAction)
		{
			if (IsDeathAnimationEnd())
				Destroy( this .gameObject );
			return;
		}
	}

	override public void MarkerDestroyAction()
	{
		Damage( 100 );
	}

}
