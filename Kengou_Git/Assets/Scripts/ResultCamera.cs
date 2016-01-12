using UnityEngine;
using System.Collections;

public class ResultCamera : MonoBehaviour {

	
	[SerializeField]
	AnimationCurve CurveX;

	[SerializeField]
	AnimationCurve CurveY;

	float Val = .0f;
	
	Vector3 OrgPos;

	// Use this for initialization
	void Start () {
		OrgPos = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Val < 1.0f) {
			Val += 0.0045f;
		}
		Vector3 pos = OrgPos;
		pos.z = pos.z + CurveX.Evaluate (Val) * 4.0f;
		pos.y = pos.y + CurveY.Evaluate ((1.0f-Val)) * 1.0f;
		Camera.main.transform.position = pos;
	}
}
