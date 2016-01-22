using UnityEngine;
using System.Collections;

public class TitleSystem : MonoBehaviour {

	[SerializeField]
	TitleFade titleFade;

	[SerializeField]
	TitleFade flickFade;

	[SerializeField]
	AudioSource audioSource;

	[SerializeField]
	AudioSource taiko;

	[SerializeField]
	AudioSource BeginSE;


	[SerializeField]
	TitleRhythm titlerhythm;


	[SerializeField]
	FulScreenMesh fullScreenMesh;

	[SerializeField]
	SwordEffectAnimation swordEffectAnimation;


	[SerializeField]
	DynamicRenderTexture dynamicRenderTexture;

	[SerializeField]
	MeshCutter meshCutter;

	[SerializeField]
	GameObject WorldCanvas;

	Vector3 ConvertStart;
	Vector3 ConvertEnd;
	Vector3 ConvertForward;

	FlickManager _flickManager;

	StageSelectStarter _stageSelectStarter;


	StageSelectStarter stageSelectStarter
	{
		get
		{
			if (!_stageSelectStarter)
			{
				_stageSelectStarter = FindObjectOfType<StageSelectStarter>();
			}
			return _stageSelectStarter;
		}
	}




	[SerializeField]
	AnimationCurve MoveCurve;

	float moveSpeed;

	bool Cuted;

	AsyncOperation LoadData = new AsyncOperation();



    public float MoveSpeed
	{
		get
		{
			return moveSpeed;
		}
	}


	FlickManager flickManager
	{
		get
		{
			if (!_flickManager)
			{
				_flickManager = FindObjectOfType<FlickManager>();
			}
			return _flickManager;
		}
	}

	float time;

	enum MainState
	{
		wait,
		titlefade ,
		begin,
		printed,
		cut,
	};

	MainState state;


	// Use this for initialization
	void Start () {
        LoadData = Application.LoadLevelAdditiveAsync("StageSelect");
        LoadData.priority = 100;
        LoadData .allowSceneActivation = true;
		flickManager .IsFlick();
#if UNITY_ANDROID
		float screenRate = (float)480 / Screen .height;
		if (screenRate > 1)
			screenRate = 1;
		int width = (int)( Screen .width * screenRate );
		int height = (int)( Screen .height * screenRate );
		Screen .SetResolution( width , height , false );
#endif
	}
	
	// Update is called once per frame
	void Update () {
		switch(state)
		{
			case MainState.wait:
			if (SmoothChecker .instance .AvgTime < 0.05f && LoadData .isDone)
			{
				titleFade .Run();
				BeginSE .Play();
				titlerhythm .enabled = false;
				state++;			
			}
			break;

			case MainState.titlefade:
			if (titleFade.IsEnd())
			{
				BeginSE .Stop();
				titleFade .enabled = false;
				flickFade .Run();
				titlerhythm .enabled = true;
				taiko .Play();
                state++;
			}
			break;

			case MainState.begin:
            time += Time.unscaledDeltaTime;
#if UNITY_ANDROID
			if(time > 15.0f)
            {
				Handheld.PlayFullScreenMovie("demo.mp4",Color.black,FullScreenMovieControlMode.CancelOnInput,FullScreenMovieScalingMode.AspectFill);
				Application.LoadLevel("NewTitle");
            }
#endif
            if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				dynamicRenderTexture .Print();
                time = .0f;
				state++;
			}
			break;

			case MainState .printed:
			if (flickManager .IsEndFlick())
			{
				Camera .main .transform .position = stageSelectStarter .NextCamera.transform.position;
				Camera .main .transform .rotation = stageSelectStarter .NextCamera .transform .rotation;

				Vector2 FlickStartPos = flickManager .FlickStartPos;

				FlickStartPos .x /= Screen .width;
				FlickStartPos .y /= Screen .height;


				Vector2 FlickEndPos = flickManager .FlickEndPos;

				FlickEndPos .x /= Screen .width;
				FlickEndPos .y /= Screen .height;

				ConvertStart = Camera .main .ViewportToWorldPoint( new Vector3( FlickStartPos .x , FlickStartPos .y , 4.0f ) );
				ConvertEnd = Camera .main .ViewportToWorldPoint( new Vector3( FlickEndPos .x , FlickEndPos .y , 4.0f ) );
				ConvertForward = Camera .main .ViewportToWorldPoint( new Vector3( FlickStartPos .x , FlickStartPos .y , 6.0f ) );


				stageSelectStarter .NextCanvas .worldCamera = Camera .main;
				fullScreenMesh .transform .position = Camera .main .transform .position + Camera .main .transform .forward * 20.0f;

				fullScreenMesh .CreateMesh();
				fullScreenMesh .SetTexture( dynamicRenderTexture .renderTexture );


				swordEffectAnimation .transform .right = (ConvertEnd - ConvertStart) .normalized;
				swordEffectAnimation .transform .position = ( ConvertEnd + ConvertStart ) / 2;
				swordEffectAnimation.AnimationStart();

				audioSource .Play();
				state++;
	
			}

			break;

			case MainState .cut:
				
				time += Time .unscaledDeltaTime;
				moveSpeed = MoveCurve .Evaluate( time ) * 20.0f;

				WorldCanvas .SetActive( false );


				//Vector2 vec0 = Random .insideUnitCircle;
				//Vector2 vec1 = Random .insideUnitCircle;
				if(!Cuted)
				{
					meshCutter .Cut( ConvertStart , ConvertEnd , ( ConvertForward - ConvertStart ) .normalized , Vector3.forward);
					Cuted = true;
				}

				if (flickManager .IsEndFlick())
				{
					Camera .main .transform .position = stageSelectStarter .NextCamera .transform .position;
					Camera .main .transform .rotation = stageSelectStarter .NextCamera .transform .rotation;

					Vector2 FlickStartPos = flickManager .FlickStartPos;

					FlickStartPos .x /= Screen .width;
					FlickStartPos .y /= Screen .height;


					Vector2 FlickEndPos = flickManager .FlickEndPos;

					FlickEndPos .x /= Screen .width;
					FlickEndPos .y /= Screen .height;

					ConvertStart = Camera .main .ViewportToWorldPoint( new Vector3( FlickStartPos .x , FlickStartPos .y , 4.0f ) );
					ConvertEnd = Camera .main .ViewportToWorldPoint( new Vector3( FlickEndPos .x , FlickEndPos .y , 4.0f ) );
					ConvertForward = Camera .main .ViewportToWorldPoint( new Vector3( FlickStartPos .x , FlickStartPos .y , 6.0f ) );

					meshCutter .Cut( ConvertStart , ConvertEnd , ( ConvertForward - ConvertStart ) .normalized , Vector3 .forward );

					swordEffectAnimation .transform .right = ( ConvertEnd - ConvertStart ) .normalized;
					swordEffectAnimation .transform .position = ( ConvertEnd + ConvertStart ) / 2;
					swordEffectAnimation .AnimationStart();

					audioSource .Play();

				}
			if(time > 1.4f)
			{
				stageSelectStarter .AllEnable();
				stageSelectStarter .NextCanvas .worldCamera = stageSelectStarter .NextCamera;
				state++;
				//LoadData .allowSceneActivation = true;
			}
		
			break;


		

		
		}
	}
}
