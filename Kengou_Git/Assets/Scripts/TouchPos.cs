using UnityEngine;
using System.Collections;

public class TouchPos : MonoBehaviour {

	[SerializeField]
	SpriteRenderer renderer;

	[SerializeField]
	TrailRenderer trail;

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
		if (!Input .GetKey( KeyCode .Mouse0 ))
		{
			renderer .enabled = false;
			return;
		}
		trail .enabled = true;
		renderer .enabled = true;
		Vector3 pos = Input .mousePosition;
		pos.x -= Screen .width / 2;
		pos .y -= Screen .height / 2;

		transform .localPosition = pos;


	}
}
