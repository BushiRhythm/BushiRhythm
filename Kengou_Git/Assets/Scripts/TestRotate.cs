using UnityEngine;
using System.Collections;

public class TestRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>() .AddTorque( new Vector3( 0 , 600.0f , .0f ) );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
