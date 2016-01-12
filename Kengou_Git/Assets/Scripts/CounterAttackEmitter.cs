using UnityEngine;
using System.Collections;

public class CounterAttackEmitter : MonoBehaviour {


	BulletResultManager _bulletResultManager;

	[SerializeField]
	CounterAttackManager[] Managers;

	[SerializeField]
	GameObject CounterAttackData;

	[SerializeField]
	bool OldUpdate = false;

	[SerializeField]
	float JudgeDot = 0.8f;

	Vector2[] JudgeAxis = 
	{
		new Vector2 (-1.0f,1.0f),
		new Vector2 (1.0f,1.0f),
		new Vector2 (1.0f,-1.0f),
		new Vector2 (-1.0f,-1.0f)
	};


	Sword _sword;

	Sword sword
	{
		get
		{
			if(!_sword)
				_sword = FindObjectOfType<Sword>();

			return _sword;
		}
	}

	public BulletResultManager bulletResultManager
	{
		get
		{
			if (!_bulletResultManager)
				_bulletResultManager = FindObjectOfType<BulletResultManager>();
			return _bulletResultManager;
		}
	}

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
	
	}

	void New_Update()
	{
		if (bulletResultManager .PulseResultDataCount == 0)
			return;


		//切った方向から呼び出すマネージャーを設定
		Vector2 swordDir = sword .SwordDirection;
		int UseManager = -1;
		float MaxDot = -float .MaxValue;

		//基準となる軸から何度ずれているかで決定する
		Vector2 StandardAxis = new Vector2( -1.0f , 1.0f );
		StandardAxis .Normalize();

		float MinDot = float .MaxValue;


		Vector3 StartAxis = new Vector3( swordDir .x , swordDir .y , .0f );

		for (int i = 0;i < bulletResultManager .PulseResultDataCount;i++)
		{

			if (bulletResultManager .GetPulseResultData( i ) .Miss)
				return;
			if (!bulletResultManager .GetPulseResultData( i ) .IsCanCounterEmit)
				return;
			//マーカーのビューポート位置を取得
			Vector2 EnemyViewport = new Vector2();
			Vector2 MarkerViewport = Camera .main .WorldToViewportPoint( bulletResultManager .GetPulseResultData( i ) .MarkerWorldPos );

			//一番右隣で近い軸を探す
			for (int j = 0;j < JudgeAxis .Length;j++)
			{
				//敵のビューポート位置を取得
				if (!Managers[j] .GetEnemyViewPort(ref EnemyViewport ))
					continue;
				Vector2 Length = EnemyViewport - MarkerViewport;
				Length.Normalize();

				float dot = Vector2 .Dot( Length , swordDir );
				if (dot < JudgeDot)
					continue;
				if (MaxDot < dot)
				{
					MaxDot = dot;
					UseManager = j;
				}

			}
			//位置と方向からカウンターアタックを作成
			//初期位置がStartPosになる
			Vector3 StartPos = bulletResultManager .GetPulseResultData( i ) .WorldPos;

			float NearLimit = Camera .main .transform .position .z + Camera .main .nearClipPlane + 0.1f;

			if (NearLimit > StartPos .z)
				StartPos .z = NearLimit;
			//軌跡が見えなくなるのを防止

			//試しに弾き返しを凍結
			if (UseManager == -1)
				return;

			GameObject emitobj = Instantiate( CounterAttackData , StartPos , Quaternion .identity ) as GameObject;
			AttackCounterEffecter CounterAttack = emitobj .GetComponent<AttackCounterEffecter>();

			CounterAttack .StartAxis = StartAxis;

			CounterAttack .MaxTime = staticScript .rhythmManager .OnTempoTime;

			bulletResultManager .GetPulseResultData( i ) .IsCounterEmited = true;


			Managers[UseManager] .CounterAttackEntry( CounterAttack );
		}
	
	}

	void Old_Update()
	{
		if (bulletResultManager .PulseResultDataCount == 0)
			return;


		//切った方向から呼び出すマネージャーを設定
		Vector2 swordDir = sword .SwordDirection;
		int UseManager = -1;
		float MaxDot = -float .MaxValue;

		//基準となる軸から何度ずれているかで決定する
		Vector2 StandardAxis = new Vector2( -1.0f , 1.0f );
		StandardAxis .Normalize();

		//一番右隣で近い軸を探す
		for (int i = 0;i < JudgeAxis .Length;i++)
		{
			Vector2 Axis = JudgeAxis[i] .normalized;
			if (Axis .x * swordDir .y - Axis .y * swordDir .x > 0.0f)
				continue;

			float dot = Vector2 .Dot( Axis , swordDir );
			if (MaxDot < dot)
			{
				MaxDot = dot;
				UseManager = i;
			}

		}


		Vector3 StartAxis = new Vector3( swordDir .x , swordDir .y , .0f );
		for (int i = 0;i < bulletResultManager .PulseResultDataCount;i++)
		{

			if (bulletResultManager .GetPulseResultData( i ) .Miss)
				return;

			//位置と方向からカウンターアタックを作成
			//初期位置がStartPosになる
			Vector3 StartPos = bulletResultManager .GetPulseResultData( i ) .WorldPos;

			float NearLimit = Camera .main .transform .position .z + Camera .main .nearClipPlane + 0.1f;

			if (NearLimit > StartPos .z)
				StartPos .z = NearLimit;
			//軌跡が見えなくなるのを防止

			GameObject emitobj = Instantiate( CounterAttackData , StartPos , Quaternion .identity ) as GameObject;
			AttackCounterEffecter CounterAttack = emitobj .GetComponent<AttackCounterEffecter>();

			CounterAttack .StartAxis = StartAxis;
			Managers[UseManager] .CounterAttackEntry( CounterAttack );
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (OldUpdate)
			Old_Update();
		else
			New_Update();
	}
}
