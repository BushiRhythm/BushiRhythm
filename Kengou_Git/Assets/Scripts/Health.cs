using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    [SerializeField]
    AnimationCurve AlphaCurve;

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


    [SerializeField, HeaderAttribute("何回目でGameOverか")]
    private int MAXHP = 3;
    [SerializeField]
    private int Stage1HP = 7;
    [SerializeField]
    private int Stage2HP = 5;
    [SerializeField]
    private int Stage3HP = 3;
     [SerializeField]
    private float HP;

    [SerializeField, HeaderAttribute("回復速度")]
    private float recovery = 0.02f;

    [SerializeField, HeaderAttribute("無敵時間(拍子)")]
    private int mutekiTime = 1;
    [SerializeField]
    private int mutekiCount = 0;

    [SerializeField, HeaderAttribute("回復フラグ(こっち優先)")]
    private bool notRecovery = false;
    [SerializeField, HeaderAttribute("次回復するまで(拍子)")]
    private int notRecoveryTime = 5;

    float time;

    float nextRecoveryTime = 0;

    public StageSelectMain _SelectMain;
    public StageSelectMain SelectMain
    {
        get
        {
            if (!_SelectMain) _SelectMain = FindObjectOfType<StageSelectMain>();
            return _SelectMain;
        }
    }

    bool HPSet = false;

    // Use this for initialization
    void Start()
    {
        canvasRenderer.SetAlpha(.0f);
        HP = MAXHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (!HPSet)
        {
            if (SelectMain.selectStage == 0) MAXHP = Stage1HP;
            if (SelectMain.selectStage == 1) MAXHP = Stage2HP;
            if (SelectMain.selectStage == 2) MAXHP = Stage3HP;
            HP = MAXHP;
            HPSet = true;
        }
        if (rhythmManager.FixedRhythm.Tyming)
        {
            time = rhythmManager.OnTempoTime;
            mutekiCount--;
            if (mutekiCount < 0)
                mutekiCount = 0;
            if (HP <= 1 && HP > 0)
            {
                staticScript.playerSE.BeforeDeath();
                staticScript.playerSE.BeforeDeath();
                staticScript.playerSE.BeforeDeath();
                staticScript.playerSE.BeforeDeath();
                staticScript.playerSE.BeforeDeath();
            }
        }

        if(HP < MAXHP )
        {
            
            float alpha = (1.0f - HP / MAXHP) * ((1.0f - (1.0f - HP / MAXHP)) * 0.5f) + AlphaCurve.Evaluate(time / rhythmManager.OnTempoTime) * (1.0f - HP / MAXHP);
            alpha *= 1.5f;
            if (alpha < 0)
                alpha = 0;
            if (alpha > 1)
                alpha = 1;
			canvasRenderer .SetAlpha( alpha);
            if (HP > 0)
                Recovery();
           
        }
        else if(HP > 0)
        {
            canvasRenderer.SetAlpha(.0f);
        }
       
        float t = Time.deltaTime;
        time -= t;
        nextRecoveryTime -= t;
    }

    private void Recovery()
    {
        if (notRecovery)
            return;
        if (nextRecoveryTime > 0)
            return;
        HP += recovery * Time.deltaTime;
        
        if (HP > MAXHP)
        {
            HP = MAXHP;
        }
    }

    public bool IsAlive
    {
        get 
        {
            if (HP <= 0)
                return false;
            return true;
        }
    }

    public bool IsDeath
    {
        get 
        {
            if (HP <= 0)
                return true;
            return false;
        }
    }

    public void Damage(float damage)
    {
        if (mutekiCount <= 0)
        {
            nextRecoveryTime = notRecoveryTime;
            mutekiCount = mutekiTime;
            HP -= damage;
        }
    }

}
