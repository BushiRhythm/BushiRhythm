using UnityEngine;
using System.Collections;

public class QuadScaler : MonoBehaviour {
	[SerializeField]
	float AnchorMin;

	[SerializeField]
	float AnchorMax;

	enum ASPECT
	{
		WidthOnly,
		HeightOnly,
	};

	[SerializeField]
	ASPECT AspectSetting;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//
	}
}
