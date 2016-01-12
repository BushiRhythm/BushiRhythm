using UnityEngine;
using System.Collections;

public class BeatScale : MonoBehaviour {
	
	[SerializeField,Range(.0f,1.0f)]
	private float BeatSpeed = .0f;

	[SerializeField,Range(.0f,1.0f)]
	private float BeatPower = .0f;

	private float BeatSize = 1.0f;
	private Vector3 BaseSize;
	RhythmManager _RManager;
	
	RhythmManager RManager
	{
		get
		{
			if (!_RManager)
				_RManager = Object.FindObjectOfType<RhythmManager>();
			return _RManager;
		}
		
	}

	bool IsRManager = false;
	
	bool isManager () {
		if( _RManager)return true;
		if(!_RManager)return false;

		return false;

	}
	// Use this for initialization
	void Start () {
		_RManager = Object.FindObjectOfType<RhythmManager>();
		IsRManager = isManager ();
		BaseSize = transform.localScale;
	}

	void RhythmUpdate(){
		if (RManager.FixedRhythm.Tyming) {
			ImageBeat();
		}
	}
	// Update is called once per frame
	void Update () {

		BeatSize = BeatSize * (1.0f - BeatSpeed) + BeatSpeed;
		this.transform.localScale = BeatSize * BaseSize;
		if (IsRManager)
			RhythmUpdate ();
	}

	public void ImageBeat(){
		BeatSize = 1.0f - BeatPower;
	}

}
