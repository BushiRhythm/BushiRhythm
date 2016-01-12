using UnityEngine;
using System.Collections;
using UnityEngine .UI;

public class TimeLimit : MonoBehaviour {

	float Adjust = 0.5f;

	int time;

	float FTime;
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

    Image _image;
    Image image
    {
        get
        {
            if (!_image)
                _image = GetComponent<Image>();
            return _image;
        }

    }


	// Use this for initialization
	void Start () {
	
	}

	public bool IsTimeOver
	{
		get
		{
			return FTime < .0f;
		
		}
	}

    int OldFTime = 0;

	// Update is called once per frame
    void Update()
    {
		FTime = ( staticScript .bgmData .BGMLength - staticScript .rhythmManager .FixedRhythm .Time - Adjust );

		time = (int)FTime;

        if (OldFTime != (int)time)
        {
            text.SetNum((int)FTime);
            OldFTime = (int)FTime;
            if (time <= 5)
            {
                image.color = Color.red;
            }
            else if (time < 30)
            {
                image.color = Color.yellow;
            }
            else
            {
                image.color = Color.white;
            }
            text.SetColor(image.color);
        }

	}
}
