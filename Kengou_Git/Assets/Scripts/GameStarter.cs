using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStarter : MonoBehaviour {

	bool IsInit = false;

	bool IsInit2 = false;

	bool IsRun = false;

	bool IsFadeOut = false;

	int StartRhythm = 0;

	float FadeOutTime = .0f;

	[SerializeField]
	CanvasRenderer[] renderers;

	[SerializeField]
	AnimationCurve RendererAlpha;

	[SerializeField]
	AnimationCurve FadeOutAlphaCurve;

	[SerializeField]
	AnimationCurve ImageScale;

	[SerializeField]
	Transform EmitPoint;

	[SerializeField]
	CanvasRenderer FadeOutScreen;

	[SerializeField]
	Sprite[] CountDownImages;

	[SerializeField]
	CountDownImage CDImage;

	[SerializeField]
	GameObject huti;

	[SerializeField]
	GameObject taiko;

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!IsInit2 )
		{
			StartRhythm = (int)( staticScript .bgmData .OffSetTime /  staticScript .bgmData .OneRhythmTime );
			IsInit2 = true;
		}
		int RestRhythm = StartRhythm + 2 - (int)staticScript .rhythmManager .ReadyFixedRhythm .Pos;

		if (staticScript .rhythmManager .FixedRhythm .Pos > -0.5f)
		{
			if (!IsInit)
			{
				staticScript .actionTymingManager .Enable();
				IsInit = true;
			}		
		}
		if (staticScript .rhythmManager .ReadyFixedRhythm .Tyming)
		{
			IsFadeOut = true;

			if (0 <= RestRhythm)
			{
				int index = RestRhythm;

				if(index < CountDownImages.Length)
				{
					GameObject tmp = Instantiate( CDImage .gameObject , EmitPoint .position , EmitPoint .rotation ) as GameObject;
					tmp .transform .parent = staticScript .uiCanvas .transform;
					CountDownImage emit = tmp.GetComponent<CountDownImage>();
					emit .DefaultScale = ImageScale .Evaluate( index );
					emit .image.sprite = CountDownImages[index];
					if(index == 0)
					{
						Instantiate( taiko );
						emit .MaxRhythm = 2;
					}
					else
					{
						if(index <= 3)
							Instantiate( huti );
						emit .RotateSpeed = Random .Range( -0.2f , 0.2f );
					}
						
				}

			}
		}
		if (IsFadeOut)
		{
			FadeOutTime += Time .deltaTime;
		}

		float FadeOutAlpha = FadeOutAlphaCurve .Evaluate( FadeOutTime );

		FadeOutScreen .SetAlpha( FadeOutAlpha );
		if (FadeOutAlpha == .0f)
			FadeOutScreen .gameObject .SetActive( false );

		if(!IsRun)
		{
			IsRun = true;
		}
		float alpha = .0f;
		alpha = RendererAlpha .Evaluate( staticScript .rhythmManager .FixedRhythm .Time );
		foreach (CanvasRenderer renderer in renderers)
		{
			renderer .SetAlpha( renderer .GetAlpha() * alpha );
		}
	}
}
