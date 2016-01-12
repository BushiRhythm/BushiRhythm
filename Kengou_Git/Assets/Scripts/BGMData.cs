using UnityEngine;
using System.Collections;

public class BGMData : MonoBehaviour {

    public int Tempo = 120;
    public float OffSetTime = .0f;

	float oneRhythmTime;

	public float OneRhythmTime
	{
		get
		{
			return oneRhythmTime;
		}
	}

	float WorkSpeed;

    float _markerOffSetTime; //発射されてから弾がプレイヤーに到達するまでの実時間

	float DefaultVolume;

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


	public float MarkerOffSetTime
	{
		get
		{
			return _markerOffSetTime;
		}
	}

    float _defaultAnimSpeed; //アニメーターで実際に倍速をかける量

	public float DefaultAnimSpeed
	{
		get
		{
			return _defaultAnimSpeed;
		}
	}

    bool IsPlay = false;

	bool IsPlayAction = false;

    Animation _animation;

    //[SerializeField]
    //Animation _backAnimation;

    AudioSource audioSource;

	CriAtomSource criAtomSource;

	CriAtomExPlayback playback;

    public float BGMTime
    {
		get
		{

			if (audioSource)
			{
				#if UNITY_ANDROID
				return audioSource.time;
				#endif
				return audioSource.time;
		
			}

			if (criAtomSource)
			{
				long playedSamples;
				int SamplingRate;
				if(playback .GetNumPlayedSamples(out  playedSamples ,out SamplingRate ))
				{
					return playedSamples / (float)SamplingRate;
				}
				return criAtomSource .time / 1000.0f;
			}


			return .0f;
		}
    }

	public bool IsPlayEnd
	{
		get
		{
			if(audioSource) 
				return !audioSource .isPlaying && IsPlay;

			if (criAtomSource)
				return criAtomSource .status != CriAtomSource.Status.Playing && IsPlay;

			return false;
		}
	}

	public bool SourceIsPlay
	{
		get
		{
			if (audioSource)
				return audioSource .isPlaying;

			if (criAtomSource)
				return criAtomSource .status == CriAtomSource .Status .Playing;

			return false;
		}
	}

	public float BGMLength
	{
		get
		{
			if (audioSource)
			{
				#if UNITY_ANDROID
				return audioSource .clip .length;
				#endif
				return audioSource .clip.length;
			}

			if (criAtomSource)
			{
				 CriAtomExAcb acb;
				 CriAtomEx.CueInfo cueInfo;
				 acb = CriAtom.GetAcb (criAtomSource.cueSheet);
				 if (acb .GetCueInfo( criAtomSource .cueName , out cueInfo ))
				 {
					 return cueInfo .length / 1000.0f;
				 }
			}
				return .0f;
		}
	}

	public void MulSpeed(float speed)
	{
		WorkSpeed *= speed;
	}

	public float GetSpeed( )
	{
		if (audioSource) 
			return audioSource .pitch;
		
		if (criAtomSource)
			return criAtomSource .pitch;

		return .0f;
	}

    RhythmManager _rhythmManager;

    RhythmManager rhythmManager
    {
        get
        {
            if (!_rhythmManager)
                _rhythmManager = Object.FindObjectOfType<RhythmManager>();
            return _rhythmManager;
        }
    }

    AnimationState _mainState;
    //AnimationState _backMusicState;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
		criAtomSource = GetComponent<CriAtomSource>();
        _animation = GetComponent<Animation>();
		if (audioSource) 
			DefaultVolume = audioSource .volume;
		if (criAtomSource)
			DefaultVolume = criAtomSource .volume;
        _defaultAnimSpeed = Tempo / 60.0f;
        _markerOffSetTime = 60.0f / Tempo * 2;
		oneRhythmTime = 60.0f / Tempo;
        _mainState = _animation[gameObject.name];
        //_backMusicState = _backAnimation["BackMusic"];
        //_backMusicState.speed = DefaultAnimSpeed;
        //_mainState.speed = _defaultAnimSpeed;

        GetComponent<MarkerEmitter>().SetTime = _markerOffSetTime;
	}




    void MusicPlay()
    {
		if (SmoothChecker.instance.AvgTime < 0.05f)
		{
			if (audioSource)
				audioSource.Play();
			if (criAtomSource)
				playback = criAtomSource .Play();

			IsPlay = true;
			IsPlayAction = true;

		}
		//StartCoroutine( "AnimationPlay" );
		//AnimationPlay();
		//Invoke( "AnimationPlay" , OffSetTime );
    }

    IEnumerator AnimationPlay()
    {
        //string name = _mainState.name;
		while(true)
		{
			if (rhythmManager.DynamicRhythm.Time > -2.0f)
			{
				_animation.Play(gameObject.name);
				//_backAnimation.Play(_backMusicState.name);
				break;
			}
			else
			{
				yield return null;
			}

		}
		yield break;
        //Animator ani;
        //ani.GetCurrentAnimatorStateInfo(0).
    }
	
	// Update is called once per frame
	void Update () {

		if (audioSource)
			audioSource .pitch = WorkSpeed;
		if (criAtomSource)
			criAtomSource .pitch = 1200.0f * Mathf .Log( WorkSpeed ) / Mathf .Log( 2.0f );

		WorkSpeed = 1.0f;

        //Debug.Log(audioSource.time);
		if (!SourceIsPlay && !IsPlayAction)
        {
			MusicPlay();


            
        }

         //時間微分と時間差を考慮してタイムスケールを設定する
		if (SourceIsPlay)
        {
			float VolumeMultiple = 1.0f;

            float DeltaTime = Time.unscaledDeltaTime;

            float DynamicTime = rhythmManager.DynamicRhythm.Time;

			if(staticScript.rhythmManager.IsSlow)
			{
				VolumeMultiple = 0.5f;
			}
			VolumeMultiple *= staticScript .goalstarter .UIAlpha;
			if (audioSource)
				audioSource .volume = VolumeMultiple * DefaultVolume;
			if (criAtomSource)
				criAtomSource .volume = VolumeMultiple * DefaultVolume;
            //float CurAnimTime = (_mainState.time  - 2.0f) / _defaultAnimSpeed;

           // float FixedCurAnimTime = (_backMusicState.time - 2.0f) / DefaultAnimSpeed;

            //アニメの速度が1ではないので戻してやる

			//CurAnimTime -= OffSetTime / _defaultAnimSpeed;

			//FixedCurAnimTime -= OffSetTime / DefaultAnimSpeed;


            //時間差　
            //float SubTime = DynamicTime - CurAnimTime;

            //時間差　
            //float FixedSubTime = FixedTime - FixedCurAnimTime;

            //タイムスケールに代入

           // float TimeScale = SubTime / DeltaTime;

            //float FixedTimeScale = FixedSubTime / DeltaTime;

            //TimeScale = Mathf.Clamp(TimeScale, .1f, 10.0f);

            //FixedTimeScale = Mathf.Clamp(FixedTimeScale, .1f, 10.0f);


            //_state.speed = DefaultAnimSpeed * TimeScale;

            //_mainState.speed = _defaultAnimSpeed * TimeScale;
            //_backMusicState.speed = DefaultAnimSpeed * FixedTimeScale;


           // Debug.Log("TimeScale = " + Time.timeScale.ToString());

        }



	}
}
