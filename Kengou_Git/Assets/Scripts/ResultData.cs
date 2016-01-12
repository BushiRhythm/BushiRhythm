using UnityEngine;
using System.Collections;

public class ResultData : MonoBehaviour {

	public int FinalScore;

	public float ClearTime;

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

	bool IsPlay = true;

	public void Stop()
	{
		IsPlay = false;
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad( this );
	}
    public void SetGoalData()
    {
        if (!_staticScript)
        	return;
        ClearTime = staticScript.timer.GetTime();
        FinalScore = staticScript.score.GetScore();
	}
	
	// Update is called once per frame
	void Update () {
		if (!IsPlay)
			return;
		if (!staticScript)
			return;
		ClearTime = staticScript .timer .GetTime();
		FinalScore = staticScript .score .GetScore();
	}
}
