using UnityEngine;
using System.Collections;

public class MarkerDestroySEManager : MonoBehaviour {

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

	[SerializeField]
	SE[] HitSE;

	[SerializeField]
	SE Damage;

	[SerializeField]
	SE DamageVoice;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (staticScript .bulletResultManager .PulseResultDataCount == 0)
			return;

		for (int i = 0;i < staticScript .bulletResultManager .PulseResultDataCount;i++)
		{
			BulletResultData data = staticScript .bulletResultManager .GetPulseResultData( i );
			if (data .Miss)
			{
				Instantiate( Damage );
				//Instantiate( DamageVoice );
				continue;
			}


			Instantiate(HitSE[Mathf .Abs( data .ResultRank )]);

		}
	}
}
