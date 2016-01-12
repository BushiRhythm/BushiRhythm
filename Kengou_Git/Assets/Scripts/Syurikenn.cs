using UnityEngine;
using System.Collections;

public class Syurikenn : MonoBehaviour {
	[SerializeField]
	float RotateSpeed;

	float YAngle;

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
		Vector3 forward = transform .forward;
		Vector3 Right = Vector3 .Cross( Vector3 .up , forward );
		Vector3 Up = Vector3 .Cross( forward , Right );

		Up .Normalize();
		transform .rotation *= Quaternion .AngleAxis( RotateSpeed  * staticScript.rhythmManager.DynamicRhythm.DeltaTime, Up  );
	}
}
