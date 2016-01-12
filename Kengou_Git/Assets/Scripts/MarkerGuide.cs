using UnityEngine;
using System.Collections;

public class MarkerGuide : MonoBehaviour {

	[SerializeField]
	Marker marker;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform .localScale = new Vector3( marker .Progress * marker .Progress , marker .Progress * marker .Progress , marker .Progress * marker .Progress );
	}
}
