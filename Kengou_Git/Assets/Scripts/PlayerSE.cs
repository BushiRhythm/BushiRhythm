using UnityEngine;
using System.Collections;

public class PlayerSE : MonoBehaviour {

	[SerializeField]
	SE slow;
	
	public void Slow()
	{
		Instantiate( slow );
	}

	[SerializeField]
	SE beforeDeath;

	public void BeforeDeath()
	{
		Instantiate( beforeDeath );
	}


	[SerializeField]
	SE timeOver;

	public void TimeOver()
	{
		Instantiate( timeOver );
	}

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

	void Update()
	{
		//if(staticScript.rhythmManager.FixedRhythm.Tyming)
		//{
		//	BeforeDeath();
		//}
	}
}
