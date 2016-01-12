using UnityEngine;
using System.Collections;

public class MoveButton : MonoBehaviour {

    [SerializeField, TooltipAttribute("短すぎてtrueを取れない時は長めに"), HeaderAttribute("押判定長さ(秒)")]
    private float ButtonLength = 0.05f;

    [SerializeField, TooltipAttribute("色が戻るまでの速さ"), HeaderAttribute("色変化速度"), Range(.01f, 1.0f)]
    private float ChangeSpeed = .0f;

    [SerializeField]
    GameObject RevPosObj;

    private bool bpush = false;
    private float AlNum = .0f;
    private float FlNum = .0f;
    public bool Bpush
    {
        get { return bpush; }
    }
    [SerializeField]
    private GameObject Pimg;
    [SerializeField]
    private GameObject Missimg;

    [SerializeField]
    private CanvasRenderer[] CImages;

    OptionManager _Option;
    OptionManager Option
    {
        get
        {
            if (!_Option) _Option = FindObjectOfType<OptionManager>();
            return _Option;
        }
    }

    ActionTymingManager _actionManager;
    ActionTymingManager actionManager
    {
        get
        {
            if (!_actionManager) _actionManager = FindObjectOfType<ActionTymingManager>();
            return _actionManager;
        }
    }

    ActionSetter _actionSetter;
    ActionSetter actionSetter
    {
        get
        {
            if (!_actionSetter) _actionSetter = FindObjectOfType<ActionSetter>();
            return _actionSetter;
        }
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

    MoveButtonJudge _MVJudge;

    MoveButtonJudge MVJudge
    {
        get
        {
            if (!_MVJudge)
                _MVJudge = GetComponent<MoveButtonJudge>();
            return _MVJudge;
        }

    }

	// Use this for initialization
    void Start()
	{
        Pimg.GetComponent<CanvasRenderer>().SetAlpha(0.0f);

		
		if (Option.GetLR()) {
			this.GetComponent <RectTransform>().pivot = RevPosObj.GetComponent <RectTransform>().pivot;
            this.transform.position = RevPosObj.transform.position;
		}

        Vector3 i = Pimg.transform.localScale;
		i.x = 1.0f;
        Pimg.transform.localScale = i;
        CImages[0].SetAlpha(0.0f);
	}

    bool BTJudge = false;
	// Update is called once per frame
    void Update()
    {
        if (AlNum>.0f)
        {
            AlNum = AlNum - ChangeSpeed;
        }
        if (FlNum > .0f)
        {
            FlNum = FlNum - ChangeSpeed * 0.75f;
        }

        //BTJudge = MVJudge.actionManagerIsFailure;

        AlNum = Mathf.Clamp(AlNum, .0f, 1.0f);
        if (MVJudge.actionSetterIsStepActionSucsess)
        {
            BTJudge = true;
        }

        if (rhythmManager.FixedRhythm.Tyming && !MVJudge.actionManagerIsFailure)
        {
            BTJudge = true;
        }
        CImages[1].SetAlpha(AlNum);
        if (MVJudge.actionSetterIsStepActionFailure)
        {
            BTJudge = false;
        }

        if (BTJudge) CImages[0].SetAlpha(0.0f);
        else CImages[0].SetAlpha(1.0f);

        if (rhythmManager.FixedRhythm.Tyming && !MVJudge.actionManagerIsFailure) {
            FlNum = 0.5f;
        }
        if (MVJudge.actionSetterIsStepActionSucsess)
        {
            FlNum = 1.0f;
        }
        CImages[2].SetAlpha(FlNum);


	}
    void btPull()
    {
        bpush = false;
    }
    public void btPush()
    {
        bpush = true;
        AlNum = 1.0f;
        Invoke("btPull", 0.0001f);
    }
}
