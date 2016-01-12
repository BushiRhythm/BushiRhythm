using UnityEngine;
using System.Collections;

public class TempoLine : MonoBehaviour {

    float MaxTime = 1.0f;
    float RestTime;

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
        RestTime -= rhythmManager.FixedRhythm.DeltaTime;
	    if(rhythmManager.FixedRhythm.Tyming)
        {
            MaxTime = RestTime = rhythmManager.OnTempoTime / 2;
        }
        canvasRenderer.SetAlpha(RestTime / MaxTime);

	}
}
