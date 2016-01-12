using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwordEffectAnimation : MonoBehaviour {

	int ImageIndex = 0;

	float WorkTime = .0f;

	[SerializeField]
	AnimationCurve AlphaCurve;

	SpriteRenderer _spriteRenderer;

	SpriteRenderer spriteRenderer
	{
		get
		{
			if(!_spriteRenderer)
			{
				_spriteRenderer = GetComponent<SpriteRenderer>();
			}
			return _spriteRenderer;
		}
	}

	[SerializeField]
	Sprite[] spriteAnimation;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {	
		spriteRenderer .sprite = spriteAnimation[ImageIndex];
		if(spriteAnimation.Length - 1 > ImageIndex)
		{
			ImageIndex++;		
		}
		Color color = spriteRenderer .color;
		color .a = AlphaCurve .Evaluate( WorkTime );
		spriteRenderer .color = color;
		WorkTime += Time .deltaTime;
	}

	public bool IsAnimationEnd
	{
		get
		{
			return (WorkTime != .0f &&spriteRenderer .color.a <= .0f);
			
		}
	}

	public void AnimationStart()
	{
		ImageIndex = 0;
		WorkTime = .0f;

		spriteRenderer .sprite = spriteAnimation[ImageIndex];
		Color color = spriteRenderer .color;
		color .a = AlphaCurve .Evaluate( WorkTime );
		spriteRenderer .color = color;
	}
}
