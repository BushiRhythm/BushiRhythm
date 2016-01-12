using UnityEngine;
using System.Collections;

public class SwordSE : MonoBehaviour {


	[SerializeField]
	AudioSource UnHitSE;

	[SerializeField]
	AudioSource HitSE;

	[SerializeField]
	SE CounterSE;



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
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(staticScript.actionTymingManager.IsBladeActiond())
		{
			for (int i = 0;i < staticScript.bulletResultManager.PulseResultDataCount;i++)
			{
				if(staticScript.bulletResultManager.GetPulseResultData(i).IsCounterEmited)
				{
					Instantiate( CounterSE );
				}

			}
			Instantiate( HitSE );
			return;
		}
	}
}
