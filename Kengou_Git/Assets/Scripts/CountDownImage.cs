using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDownImage : MonoBehaviour {

	[SerializeField]
	AnimationCurve ScaleCurve;

	[SerializeField]
	AnimationCurve AlphaCurve;

	public Image image;

	public int MaxRhythm = 1;

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

	int StartRhythm;


	public float RotateSpeed;

	public float DefaultScale;


	// Use this for initialization
	void Start () {
		StartRhythm = (int)staticScript .rhythmManager .ReadyFixedRhythm .Pos;
	}
	
	// Update is called once per frame
	void Update () {
		transform .rotation *= Quaternion .Euler( .0f , .0f , RotateSpeed );
		float scale = DefaultScale * ScaleCurve .Evaluate( ( staticScript .rhythmManager .ReadyFixedRhythm .Pos - StartRhythm ) / (float)MaxRhythm );
		Color col = image.color;
		col .a = AlphaCurve .Evaluate( ( staticScript .rhythmManager .ReadyFixedRhythm .Pos - StartRhythm ) / (float)MaxRhythm );
		image .color = col;
		transform.localScale = new Vector3(scale,scale,scale);
		if (staticScript .rhythmManager .ReadyFixedRhythm .Pos - StartRhythm >= MaxRhythm)
			Destroy( this .gameObject );
	}
}
