using UnityEngine;
using System.Collections;

public class MarkerCanvas : MonoBehaviour {


	bool IsInit = false;


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
	Canvas canvas;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		if (!IsInit)
		{
			if (staticScript .gameMainCamera != null)
			{
				canvas .worldCamera = staticScript .gameMainCamera .MainCamera;
			}
			IsInit = true;
		}
	}
}
