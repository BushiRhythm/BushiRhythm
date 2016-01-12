using UnityEngine;
using System.Collections;

public class Firework : MonoBehaviour {


	int ImageIndex = 0;

	float WorkTime = .0f;

	[SerializeField]
	AnimationCurve AlphaCurve;

	[SerializeField]
	SpriteRenderer spriteRenderer;

	[SerializeField]
	Sprite[] spriteAnimation;



	// Use this for initialization
	void Start()
	{
		spriteRenderer .sprite = spriteAnimation[ImageIndex];
	}

	// Update is called once per frame
	void Update()
	{
		spriteRenderer .sprite = spriteAnimation[ImageIndex];
		if (spriteAnimation .Length - 1 > ImageIndex)
		{
			ImageIndex++;
		}
		else
		{
			WorkTime += Time .deltaTime;
		}
		Color color = spriteRenderer .color;
		color .a = AlphaCurve .Evaluate( WorkTime );
		if(color.a <= .0f)
		{
			Destroy( this .gameObject );
			return;
		}
		spriteRenderer .color = color;

	}


}
