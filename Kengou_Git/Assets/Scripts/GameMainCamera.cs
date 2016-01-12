using UnityEngine;
using System.Collections;

public class GameMainCamera : MonoBehaviour {

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
	Camera Maincamera;

	public Camera MainCamera
	{
		get
		{
			return Maincamera;
		}
	}

	bool IsInit = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!IsInit)
		{
			if (staticScript .uiCanvas != null)
			{
				Maincamera .enabled = true;
			}
			IsInit = true;
		}
	}
}
