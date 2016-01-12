using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultGuideEffecter : MonoBehaviour {


	float time;

	Vector3 LocalScale;

	[SerializeField]
	AnimationCurve ScaleCurve;

	[SerializeField]
	AnimationCurve AlphaCurve;

	[SerializeField]
	SpriteRenderer spriteRenderer;

	public void SetSprite(Sprite sprite)
	{
		spriteRenderer .sprite = sprite;
	}



	// Use this for initialization
	void Start () {
		LocalScale = transform .localScale;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos =transform.position ;
		time += Time.unscaledDeltaTime;
		float alpha = AlphaCurve.Evaluate(time);
		float scale = ScaleCurve .Evaluate( time );
		transform .localScale = LocalScale * scale * scale;
		if(alpha <= .0f)
		{
			Destroy(gameObject);
			return;
		}
		Color col =spriteRenderer.color;
		col.a = alpha;
		spriteRenderer .color = col;
	}
}
