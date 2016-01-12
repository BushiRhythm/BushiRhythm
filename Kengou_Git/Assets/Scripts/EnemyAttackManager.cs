using UnityEngine;
using System.Collections;
using System .Collections .Generic;

public class ReserveMarkerData	//予約するマーカーデータ
{
	public Vector2 Pos;
	public int Tyming = -1;
	public GameObject Bullet;
	public Transform FirePoint;
	public Enemy FireEnemy;
	public AimingMarker AimingMarker;
	public bool IsSwordEnemy;
	
}

public class EnemyAttackManager : MonoBehaviour {

			
	Canvas _markerCanvas;

	Canvas markerCanvas
	{
		get
		{
			if (_markerCanvas)
			{
				_markerCanvas = GameObject .Find( "MarkerCanvas" ) .GetComponent<Canvas>();
			}
			return _markerCanvas;

		}

	}
	

	[SerializeField]
	AimingMarker aimingMarker;

	StaticScript _staticScript;

	StaticScript staticScript
	{
		get
		{
			if (!_staticScript)
				_staticScript = GameObject .FindGameObjectWithTag("StaticScript").GetComponent<StaticScript>();
			return _staticScript;
		}
	}

	List<ReserveMarkerData> reserveMarkerDataList;

	Marker Emit(ReserveMarkerData data)
	{
		return staticScript .staticMarkerEmitter .Emit( data .Bullet , data .Pos , data .FirePoint.position,data.FireEnemy,data.IsSwordEnemy );
	}

	AimingMarker AimingMarkerEmit( ReserveMarkerData data )
	{
		return staticScript .staticMarkerEmitter .AimingMarkerEmit( aimingMarker , data );
	}

	void	List_Remove(int i )
	{
		if (reserveMarkerDataList[i] .AimingMarker != null)
			Destroy( reserveMarkerDataList[i] .AimingMarker .gameObject );

		reserveMarkerDataList .RemoveAt( i );

	}

	private bool Entry(Vector2  Pos , int Tyming,GameObject Bullet, Transform FirePoint, Enemy enemy, bool IsSwordEnemy)
	{
		int BulletCount = 0;
		for (int i = 0;i < reserveMarkerDataList .Count;i++)
		{
			ReserveMarkerData data = reserveMarkerDataList[i];

			if (data .Tyming == Tyming)
				BulletCount++;

			if (BulletCount >= 1)
				return false;
			
		}
		//攻撃できそうなら
		

		//現在出ているマーカーと重ならないように調整する
		float Radius = 0.5f;
		float RadiusSq = Radius * Radius;

		bool loop = true;
		int cnt = 0;

		Vector2 LengthSq ;

		while (loop && cnt < 5 && !IsSwordEnemy)
		{
			cnt++;
			loop = false;
			foreach (Marker marker in staticScript.markerManager.MarkerList)
			{
				LengthSq = Pos - marker .PosOnScreen;
				if (LengthSq .sqrMagnitude == .0f)
				{
					LengthSq = Random .insideUnitCircle * 0.01f;
				}
				if(LengthSq.sqrMagnitude <RadiusSq)
				{
					LengthSq .Normalize();
					Pos =marker .PosOnScreen + LengthSq * Radius;
					loop = true;
				}
			}

			foreach(ReserveMarkerData data in reserveMarkerDataList)
			{
				if (data .IsSwordEnemy)
					continue;
				LengthSq = Pos - data .Pos;
				if (LengthSq .sqrMagnitude == .0f)
				{
					LengthSq = Random .insideUnitCircle * 0.01f;
				}
				if (LengthSq .sqrMagnitude < RadiusSq)
				{
					LengthSq .Normalize();
					Pos = data .Pos + LengthSq * Radius;
					loop = true;
				}


			}


		}

		ReserveMarkerData add = new ReserveMarkerData();
		add.Tyming =Tyming;
		add .Bullet = Bullet;
		add .FirePoint = FirePoint;
		add .Pos = Pos;
		add .FireEnemy = enemy;
		add .IsSwordEnemy = IsSwordEnemy;
		reserveMarkerDataList .Add( add );
		return true;
	}

	public bool EntryNormal( Vector2 Pos , int Tyming , GameObject Bullet , Transform FirePoint , Enemy enemy)
	{
		return Entry( Pos , Tyming , Bullet , FirePoint, enemy , false );
	}

	public bool EntrySword( Vector2 Pos , int Tyming , GameObject Bullet , Transform FirePoint , Enemy enemy )
	{
		return Entry( Pos , Tyming , Bullet , FirePoint , enemy , true );
	}

	// Use this for initialization
	void Start () 
	{
		reserveMarkerDataList = new List<ReserveMarkerData>();
	}

	public void RemoveFromEnemy(Enemy enemy)
	{
		for (int i = 0;i < reserveMarkerDataList .Count;i++)
		{
			ReserveMarkerData data = reserveMarkerDataList[i];

			if(data.FireEnemy == enemy)
			{
				List_Remove( i );
				i--;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!staticScript .rhythmManager .DynamicRhythm .Tyming)
			return;
		int CurTyming = (int)staticScript .rhythmManager .DynamicRhythm .Pos;
		for (int i = 0;i < reserveMarkerDataList .Count;i++)
		{
			ReserveMarkerData data = reserveMarkerDataList[i];
			if (data .AimingMarker == null && !data .IsSwordEnemy)
			{
				data .AimingMarker = AimingMarkerEmit( data );
				data .AimingMarker .Setting_DefaultScale();
			}

			if (data .Tyming == CurTyming)
			{
				Emit( data );
				List_Remove( i );
				i--;
			}
		}

		
	}
}
