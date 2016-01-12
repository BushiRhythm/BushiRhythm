using UnityEngine;
using UnityEngine .UI;
using System.Collections;

public class TitleRhythm : MonoBehaviour {

	Vector3 DefaultScale;

	[SerializeField]
	AnimationCurve scale;


	float time = .0f;
	void Start()
	{
		DefaultScale = transform .localScale;
	}

	// Update is called once per frame
	void Update()
	{

		time += Time .unscaledDeltaTime;

		transform .localScale = DefaultScale * scale .Evaluate( time );

	}

}
