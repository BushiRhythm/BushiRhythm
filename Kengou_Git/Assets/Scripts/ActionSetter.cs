using UnityEngine;
using System.Collections;

public class ActionSetter : MonoBehaviour {

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

	bool isStepActionSuccess;

	public bool IsStepActionSuccess
	{
		get
		{
			return isStepActionSuccess;
		}
	}

	bool isStepActionFailure;

	public bool IsStepActionFailure
	{
		get
		{
			return isStepActionFailure;
		}
	}

	bool adjusted;

	int WaitFrame;

	// Use this for initialization
	void Start () {
		Application .targetFrameRate = 60;

	}
	
	// Update is called once per frame
	void Update () {

		if (IsStepActionFailure)
		{
			Debug .Log( "失敗・・・" );
		}

		if (IsStepActionSuccess)
		{
			Debug .Log( "成功！" );
		}

		if(!adjusted)
		{
	#if UNITY_ANDROID
			float screenRate = (float)480 / Screen .height;
			if (screenRate > 1)
				screenRate = 1;
			int width = (int)( Screen .width * screenRate );
			int height = (int)( Screen .height * screenRate );
			Screen .SetResolution( width , height , false );
#endif

			adjusted = true;
		}

		isStepActionSuccess = false;
		isStepActionFailure = false;

		if (staticScript .push.Bpush)
		{
			staticScript .actionTymingManager .SetSlowAction();
		}
		if (staticScript .actionTymingManager .IsSlowActiond())
		{
			staticScript .rhythmManager .Slow( 16 );

		}


		//if (moveButton.Bpush)
		if (staticScript .moveButton .Bpush)
		{
			if (staticScript .actionTymingManager .SetStepAction())
			{
				isStepActionSuccess = true;
			}
			else
			{
				isStepActionFailure = true;
			}
		}
		if (staticScript .actionTymingManager .IsStepActiond() && !staticScript .frontMoverManager.IsGoal)
			staticScript .frontMoverManager .SetFrontMoveTime( staticScript .rhythmManager .OnTempoTime / 2 );

	}
}
