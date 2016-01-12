using UnityEngine;
using System.Collections;

public class SmallWave : MonoBehaviour {


	AnimationCurve XPos;
	AnimationCurve YPos;

	bool IsLoad = false;

	[SerializeField]
	float XValue;

	[SerializeField]
	float YValue;

	Vector3 DefaultPos;

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
		DefaultPos = transform .localPosition;

	}
	
	// Update is called once per frame
	void Update () {
		if (!IsLoad)
		{
			if (staticScript .smallwaveManager == null)
				return;
			XPos = staticScript .smallwaveManager .XPos;
			YPos = staticScript .smallwaveManager .YPos;
			IsLoad = true;
		}
		float Pos = staticScript.rhythmManager.FixedRhythm.Pos;
		Vector3 pos = DefaultPos;
		pos .x += XPos .Evaluate( Pos ) * XValue;
		pos .y += YPos .Evaluate( Pos ) * YValue;

		transform .localPosition = pos;
	}
}
