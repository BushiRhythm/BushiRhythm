using UnityEngine;
using System.Collections;

public class ShadowProb : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform .position;
		pos .y = .02f;
		transform .position = pos;

		transform .forward = -Vector3 .up;
	}
}
