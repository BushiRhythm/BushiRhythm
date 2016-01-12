using UnityEngine;
using System.Collections;

public class TopEnemyManager : MonoBehaviour {

	[SerializeField]
	GameObject enemy;

	bool IsPlay=true;
	
	Enemy spawnEnemy;

	public Enemy SpawnEnemy
	{
		get
		{
			return spawnEnemy;
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



	[SerializeField]
	Transform leftTopRoutePoint;

	public Transform LeftTopRoutePoint
	{
		get
		{
			return leftTopRoutePoint;
		}
	}



	[SerializeField]
	Transform rightTopRoutePoint;
	
	public Transform RightTopRoutePoint
	{
		get
		{
			return rightTopRoutePoint;
		}
	}

	RhythmManager _rhythmManager;

	RhythmManager rhythmManager
	{
		get
		{
			if (!_rhythmManager)
				_rhythmManager = Object .FindObjectOfType<RhythmManager>();
			return _rhythmManager;
		}
	}


	[SerializeField]
	AnimationCurve xPosCurve;

	public AnimationCurve XPosCurve
	{
		get
		{
			return xPosCurve;
		}
	}

	[SerializeField]
	AnimationCurve yPosCurve;

	public AnimationCurve YPosCurve
	{
		get
		{
			return yPosCurve;
		}
	}

	int RestRhythm;

	[SerializeField]
	int EmitRhythm;

	// Use this for initialization
	void Start () {
		SpawnCountReset();
	}
	
	void EnemySpawn()
	{
		spawnEnemy = ( Instantiate( enemy , Vector3 .zero , Quaternion .identity ) as GameObject ) .GetComponent<Enemy>();
	
	}

	void SpawnCountReset()
	{
		RestRhythm = EmitRhythm;
	}

	public void Play()
	{
		IsPlay = true;
		SpawnCountReset();
	}

	public void Stop()
	{
		IsPlay = false;
		if(spawnEnemy)
		{
			Destroy( spawnEnemy .gameObject );
		}
	}

	// Update is called once per frame
	void Update () {
		if (!RightTopRoutePoint)
		{
			if(staticScript.topRoutePoint != null)
			{
				leftTopRoutePoint = staticScript .topRoutePoint .LeftTopRoutePoint;
				rightTopRoutePoint = staticScript .topRoutePoint .RightTopRoutePoint;
			}
				
		}
		if (!IsPlay || EmitRhythm == 0)
			return;
		if(rhythmManager.DynamicRhythm.Tyming)
		{
			RestRhythm--;
			if(RestRhythm == 0)
			{
				EnemySpawn();
			}
			if (RestRhythm <0 && (spawnEnemy == null || spawnEnemy.IsDeathAction))
			{
				SpawnCountReset();
			}
		}
	}
}
