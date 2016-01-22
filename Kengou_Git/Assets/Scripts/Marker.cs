using UnityEngine;
using System.Collections;


public class Marker : MonoBehaviour {

    const float TimeFromOneFrame = 1 / 60.0f;

	[SerializeField]
	float SizeRate = 0.1f;

	[SerializeField]
	public Transform MarkerBullet;

	[SerializeField]
	AnimationCurve Red;

	[SerializeField]
	AnimationCurve Blue;

	[SerializeField]
	AnimationCurve Green;

	[SerializeField]
	AnimationCurve Alpha;

	[SerializeField]
	GameObject WhiteRing;

	Material WhiteRingMat;

	public GameObject MarkerCanvas;

    public MarkerFirePoint markerFirePoint;

	public Enemy HavingEnemy;

    private FlickManager _flickManager;

    private Health _health;

    private RhythmManager _rhythmManager;

    bool IsDestroy = false;

	bool IsLooping = false;

	bool IsCanSwordAttack = true;

	public int StartRhythm;

	public bool IsCanCounter;

	private int LandingRhythm;

	JudgeBar NormalJudgeBar;

	JudgeBar SlowJudgeBar;

	public void LoopSetting()
	{
		IsLooping = true;
	}

	public void IsCanSwordAttackEneble()
	{
		IsCanSwordAttack = true;
	}

	public void IsCanSwordAttackDisable()
	{
		IsCanSwordAttack = false;
	}
    
    public Health health
    {
        get
        {
            if (!_health)
                _health = FindObjectOfType<Health>();
            return _health;
        }

    }

	Collider _collider;

	Collider collider
	{
		get{
			if(!_collider)
			{
				_collider = GetComponent<Collider>();
			}
			return _collider;
		}
	}

	public bool RayCastHit(Ray ray , out RaycastHit hitInfo , float maxDistance )
	{
		return collider .Raycast( ray , out hitInfo , maxDistance );
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

	CanvasRenderer _canvasRenderer;

	CanvasRenderer canvasRenderer
	{
		get
		{
			if (!_canvasRenderer)
				_canvasRenderer = GetComponent<CanvasRenderer>();
			return _canvasRenderer;
		}
	}


    public Vector2 PosOnScreen;
    float _RestTime;   //発射からベストタイミングまでの残り時間
    int _JudgeIndex;     //判定用のインデックス
	int _SlowJudgeIndex;     //判定用のインデックス スロー
    float _MaxTime;     //設定時の時間

	public int JudgeIndex
	{
		get
		{
			return _JudgeIndex;
		}
	}

	public int SlowJudgeIndex
	{
		get
		{
			return _SlowJudgeIndex;
		}
	}
	

    public float RestTime
    {
        get
        {
            return _RestTime;
        }
    }

    public float Progress //0.0～1.0まで表示
    {
        get
        {
            return 1.0f - _RestTime / _MaxTime;
        }
    }

	public bool IsSwordTyming	//切れるタイミングか
	{
		get
		{
			return Mathf .Abs( LandingRhythm - ( staticScript .rhythmManager .DynamicRhythm .Pos - StartRhythm ) ) < 0.5f && IsCanSwordAttack;

		}
	}
    public void DestroyRequest()
    {
		if (_JudgeIndex < staticScript .bulletResultManager .NormalJudgeBar .NumJudge && IsCanSwordAttack)
        {
            //BulletDestroy();

            IsDestroy = true;
        }
    }
	// Use this for initialization
	void Start () {
		if(WhiteRing != null)
		{
			MeshRenderer WhiteRingRenderer = WhiteRing.GetComponent<MeshRenderer>();
			WhiteRingMat = WhiteRingRenderer.materials[0];
		}

		NormalJudgeBar = staticScript .bulletResultManager .NormalJudgeBar;
		SlowJudgeBar = staticScript .bulletResultManager .SlowJudgeBar;
		_JudgeIndex = NormalJudgeBar .NumJudge;
		_SlowJudgeIndex = SlowJudgeBar .NumJudge;

		StartRhythm = (int)staticScript .rhythmManager .DynamicRhythm .Pos;
	}

    public void SetTime(float value)
    {
        _RestTime = value;
        _MaxTime = value;
    }

	public void SetLandingRhythm( int value )
	{
		LandingRhythm = value;
		_RestTime = value * staticScript .rhythmManager .OnTempoTime;
		_MaxTime = _RestTime;
	}

    public void SettingPos()//大きさも調整
    { 
        Vector2 size = MarkerCanvas.GetComponent<RectTransform>().sizeDelta;
		//まず大きさの設定

		float ReqSize = size .y * SizeRate;

		float ReqScale = ReqSize / GetComponent<RectTransform>() .sizeDelta .y ;

		transform .localScale = new Vector3(ReqScale,ReqScale,ReqScale);
        //AdjustScreenを使って移動させる
        Vector3 pos;
		//正方形でとるために変更
        pos.x = (size.y / 2) * PosOnScreen.x;
        pos.y = (size.y / 2) * PosOnScreen.y;
        pos.z = .0f;

        this.transform.localPosition = pos;
        //Debug.Log("位置設定");
    }

    protected void BulletDestroy()
    {
		if (HavingEnemy)
			HavingEnemy .MarkerDestroyAction();

		staticScript .bulletResultManager .Push_Result( this , MarkerBullet .position , false , IsCanCounter );


        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		_RestTime =( LandingRhythm - ( staticScript .rhythmManager .DynamicRhythm .Pos - StartRhythm ) )* staticScript .rhythmManager .OnTempoTime;

		Color col = staticScript .markerColorManager .GetColor( StartRhythm );

		//canvasRenderer .SetColor( new Color( col .r , col .g , col.b , Alpha .Evaluate( Progress ) ) );
		if(WhiteRingMat!= null)
			WhiteRingMat.color =  new Color(col.r, col.g, col.b, Alpha.Evaluate(Progress));
        SettingPos();
		_JudgeIndex = NormalJudgeBar .Judge( _JudgeIndex , _RestTime );
		_SlowJudgeIndex = SlowJudgeBar .Judge( _SlowJudgeIndex , _RestTime );
        if (_JudgeIndex  == 0)
        {
			//ループならリセット
			if(IsLooping)
			{
				_JudgeIndex = NormalJudgeBar .NumJudge;
				_SlowJudgeIndex = SlowJudgeBar .NumJudge;

				StartRhythm = (int)staticScript .rhythmManager .DynamicRhythm .Pos;

				SetLandingRhythm( 1 );
			}
			else
			{
				staticScript .bulletResultManager .Push_Result( this , MarkerBullet .position ,true ,false);

				health.Damage(1);
				if (health.IsDeath)
					staticScript .bulletResultManager .GameOver();
				Destroy(this.gameObject);
    
			}
    }
        if(IsDestroy)
            BulletDestroy();
        //if (JudgeIndex < judgeBar.NumJudge)
        //{
        //   if (actionTymingManager.IsBladeActiond())
        //  {
              
        //      BulletDestroy();
        //  }
        //}


	}



    void OnTriggerStay(Collider other)
    {
		if (staticScript.actionTymingManager .IsBladeActiond())
        {
			if (_JudgeIndex < NormalJudgeBar .NumJudge && IsCanSwordAttack)
            {
                //BulletDestroy();

                IsDestroy = true;
            }

        }

    }
}
