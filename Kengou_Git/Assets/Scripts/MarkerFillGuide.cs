using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MarkerFillGuide : MonoBehaviour {


	[SerializeField]
	Marker marker;

	[SerializeField]
	Image image;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		float p = ( marker .Progress -0.5f) * 2.0f;
		if (p < .0f)
			p = .0f;
		else if (p > 1.0f)
			p = 1.0f;
		image .fillAmount = p;
	}
}
