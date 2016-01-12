using UnityEngine;
using System.Collections;

public class MoveSE : MonoBehaviour {

	[SerializeField]
	AudioSource SE;


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
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		if (staticScript .actionTymingManager .IsStepActiond())
		{
			SE .Play();
		}
	}
}
