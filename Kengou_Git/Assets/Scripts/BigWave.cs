using UnityEngine;
using System.Collections;

public class BigWave : MonoBehaviour {

	[SerializeField]
	Vector3 DefaultPos;

	[SerializeField]
	AnimationCurve Angle;

	[SerializeField]
	AnimationCurve XAdjast;

	StaticScript _staticScript;

	[SerializeField]
	float MoveScale = 10.0f;

	protected StaticScript staticScript
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
		DefaultPos = transform .localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		float time =staticScript .rhythmManager .FixedRhythm .Pos;

		float angle = Angle.Evaluate(time);

		Vector3 local = DefaultPos;
		local .x += DefaultPos .x + Mathf .Sin( angle ) * MoveScale + XAdjast.Evaluate(time) * MoveScale;
		local .y += DefaultPos .y + Mathf .Cos( angle ) * MoveScale;

		transform .localPosition = local;
	}
}
