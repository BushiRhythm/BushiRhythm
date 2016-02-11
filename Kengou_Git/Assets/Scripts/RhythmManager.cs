using UnityEngine;
using System.Collections;

public struct Rhythm   //時間軸が3種類あるので構造体でまとめる
{
    private float _Pos; //現在位置
    private float _DeltaTime; //時間微分
	private float _DeltaPos;
    private float _Time;
    private bool _Tyming;
	private int _LastTyming;
	private float _Progress;
    public float Pos
    {
        set //時間微分が更新されます
        {
            float LenPos = value - _Pos;
			_DeltaTime = LenPos * RhythmManager .OneRyhthmTime;
			_DeltaPos = LenPos;
			if(_LastTyming < (int)value && _Pos != (int)value)
			{
				_LastTyming = (int)value;
				_Tyming = true;
			}
			else
			{
				_Tyming = false;
			}
            _Pos = value;
            _Time = value * RhythmManager.OneRyhthmTime;
			_Progress = _Pos - (int)_Pos;
           
        }
        get
        { return _Pos; }
    }

	public void Init(float pos)
	{
		_LastTyming = -999;
		_Pos = -999;
		Pos = pos;
		_Tyming = false;

	}

    public float Time
    {
        get
        {
            return _Time;
        }
    }

    public bool Tyming
    {
        get
        {
            return _Tyming;
        }
    }

	public float DeltaPos
	{
		get
		{
			return _DeltaPos;
		}

	}
 

    public float DeltaTime
    {
        get
        {
            return _DeltaTime;
        }
    }

	public float Progress
	{
		get
		{
			return _Progress;
		}
	}
}

public class RhythmManager : MonoBehaviour {

	bool Init = false;

	bool Stoped = false;

	bool ForceStoped = false;

	float StopTime;

	[SerializeField]
	AnimationCurve StopCurve;

	[SerializeField]
	SE taiko;

	[SerializeField]
	SE huti;

	public void Stop()
	{
		Stoped = true;
		StopTime = .0f;
	}

	public void ForceStop()
	{
		ForceStoped = true;
	}

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

    public static float OneRyhthmTime
    {
        get
        {
           return (60.0f / RhythmManager.Tempo);
        }
    }

    public static int Tempo;

    [SerializeField]
    int SlowTempo = 2;	//何拍子を一拍子とするか

    Rhythm _FixedRhythm; //固定時間軸拍子

    public Rhythm FixedRhythm
    {
        get
        {
            return _FixedRhythm;
        }
    }

	Rhythm _ReadyFixedRhythm; //固定時間軸拍子

	public Rhythm ReadyFixedRhythm
	{
		get
		{
			return _ReadyFixedRhythm;
		}
	}




    Rhythm _DynamicRhythm; //可変時間軸拍子

    public Rhythm DynamicRhythm
    {
        get
        {
            return _DynamicRhythm;
        }
    }


    private Rhythm _SlowRhythm; //スロー時間軸拍子

    public Rhythm SlowRhythm
    {
        get
        {
            return _SlowRhythm;
        }
    }

    public float OnTempoTime
    {
        get
        {
            return 60.0f/ staticScript.bgmData.Tempo;
        }
    }

    public float SlowProgress
    {
        get
        {
            
             return (_FixedRhythm.Pos - SlowStartPos) /   (float)(SlowEndPos - SlowStartPos);
        }

    }


    //特殊な変数
    private int DynamicRhythmDelay; //可変時間軸遅延数
    private int SlowStartPos;   //スロー時間軸開始位置
    private int SlowEndPos;   //スロー時間軸終了位置
    private bool _IsSlow; //スロー発動中か否か

    public bool IsSlow
    {
        get
        {
            return _IsSlow;
        }
    }


    public void Slow(int Rhythm)// Rhythm拍子発動させる
    {
        if (_IsSlow)
            return;

       SlowStartPos = (int)(_FixedRhythm.Pos + 0.2f);

       SlowEndPos = SlowStartPos + Rhythm;

       staticScript.playerSE.Slow();

       _IsSlow = true;
    }



	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Tempo = staticScript.bgmData.Tempo;
		int OffSetRhythm =(int)( staticScript .bgmData .OffSetTime / OneRyhthmTime );
		if(!Init)
		{
			_DynamicRhythm .Init( -staticScript .bgmData .OffSetTime / OneRyhthmTime );
			_FixedRhythm .Init( -staticScript .bgmData .OffSetTime / OneRyhthmTime );
			_ReadyFixedRhythm .Init( _FixedRhythm .Pos + ( OffSetRhythm + 2 ) );
			Init = true;
		}
		//音楽の位置から固定時間軸拍子の位置を設定
		float bgmTime = staticScript .bgmData .BGMTime;

		if (ForceStoped)
		{
			_FixedRhythm .Pos = _FixedRhythm .Pos ;
		}
		else if (Stoped)
		{
			StopTime += Time .unscaledDeltaTime;
			_FixedRhythm .Pos = _FixedRhythm .Pos + ( Time .unscaledDeltaTime * staticScript .bgmData .GetSpeed() / OneRyhthmTime ) * StopCurve .Evaluate( StopTime );

		}
		else if(staticScript.bgmData.IsPlayEnd)
		{
			_FixedRhythm .Pos = _FixedRhythm .Pos + (Time .unscaledDeltaTime * staticScript.bgmData.GetSpeed() / OneRyhthmTime);
		}
		else
		{
			_FixedRhythm .Pos = ( bgmTime - staticScript .bgmData .OffSetTime ) / OneRyhthmTime;
		}
		_ReadyFixedRhythm .Pos = _FixedRhythm .Pos + ( OffSetRhythm + 2 );

		if (_FixedRhythm .Tyming && _IsSlow)
		{
			int Rest = SlowEndPos - (int)_FixedRhythm .Pos;

			if(Rest == 0)
			{
				Instantiate( taiko );
			}
			else if(Rest <= 3)
			{
				Instantiate( huti );
			}

		}
        // スロー終了
        if (_IsSlow && SlowEndPos < _FixedRhythm.Pos)
        {
            _IsSlow = false;

            //ずれた分を遅延に加算
			DynamicRhythmDelay += ( SlowEndPos - SlowStartPos ) - (int)( ( SlowEndPos - SlowStartPos ) / SlowTempo );
        }
        if (_IsSlow)
        {
			

            //スロー時間軸位置を開始位置と固定時間軸拍子の位置から設定
            _SlowRhythm.Pos = _FixedRhythm.Pos - SlowStartPos;

            //可変拍子位を開始位置と固定時間軸拍子の位置から設定
			_DynamicRhythm .Pos = ( ( SlowStartPos - DynamicRhythmDelay ) + _SlowRhythm .Pos / SlowTempo );
        }
        else
        {
            //固定時間軸拍子の位置から可変拍子位置を設定
            _DynamicRhythm.Pos = (_FixedRhythm.Pos - DynamicRhythmDelay);
        }

		//Time .timeScale = _DynamicRhythm .DeltaTime;
		//if (Time .timeScale < 0.1f)
		//{
		//	Time .timeScale = 0.1f;
		//}

        

//#if UNITY_EDITOR
//		staticScript .debugCode .AddText( "DynamicPos : " + _DynamicRhythm .Pos );
//#endif
	
	}
}
