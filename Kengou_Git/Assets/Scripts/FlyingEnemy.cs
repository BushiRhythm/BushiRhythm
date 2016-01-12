using UnityEngine;
using System.Collections;

public class FlyingEnemy : Enemy {

	int RestAttack;
	[SerializeField]
	int AttackCount = 3;

	[SerializeField]
	int RadiusStep = 5;

	int CurStep = -1;

	bool RightMove = true;

	[SerializeField]
	AnimationCurve ForceCurve;

	Transform DeathMoveParent;

	float DeathTime;

	Vector3 DeathDir;

	Vector2 Pos;
	float Dir	;


	[SerializeField]
	private GameObject marker;

	[SerializeField]
	Animator _animator;

	Transform LeftTopRoutePoint;

	Transform RightTopRoutePoint;

	AnimationCurve XPosCurve;

	AnimationCurve YPosCurve;

	StaticScript _staticScript;

	StaticScript staticScript
	{
		get
		{
			if (!_staticScript)
				_staticScript = GameObject .FindGameObjectWithTag( "StaticScript" ) .GetComponent<StaticScript>();
			return _staticScript;
		}
	}
	// Use this for initialization
	void Start () {
		base .Start();
		LeftTopRoutePoint = staticScript.topEnemyManager .LeftTopRoutePoint;
		RightTopRoutePoint = staticScript .topEnemyManager .RightTopRoutePoint;
		XPosCurve = staticScript .topEnemyManager .XPosCurve;
		YPosCurve = staticScript .topEnemyManager .YPosCurve;
		firePoint = GetComponent<EnemyFirePoint>() .firePoint;
		weakPoint = GetComponent<EnemyWeakPoint>() .weakPoint;
	}

	void DeathMove()
	{
		float Force = ForceCurve .Evaluate( DeathTime ) * staticScript .rhythmManager .DynamicRhythm .DeltaPos;
		DeathMoveParent .position += DeathDir * Force * 60.0f;
		DeathMoveParent .rotation *= Quaternion .AngleAxis( Force * 1500.0f , DeathDir );
		DeathDir += Random .insideUnitSphere * 0.05f;
		DeathDir.y += 0.01f;
		DeathDir .Normalize();
		transform .localPosition = transform .localPosition + transform .localPosition .normalized * Force;
	
	}

	void NormalMove()
	{
		if (staticScript.rhythmManager .DynamicRhythm .Tyming)
		{
			CurStep++;
			if (CurStep >= RadiusStep * 2)
			{
				RightMove = !RightMove;
				CurStep = 0;
			}

			if(RestAttack >0)
			{
				RestAttack--;
				Attack();
			}
			if (CurStep == RadiusStep - 3)
				AttackStart();
		}
		//位置を更新

		Vector3 Start;
		Vector3 Len;

		if(RightMove)
		{
			Len = RightTopRoutePoint .position - LeftTopRoutePoint .position;
			Start = LeftTopRoutePoint .position;
		}
		else
		{
			Len = LeftTopRoutePoint .position - RightTopRoutePoint .position;
			Start = RightTopRoutePoint .position;
		}

		Vector3 OneStep = Len / (RadiusStep * 2);

		float WorkPos = staticScript .rhythmManager .DynamicRhythm .Pos - (int)staticScript .rhythmManager .DynamicRhythm .Pos;

		Vector3 YAxis = Vector3.up * OneStep.magnitude;

		transform .position = Start + OneStep * ( CurStep + XPosCurve .Evaluate( WorkPos ) ) + YAxis * YPosCurve .Evaluate( WorkPos );
	}
	
	// Update is called once per frame
	void Update () {

		if (!DeathAction)
			NormalMove();
		else
			DeathMove();

		if (DeathAction)
			DeathTime += staticScript .rhythmManager .DynamicRhythm .DeltaPos;

		if (IsDeath)
		{
			if(!DeathAction)
			{

				DeathMoveParent = new GameObject().transform;
				DeathMoveParent .gameObject .name = "DeathMoveParent";
				DeathMoveParent .position = transform .position + Random .insideUnitSphere * 0.4f;
				transform .parent = DeathMoveParent;
				DeathAction = true;
				DeathDir = (transform .position - staticScript .gameMainCamera .MainCamera .transform.position).normalized;
				DeathDir .x = -DeathDir .x;
				staticScript .enemyAttackManager .RemoveFromEnemy( this );
				staticScript .enemySEManager .EnemyVoices();
				//staticScript .enemySEManager .TakoDamegeSE();
				DeathEnemy();
			}
		}

		if(ForceCurve.keys[ForceCurve.length -1] .time < DeathTime)
		{
			Destroy( this .gameObject );
			Destroy( DeathMoveParent .gameObject );
		}

	}

	void AttackStart()
	{
		Pos = new Vector2( Random .Range( -0.3f , 0.3f ) , Random .Range( -0.3f , 0.3f ) );
		Dir = Random .Range( .0f , Mathf .PI * 2 );

		RestAttack =AttackCount;
		_animator .SetTrigger( "AttackReady" );
	}

	override public void MarkerEmitAction()
	{
		_animator .SetTrigger( "Attack" );
		staticScript .enemySEManager .SyurikenThrow();
	}


	void Attack()
	{
		//staticScript .staticMarkerEmitter .Emit( marker , new Vector2( Random .Range( -0.3f , 0.3f ) , Random .Range( -0.3f , 0.3f ) ) , FirePoint .position );
		Dir += Random .Range( -1.7f , 1.7f);
		Pos.x += Mathf .Sin( Dir ) * 0.3f;
		Pos .y += Mathf .Cos( Dir ) * 0.3f;
		//強制ランダム
		Pos = new Vector2( Random .Range( -0.4f , 0.4f ) , Random .Range( -0.2f , 0.2f ) );
		staticScript .enemyAttackManager .EntryNormal( Pos, 3 + (int)staticScript .rhythmManager .DynamicRhythm .Pos , marker , FirePoint , this );

	}


}
