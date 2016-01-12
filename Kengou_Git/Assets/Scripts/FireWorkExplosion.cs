using UnityEngine;
using System.Collections;

public class FireWorkExplosion : MonoBehaviour {

	float time;

	StaticScript _staticScript;

	StaticScript staticScript
	{
		get
		{
			if (!_staticScript)
				_staticScript = GameObject .FindGameObjectWithTag( "StaticScript" ) .GetComponent<StaticScript>();
			return _staticScript;
		}
	}

	[SerializeField]
	AnimationCurve ScaleCurve;

	[SerializeField]
	AnimationCurve AlphaCurve;

	[SerializeField]
	SpriteRenderer renderer;

	Vector3 DefaultScale;

	bool isEnd;

	public bool IsEnd
	{
		get
		{
			return isEnd;
		}
	}


	// Use this for initialization
	void Start () {

		DefaultScale = transform .localScale;
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.forward = (transform.position - Camera.main.transform.position).normalized;


		time += Time .deltaTime;
		Color col = renderer .color;
		col .a = AlphaCurve .Evaluate( time );
		renderer .color = col;
		transform .localScale = DefaultScale * ScaleCurve .Evaluate( time );
		if(renderer .color.a <= .0f)
		{
			if (!isEnd)
			{
				isEnd = true;
				staticScript .goalstarter .FireCountUp();
			}
		}
	}
}
