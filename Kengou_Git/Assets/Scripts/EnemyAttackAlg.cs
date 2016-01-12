using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

//敵の攻撃アルゴリズム
public class EnemyAttackAlg : MonoBehaviour {

	Vector2 ScreenPos;

	int CurRhythm;

	[SerializeField]
	MarkerPatternData markerPatternData;


	//private Canvas canvas;

	[SerializeField]
	private MarkerPattern pattern;

	bool IsPlay;


	int StartPos;

	int _loopTime;

	public int loopTime
	{
		get
		{
			return CurRhythm / markerPatternData .MaxRhythm;

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



	EnemySpawnAlg _enemySpawnAlg;

	EnemySpawnAlg enemySpawnAlg
	{
		get
		{
			if (!_enemySpawnAlg)
				_enemySpawnAlg = GetComponent<EnemySpawnAlg>();
			return _enemySpawnAlg;
		}
	}

	Enemy _enemy;

	Enemy enemy
	{
		get
		{
			if (!_enemy)
				_enemy = GetComponent<Enemy>();
			return _enemy;
		}
	}


	// Use this for initialization
	void Start () {
		//canvas = GameObject .Find( "MarkerCanvas" ) .GetComponent<Canvas>();
		//StartAttack();
	}

	void Emit()
	{
		if(staticScript .enemyAttackManager .EntryNormal( ScreenPos ,1 + (int)staticScript.rhythmManager.DynamicRhythm.Pos,  pattern.marker ,enemy .FirePoint,enemy))
		{
			bool IsEntry = false;
			if (markerPatternData .IsTyming( CurRhythm + 1 ,ref ScreenPos))
			{
				if (staticScript .enemyAttackManager .EntryNormal( ScreenPos , 2 + (int)staticScript .rhythmManager .DynamicRhythm .Pos , pattern .marker , enemy .FirePoint , enemy ))
				{
					IsEntry = true;
				}
			}

			if (IsEntry)
			{
				enemy .DoubleAttackMotionStart();
				//staticScript .enemySEManager .ShooterAttackSE();
			}
			else
			{
				enemy .SingleAttackMotionStart();
				//staticScript .enemySEManager .ShooterAttackSE();
			}
		}
	}

	
	// Update is called once per frame
	void Update () {



		if (IsPlay)
		{
			if (staticScript.rhythmManager .DynamicRhythm .Tyming)
			{
				if (enemy .IsWaitMotion())
				{
					if (markerPatternData.IsTyming(CurRhythm,ref ScreenPos))
						Emit();				
				}

				CurRhythm++;

			}

		}
	}

	public void StartAttack()
	{
		IsPlay = true;
		CurRhythm = 0;
	}

	public void StopAttack()
	{
		IsPlay = false;

	}
}
