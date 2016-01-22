using UnityEngine;
using System.Collections;

public class Syurikenn : MonoBehaviour {
	[SerializeField]
	float RotateSpeed;

	float YAngle;

	float RotateSpeedZ;

	float ZAngle;

	float RotateSpeedX;

	float XAngle;

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
		RotateSpeedZ = Random.Range(-100.0f, 100.0f);
		RotateSpeedX = 30.0f;
	}
	
	// Update is called once per frame
	void Update () {
		YAngle += RotateSpeed * staticScript.rhythmManager.DynamicRhythm.DeltaTime;
		XAngle += RotateSpeedX * staticScript.rhythmManager.DynamicRhythm.DeltaTime;
		ZAngle += RotateSpeedZ * staticScript.rhythmManager.DynamicRhythm.DeltaTime;

		transform.rotation = Quaternion.identity;

		transform.rotation *= Quaternion.AngleAxis(ZAngle, Vector3.forward);

		transform.rotation *= Quaternion.AngleAxis(XAngle, Vector3.right);

		transform.rotation *= Quaternion.AngleAxis(YAngle, Vector3.up);

		//Up .Normalize();
		//transform .rotation *= Quaternion .AngleAxis( RotateSpeed  * staticScript.rhythmManager.DynamicRhythm.DeltaTime, Up  );
	}
}
