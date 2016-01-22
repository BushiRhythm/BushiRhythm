using UnityEngine;
using System.Collections;
using UnityEngine .UI;

public class AimingMarker : Marker {

	public ReserveMarkerData data;

	[SerializeField]
	AnimationCurve ScaleCurve;

	int StartRhythm;

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

	Image _image;

	Image image
	{
		get
		{
			if(!_image)
			{
				_image = GetComponent<Image>();
			}
			return _image;
		}
	}

	Vector3 DefaultScale;

	// Use this for initialization
	void Start () {
		StartRhythm = (int)staticScript .rhythmManager .DynamicRhythm .Pos;
	}

	public void Setting_DefaultScale()
	{
		DefaultScale = transform .localScale;
	}
	
	// Update is called once per frame
	void Update () {

	

		Color col = staticScript .markerColorManager .GetColor( data .Tyming ); 
		int length = data .Tyming - StartRhythm;

		float BeforeProgress = ((int) staticScript .rhythmManager .DynamicRhythm .Pos - StartRhythm ) / (float)length;
		float AfterProgress = ( (int)staticScript .rhythmManager .DynamicRhythm .Pos - StartRhythm + 1 ) / (float)length;
		//float Progress = ( staticScript .rhythmManager .DynamicRhythm .Pos - StartRhythm ) / length;
		float SubProgress = AfterProgress - BeforeProgress;
		float Progress = BeforeProgress + SubProgress * ScaleCurve .Evaluate( staticScript .rhythmManager .DynamicRhythm .Progress );

		

		if (Progress < .0f)
			Progress = .0f;
		if (Progress > 1.0f)
			Progress = 1.0f;

		//Progress = Progress * Progress;


		col .a =0.3f;
		transform .localScale = DefaultScale * Progress;

		image .color = col;
	}
}
