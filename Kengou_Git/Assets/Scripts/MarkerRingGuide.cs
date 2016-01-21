using UnityEngine;
using UnityEngine .UI;
using System.Collections;

public class MarkerRingGuide : MonoBehaviour {

	[SerializeField]
	Marker marker;

	[SerializeField]
	Image image;

	[SerializeField]
	Renderer renderer;

	[SerializeField]
	AnimationCurve Red;

	[SerializeField]
	AnimationCurve Blue;

	[SerializeField]
	AnimationCurve Green;

	[SerializeField]
	AnimationCurve Alpha;

	[SerializeField]
	float DefaultScale;

	[SerializeField]
	float GoalScale;

	// Use this for initialization
	void Start () {
		//mat .CopyPropertiesFromMaterial( image .material );
		//	image .material = new Material( mat );
		Update();
		//renderer .material .color =  new Color(1.0f,.0f,1.0f,1.0f);
		//renderer .material .SetFloat( "_CenterValue" , 0.8f);
	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.color = new Color(Red.Evaluate(marker.Progress), Green.Evaluate(marker.Progress), Blue.Evaluate(marker.Progress), Alpha.Evaluate(marker.Progress));
		renderer .material .SetFloat( "_CenterValue" , GoalScale + (DefaultScale - GoalScale) * ( 1.0f - marker .Progress ) );

	
	}
}
