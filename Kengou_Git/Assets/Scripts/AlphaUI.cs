using UnityEngine;
using System.Collections;

public class AlphaUI : MonoBehaviour {

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

	float DefaultAlpha = .0f;

	// Use this for initialization
	void Start () {
		DefaultAlpha = canvasRenderer .GetAlpha();
	}
	
	// Update is called once per frame
	void Update () {
		canvasRenderer .SetAlpha( DefaultAlpha );
	}
}
