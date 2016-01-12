using UnityEngine;
using System .Collections;

public class StaticMarkerEmitter : MonoBehaviour
{

	private Canvas canvas;

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
	int LandingRhythm;


	public Marker Emit( GameObject marker , Vector2 ScreenPos , Vector3 FirePos ,Enemy enemy, bool IsSwordEnemy)
	{

		GameObject tmp = Instantiate( marker , Vector3 .zero , Quaternion .identity ) as GameObject;

		Marker markerCompornent = tmp .transform .GetComponent<Marker>();
		markerCompornent .MarkerCanvas = canvas .gameObject;
		tmp .transform .parent = canvas .transform;
		tmp .transform .localScale = marker .transform .localScale;
		markerCompornent .PosOnScreen .x = ScreenPos .x;
		markerCompornent .PosOnScreen .y = ScreenPos .y;
		markerCompornent .SetLandingRhythm( LandingRhythm );
		//markerCompornent .SetTime( bgmData.MarkerOffSetTime );
		markerCompornent .SettingPos();

		MarkerFirePoint markerFirePoint = markerCompornent .markerFirePoint;
		if (markerFirePoint != null)
		{
			markerFirePoint .SetStaticFirePoint( FirePos );
			markerFirePoint .SettingSpeed();
		}
		markerCompornent .HavingEnemy = enemy;
		if(markerCompornent.MarkerBullet == null)
		{
			if (enemy .WeakPoint != null)
				markerCompornent .MarkerBullet = enemy .WeakPoint;
			else
			{
				markerCompornent .MarkerBullet = markerCompornent.transform;
			}

		}
		staticScript .markerManager .Add( markerCompornent );
		markerCompornent .HavingEnemy .MarkerEmitAction();
		markerCompornent .IsCanCounter = false;

		if (!IsSwordEnemy)
		{
			markerCompornent .IsCanCounter = true;
		}

		return markerCompornent;
	}

	public AimingMarker AimingMarkerEmit( AimingMarker aimingMarker , ReserveMarkerData data )
	{

		GameObject tmp = Instantiate( aimingMarker .gameObject , Vector3 .zero , Quaternion .identity ) as GameObject;

		AimingMarker markerCompornent = tmp .transform .GetComponent<AimingMarker>();
		markerCompornent .MarkerCanvas = canvas .gameObject;
		tmp .transform .parent = canvas .transform;
		tmp .transform .localScale = aimingMarker .transform .localScale;
		markerCompornent .PosOnScreen .x = data .Pos .x;
		markerCompornent .PosOnScreen .y = data .Pos .y;
		markerCompornent .data = data;
		markerCompornent .SettingPos();
		return markerCompornent;
	}

	// Use this for initialization
	void Start()
	{
		canvas = GameObject .Find( "MarkerCanvas" ) .GetComponent<Canvas>();
	}


}
