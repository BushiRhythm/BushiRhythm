using UnityEngine;
using System.Collections;

public class RandomPicther : MonoBehaviour {


	AudioSource _audioSource;

	AudioSource audioSource
	{
		get
		{
			if(_audioSource)
				_audioSource = GetComponent<AudioSource>();
			return _audioSource;
		}
	}
	[SerializeField]
	float RamdomValue;

	float DefaultPitch;

	float CurPitch;

	// Use this for initialization
	void Start () {
		DefaultPitch = audioSource .pitch;
		CurPitch= Random .Range( -RamdomValue , RamdomValue );
	}
	
	// Update is called once per frame
	void Update () {
		audioSource .pitch = DefaultPitch * CurPitch;
	}
}
