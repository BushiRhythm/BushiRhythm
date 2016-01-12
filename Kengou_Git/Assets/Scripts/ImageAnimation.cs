using UnityEngine;
using System.Collections;

public class ImageAnimation : MonoBehaviour {

	int index = 0;
	[SerializeField]
	Sprite[] images;

	[SerializeField]
	SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		renderer .sprite = images[0];
	}
	
	// Update is called once per frame
	void Update () {
		index++;
		if (images .Length <= index)
			Destroy( this .gameObject );

		renderer .sprite = images[index];
	}
}
