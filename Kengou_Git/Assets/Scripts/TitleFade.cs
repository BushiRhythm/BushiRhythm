using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleFade : MonoBehaviour {

	Vector3 DefaultScale;

	[SerializeField]
	AnimationCurve scale;

	[SerializeField]
	AnimationCurve alpha;

	[SerializeField]
	SpriteRenderer image;

	float time = .0f;

	bool IsRun = false;

	bool isEnd = false;
	// Use this for initialization
	void Start () {
		DefaultScale = transform .localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if(IsRun)
		{
			time += Time .unscaledDeltaTime;
			
		}
	
		Color col = image.color;

		col .a = alpha .Evaluate( time );
		image .color = col;
		transform .localScale = DefaultScale * scale .Evaluate( time );

		if(scale.Evaluate(time) == 1.0f && alpha .Evaluate( time ) == 1.0f)
		{
			isEnd = true;
		}
	}

	public void Run()
	{
		IsRun = true;
	}

	public bool IsEnd()
	{
		return isEnd;
	}
}
