using UnityEngine;
using System.Collections;

public class GameOverStarter : MonoBehaviour {

	StaticScript _staticScript;

	protected StaticScript staticScript
	{
		get
		{
			if (!_staticScript)
				_staticScript = GameObject .FindGameObjectWithTag( "StaticScript" ) .GetComponent<StaticScript>();
			return _staticScript;
		}
	}

	[SerializeField]
	AnimationCurve MusicSpeedCurve;

	[SerializeField]
	AnimationCurve BlurCurve;

	[SerializeField]
	AnimationCurve AlphaCurve;

	[SerializeField]
	AnimationCurve RedCurve;

	[SerializeField]
	AudioSource se;

	[SerializeField]
	DynamicRenderTexture dynamicRenderTexture;


	bool IsRun = false;

	bool IsPrinted = false;

	bool IsSwitch = false;

	float time;

	public bool IsRuned
	{
		get
		{
			return IsRun;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (staticScript .goalstarter .IsRuned)
			return;

		if (staticScript .timeOverStarter .IsRuned)
			return;

		if (!IsSwitch)
		{
			staticScript .gameOverSystem .transform .position = Camera .main .transform .position;
		}
		if(!IsRun)
		{
			if (staticScript .bulletResultManager .IsGameOver)
			{
				staticScript .actionTymingManager .Disable();
				staticScript .touchPos .gameObject .SetActive( false );
				IsRun = true;
			}
		}
		else //if(IsRun)
		{
			
			time += Time .unscaledDeltaTime;
			float MusicSpeed = MusicSpeedCurve .Evaluate( time );

			staticScript .blur .enabled = true;
			staticScript .blur .blurSpread = BlurCurve .Evaluate( time );

			Color col = new Color(1.0f,.0f,.0f,.0f);
			col.a = RedCurve.Evaluate( time );
			staticScript .colorScreen .color = col;


			float Alpha = AlphaCurve.Evaluate( time );
			foreach(CanvasRenderer renderer in staticScript.uICollection.renderers )
			{
				renderer .SetAlpha( renderer .GetAlpha() * Alpha );
			}

			staticScript .bgmData .MulSpeed( MusicSpeed );


			if(MusicSpeed <= .0f)
			{
				if (!IsSwitch && IsPrinted)
				{
					staticScript .fulScreenMesh .CreateMesh();
					staticScript. fulScreenMesh .SetTexture( dynamicRenderTexture .renderTexture );
					staticScript .markerCanvas .gameObject .SetActive( false );
					staticScript .gameOverSystem .Run();
					staticScript .uiCanvas.gameObject.SetActive(false);
					Camera .main .enabled = false;
					staticScript .gameOverSystem .MyCamera .enabled = true;
					se .Play();
					IsSwitch = true;
				}
				if (!IsPrinted)
				{
					dynamicRenderTexture .Print();
					IsPrinted = true;
				}
		}


		}
	}
}
