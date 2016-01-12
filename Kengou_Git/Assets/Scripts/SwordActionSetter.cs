using UnityEngine;
using System .Collections;

public class SwordActionSetter : MonoBehaviour
{

	ActionTymingManager _actionTymingManager;
	ActionTymingManager actionTymingManager
	{
		get
		{
			if (!_actionTymingManager)
				_actionTymingManager = Object .FindObjectOfType<ActionTymingManager>();
			return _actionTymingManager;
		}

	}

	Sword _sword;
	Sword sword
	{
		get
		{
			if (!_sword)
				_sword = Object .FindObjectOfType<Sword>();
			return _sword;
		}

	}
	FlickManager _flickManager;
	FlickManager flickManager
	{
		get
		{
			if (!_flickManager)
				_flickManager = Object .FindObjectOfType<FlickManager>();
			return _flickManager;
		}

	}

	StaticScript _staticScript;
	StaticScript staticScript
	{
		get
		{
			if (!_staticScript)
				_staticScript = Object .FindObjectOfType<StaticScript>();
			return _staticScript;
		}

	}

	private bool flicknot2 = false;
	private bool flickCheck;
	private GameObject Collision2DFlick;

	RectTransform CollisionFlickTransform;
	BoxCollider CollisionFlick;

	[SerializeField , HeaderAttribute( "フリック速度判定" )]
	float FlickCheckSpeed = 1.0f;


	private GameObject MarkerCanvas;

    private Marker hitMarker;

	// Use this for initialization
	void Start()
	{
		Collision2DFlick = GameObject .Find( "swordCollisionFlick" );
		CollisionFlick = Collision2DFlick .GetComponent<BoxCollider>();
		CollisionFlickTransform = Collision2DFlick .GetComponent<RectTransform>();

		MarkerCanvas = GameObject .Find( "MarkerCanvas" );
	}

	// Update is called once per frame
	void Update()
	{
		flickCheck = false;

		if (!flickManager .IsFlick())
		{
			flicknot2 = false;
		}

		if (flicknot2)
		{
			return;
		}

		//if (flickManager.MarkeMoveTymingFlag())
		//{
		//    if(flickManager.IsTouch2())
		//    {
		//        if(flickManager.FlickEndPos != flickManager.FlickStartPos)
		//            flickCheck = true;
		//    }
		//}
		//
		if (flickManager .IsFlick())
		{
			Vector3 pos = Vector3 .zero;
			Vector3 scale = Vector3 .zero;
			Quaternion q = Quaternion .identity;

			FlickSword( ref pos , ref q , ref scale );

			CollisionFlickTransform .localPosition = pos;
			CollisionFlickTransform .rotation = q;
			CollisionFlickTransform .localScale = scale;
			if (flickManager .OneFlickSpeed >= FlickCheckSpeed)
				foreach (Marker m in staticScript .markerManager .MarkerList)
				{
					if (m == null)
						continue;
					if (flickCheck)
						break;

					if (m .IsSwordTyming)
					{
						RaycastHit RH;
						Ray r = new Ray( sword .StartPos , sword .SwordDirection3D );
						if (m .RayCastHit( r , out RH , 5000 ))
						{
                            hitMarker = m;
							//m .DestroyRequest();
							flickCheck = true;
						}
						else
						{
							Vector3 r2Pos = sword .StartPos;
							r2Pos .z -= 30000;
							Vector3 r2Dir = new Vector3( 0 , 0 , 1 );


							Ray r2 = new Ray( r2Pos , r2Dir );
							if (m .RayCastHit( r2 , out RH , 30000 ))
							{
                                hitMarker = m;
								//m .DestroyRequest();
								flickCheck = true;
							}
						}
					}
				}


			if (Object .FindObjectOfType<SwordFlickCollision>() .IsCollision && flickManager .OneFlickSpeed >= FlickCheckSpeed)
				flickCheck = true;

			Object .FindObjectOfType<SwordFlickCollision>() .IsCollision = false;
		}
		if (flickManager .IsEndFlick())
		{
			flickCheck = true;
		}

		if (flickCheck)
		{
			flicknot2 = true;
			actionTymingManager .SetBladeAction();
		}
	}

	private void FlickSword( ref Vector3 pos , ref Quaternion q , ref Vector3 scale )
	{
		Vector2 size = MarkerCanvas .GetComponent<RectTransform>() .sizeDelta;

		CollisionFlick .enabled = true;
		Vector2 FlickEndPos = flickManager .FlickEndPos;
		Vector2 FlickStartPos = flickManager .FlickStartPos;

		Vector2 FlickDirection = FlickEndPos - FlickStartPos;

		float leng = Mathf .Sqrt( Mathf .Pow( FlickDirection .x , 2 ) + Mathf .Pow( FlickDirection .y , 2 ) );

		float rad = Mathf .Atan2( FlickDirection .y , FlickDirection .x );

		FlickDirection = ( FlickEndPos + FlickStartPos ) / 2;
		rad += 3.141592f / 2;
		rad *= Mathf .Rad2Deg;




		scale = Collision2DFlick .transform .localScale;


		scale .x = 0.40f;
		//scale.x = 600 / Screen.width;

		scale .y = leng * 0.01f;



		q = Quaternion .AngleAxis( rad , Camera .main .transform .forward );
		//Collision2D.transform.position = FlickStartPos;


		Vector3 PosOnScreen = FlickStartPos;

		//PosOnScreen.x /= Screen.width;
		//PosOnScreen.y /= Screen.height;

		PosOnScreen .x /= Camera .main .pixelWidth;
		PosOnScreen .y /= Camera .main .pixelHeight;

		PosOnScreen .x = ( PosOnScreen .x - 0.5f ) * 2;
		PosOnScreen .y = ( PosOnScreen .y - 0.5f ) * 2;



		//AdjustScreenを使って移動させる

		pos .x = ( size .x / 2 ) * PosOnScreen .x;
		pos .y = ( size .y / 2 ) * PosOnScreen .y;
		pos .z = .0f;

	}

    public Marker HitMarker
    {
        get { return hitMarker; }
    }

    public bool FlickNotCheck
    {
        get { return flicknot2; }
    }

}
