using UnityEngine;
using System.Collections;

public class SlowScreen : MonoBehaviour {

    [SerializeField]
    AnimationCurve Curve;

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

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(staticScript.rhythmManager.IsSlow)
        {
           canvasRenderer.SetAlpha(Curve.Evaluate(staticScript.rhythmManager.SlowProgress));
		   staticScript .effectTest .BlendValue = Curve .Evaluate( staticScript .rhythmManager .SlowProgress ) * 1.5f;
        }
        else
        {
            canvasRenderer.SetAlpha(.0f);
			staticScript .effectTest .BlendValue = .0f;
        }
	}
}
