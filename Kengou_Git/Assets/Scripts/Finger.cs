using UnityEngine;
using System.Collections;

public class Finger : MonoBehaviour {

    [SerializeField]
    AnimationCurve ScaleCurve;

    private Vector3 pos;
    private Vector3 scale;
    private Quaternion rote;
   private  float alpha;
	// Use this for initialization


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

    StaticScript _staticscript;
    StaticScript staticscript
    {
        get
        {
            if (!_staticscript)
                _staticscript = Object.FindObjectOfType<StaticScript>();
            return _staticscript;
        }

    }
    ActionSetter _actionSetter;
    ActionSetter actionSetter
    {
        get
        {
            if (!_actionSetter)
                _actionSetter = Object.FindObjectOfType<ActionSetter>();
            return _actionSetter;
        }
    }

    OptionManager _optionManager;

    OptionManager optionManager
    {
        get
        {
            if (!_optionManager)
                _optionManager = FindObjectOfType<OptionManager>();
            return _optionManager;
        }

    }
    FrontMoverManager _frontMoverManager;
    FrontMoverManager frontMoverManager
    {
        get
        {
            if (!_frontMoverManager)
                _frontMoverManager = FindObjectOfType<FrontMoverManager>();
            return _frontMoverManager;
        }

    }
    private bool tutorial = true;

    Vector3 fingervec;

    int step = 0;
    //  全部１なら終了
    int[] Moveflg = null;
    //  何回タップできたら消すか
    const int tapNum = 3;
    // 何歩進んだら消すか
    const int checkstep = 20;

    //ボタンを押し間違ったときfalse、falseなら消す判定を飛ばす
    bool movefalse = true;
    
	void Start () {
        pos = this.transform.localPosition;
        scale = this.transform.localScale;
        rote = this.transform.localRotation;
        alpha = 1.0f;
        fingervec = new Vector3(-0.5f, 0.5f, 0);

        //if (optionManager.GetLR())
        //{
        //    fingervec.x = -fingervec.x;
        //}
        Moveflg = new int[tapNum];
        
	}
    

	// Update is called once per frame
	void Update () {
        if (alpha == 0)
        {
            canvasRenderer.SetAlpha(alpha);
            return;
        }

        Tutorial();

        float rhythmPos = staticscript.rhythmManager.FixedRhythm.Pos;

        Vector3 p = pos;
        Vector3 s = scale;
        Quaternion q = rote;

        //Vector3 v = new Vector3(1, 1, 1);
        //s += v * ScaleCurve.Evaluate(rhythmPos) * 5;
        //int deg = (int)((1.0f-this.transform.localScale.y) * 180);
        //Quaternion qhoge = Quaternion.AngleAxis(deg, this.transform.right);
        //q = rote * qhoge;

        p += fingervec * ScaleCurve.Evaluate(rhythmPos) * 500;

        
        

        this.transform.localPosition = p;
        this.transform.localScale = s;
        this.transform.localRotation = q;
        canvasRenderer.SetAlpha(alpha);

        if (step < rhythmPos)
        {
            step++;
            Moveflg[step % tapNum] = 0;
            movefalse = true;

        }
        if (!movefalse)
        {
            return;
        }
        if (actionSetter.IsStepActionFailure)
        {
            movefalse = false;
            for (int i = 0; i < tapNum; i++)
            {
                Moveflg[i] = 0;
            }
            return;
        }


        if (staticscript.actionTymingManager.IsStepAction() && staticscript.actionTymingManager.IsStepActiond())
        {
            Moveflg[step % tapNum] = 1;
        }
        bool check = true;
        for (int i = 0; i < tapNum; i++)
        {
            if (!check)
                break;
            if (Moveflg[i] == 0)
                check = false;
        }
        if (check)
        {
            tutorial = false;
        }
        if (frontMoverManager.GetStep > checkstep)
        {
            tutorial = false;
        }
	}

    bool Tutorial()
    {
        
        if (!tutorial)
        {
            //alpha = 0;
            if (alpha > 0)
                alpha -= 0.5f * Time.unscaledDeltaTime;
            if (alpha < 0)
                alpha = 0;
            canvasRenderer.SetAlpha(alpha);
        }
        return tutorial;
    }
}
