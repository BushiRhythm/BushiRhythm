using UnityEngine;
using UnityEngine .UI;
using System.Collections;

public class ResultGrade : MonoBehaviour {
	[SerializeField]
	Sprite[] images;

	[SerializeField]
	float Speed;

	float time;

	[SerializeField]
	AnimationCurve AlphaCurve;

	[SerializeField]
	public NumberSet score;

	[SerializeField]
	SpriteRenderer[] renderers;

	SpriteRenderer _spriteRenderer;

	SpriteRenderer spriteRenderer
	{
		get
		{
			if (!_spriteRenderer)
			{
				_spriteRenderer = GetComponent<SpriteRenderer>();
			}
			return _spriteRenderer;
		}
	}

	public void SetSprite(int i)
	{
		spriteRenderer .sprite = images[i];
	}

	public Sprite sprite
	{
		get
		{
			return spriteRenderer .sprite;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos =transform.position ;
		pos.y +=Speed * Time.unscaledDeltaTime;
		time += Time.unscaledDeltaTime;
		transform.position = pos;
		float alpha = AlphaCurve.Evaluate(time);

		if(alpha <= .0f)
		{
			Destroy(gameObject);
			return;
		}
		Color col =spriteRenderer.color;
		col.a = alpha;
		spriteRenderer .color = col;
		col .a *= 2.0f;
		foreach(SpriteRenderer renderer in renderers)
		{
			renderer .color = col;
		}
	}
}
