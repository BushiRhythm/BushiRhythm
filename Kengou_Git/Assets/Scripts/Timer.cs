using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

    private float Time;

    Text _text;


    Text text
    {
        get
        {
            if (!_text)
                _text = GetComponent<Text>();
            return _text;
        }

    }

	bool IsPlay = true;

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


	CanvasRenderer _canvasRenderer;

	CanvasRenderer canvasRenderer
	{
		get
		{
			if (!_canvasRenderer)
				_canvasRenderer = GetComponent<CanvasRenderer>();
			return _canvasRenderer;
		}

	}


    RhythmManager _rhythmManager;

    RhythmManager rhythmManager
    {
        get
        {
            if (!_rhythmManager)
                _rhythmManager = Object.FindObjectOfType<RhythmManager>();
            return _rhythmManager;
        }

    }

	public void Stop()
	{
		IsPlay = false;
	}

	public float GetTime()
	{
		return Time;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!IsPlay)
			return;
        //Time += rhythmManager.DynamicRhythm.DeltaTime;
		Time = rhythmManager .DynamicRhythm .Time;
        if (Time < 0)
            Time = 0;
        text.text = string.Format("{0:00}:{1:00}:{2:0}", Mathf.Floor(Time / 60f), Mathf.Floor(Time % 60f), Mathf.Floor(Time % 1 * 10));
	}
}
