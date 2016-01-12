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
	// Use this for initialization
	void Start () {
		//mat .CopyPropertiesFromMaterial( image .material );
	//	image .material = new Material( mat );
		renderer .material .SetColor( "Color" ,new Color(1.0f,.0f,.0f,1.0f) );
		renderer .material .SetFloat( "_CenterValue" , 0.8f);
	}
	
	// Update is called once per frame
	void Update () {
		renderer .material .SetFloat( "_CenterValue" , 0.3f + 0.6f * ( 1.0f - marker .Progress ) );

	
	}
}
