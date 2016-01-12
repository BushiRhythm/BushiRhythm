using UnityEngine;
using UnityEngine .UI;
using System.Collections;

public class Score : MonoBehaviour {

	BulletResultManager _bulletResultManager;

	public BulletResultManager bulletResultManager
	{
		get
		{
			if (!_bulletResultManager)
				_bulletResultManager = FindObjectOfType<BulletResultManager>();
			return _bulletResultManager;
		}
	}

    NumberParent _text;

    NumberParent text
	{
		get
		{
			if (!_text)
                _text = GetComponent<NumberParent>();
			return _text;
		}

	}

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
    GaugeTest _gauge;

    GaugeTest gauge
    {
        get
        {
            if (!_gauge)
                _gauge = FindObjectOfType<GaugeTest>();
            return _gauge;
        }

    }

	int score;
	int oldScore;

	bool IsPlay = true;

	public void Stop()
	{
		IsPlay = false;
	}

	public int GetScore()
	{
		return oldScore;
	}
	// Use this for initialization
	void Start () {
		oldScore = 0;
        text.SetNum(oldScore);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!IsPlay)
			return;
		for(int i = 0;i < bulletResultManager.PulseResultDataCount;i++ )
		{
            int ptr = bulletResultManager.GetPulseResultData(i).Score;
            score += ptr;
            gauge.AddGauge((int)(ptr * 0.125f));
		}
		if(score != oldScore)
		{
			oldScore = score;
            text.SetNum(oldScore);
		}
	}
}
