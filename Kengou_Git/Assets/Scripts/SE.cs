using UnityEngine;
using System.Collections;

public class SE : MonoBehaviour {
    AudioSource _AudioSource;

	float time;

	[SerializeField]
	float Maxtime = 6.0f;

	// Use this for initialization
	void Start () {
        _AudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!_AudioSource.isPlaying)
		{
			time += Time.unscaledDeltaTime;
			if(Maxtime < time)
				Destroy(gameObject);
		}

	}
}
