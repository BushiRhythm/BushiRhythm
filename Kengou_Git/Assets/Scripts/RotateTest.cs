using UnityEngine;
using System.Collections;

public class RotateTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion l = Quaternion.AngleAxis(10, Vector3.up);

        transform.rotation *= l;
	}
}
