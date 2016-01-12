using UnityEngine;
using System.Collections;

public class TempoHuzi : MonoBehaviour {

	RhythmManager _rhythmManager;

	RhythmManager rhythmManager
	{
		get
		{
			if (!_rhythmManager)
				_rhythmManager = Object .FindObjectOfType<RhythmManager>();
			return _rhythmManager;
		}
	}


	[SerializeField]
	float NyokiTime = 0.3f;

	float WorkTime = .0f;

	[SerializeField]
	float NyokiHeight = 3.0f;

	float DefaultHeight;

	// Use this for initialization
	void Start () {
		DefaultHeight = transform .localPosition .y;
	}
	
	// Update is called once per frame
	void Update () {
		WorkTime -= rhythmManager .FixedRhythm .DeltaTime;

		if (WorkTime < .0f)
			WorkTime = .0f;
		if(rhythmManager.FixedRhythm.Tyming)
		{
			WorkTime = NyokiTime;
		}

		Vector3 pos = transform .localPosition;
		pos .y = NyokiHeight * WorkTime / NyokiTime + DefaultHeight;
		transform .localPosition = pos;
	}
}
