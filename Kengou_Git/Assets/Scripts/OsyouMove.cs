using UnityEngine;
using System.Collections;

public class OsyouMove : MonoBehaviour {

	[SerializeField]
    AnimationCurve OsyouY;
    [SerializeField]
    Vector3 StartPos;
	
	[SerializeField]
	float CurveWait = 0.0f;
	[SerializeField]
	float CurveSpeed = 0.1f;
	[SerializeField]
	float JumpValue = 0.1f;
	// Use this for initialization
	void Start () {
        StartPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		CurveWait = Mathf.Min(1.0f,CurveSpeed + CurveWait);
        Vector3 pos = StartPos;
		pos.y += OsyouY.Evaluate (CurveWait) * JumpValue;
		transform.localPosition = pos;
	}
}
