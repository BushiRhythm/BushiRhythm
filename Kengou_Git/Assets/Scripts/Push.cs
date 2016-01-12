using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Push : MonoBehaviour {

    [SerializeField, TooltipAttribute("色が変わるまでの速さ"), HeaderAttribute("色変化速度"),Range(.01f,1.0f)]
	private float ChangeSpeed = .0f;

    [SerializeField]
    GameObject RevPosObj;

    private bool bpush = false;
    private float AlNum = .0f;
    public bool Bpush {
        get { return bpush; }
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

	Button _button;
	public Button button
	{
		get
		{
			if(!_button)
			{
				_button = GetComponent<Button>();
			}
			return _button;
		}

	}

    OptionManager _Option;
    OptionManager Option
    {
        get
        {
            if (!_Option) _Option = FindObjectOfType<OptionManager>();
            return _Option;
        }
    }
    [SerializeField]
    private GameObject Pimg;

    CanvasRenderer _ThisCanvas;
    CanvasRenderer ThisCanvas{
        get{
            if(!_ThisCanvas) _ThisCanvas = GetComponent<CanvasRenderer>();
            return _ThisCanvas;
        }
    }
    CanvasRenderer _PCanvas;
    CanvasRenderer PCanvas
    {
        get
        {
            if (!_PCanvas) _PCanvas = Pimg.GetComponent<CanvasRenderer>();
            return _PCanvas;
        }
    }


	// Use this for initialization
	void Start () {
        Pimg.GetComponent<CanvasRenderer>().SetAlpha(0.0f);

        if (Option.GetLR())
        {
			this.GetComponent <RectTransform> ().pivot = RevPosObj.GetComponent <RectTransform> ().pivot;
			this.transform.position = RevPosObj.transform.position;
		}
        Vector3 i = Pimg.transform.localScale;
        i.x = 1.0f;
        Pimg.transform.localScale = i;
		
		Vector2 pv;
		pv.x = 0.5f;
		pv.y = 0.5f;
	}

    // Update is called once per frame

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
    GaugeTest _Gauge;
    GaugeTest Gauge{
        get{
            if (!_Gauge) _Gauge = FindObjectOfType<GaugeTest>();
            return _Gauge;
        }
    }

    void Update()
    {
        if (bpush){
            AlNum = AlNum + ChangeSpeed;
        }else{
            AlNum = AlNum - ChangeSpeed;
        }
        AlNum = Mathf.Clamp(AlNum, .0f, 1.0f);
		ThisCanvas.SetAlpha( ( 1.0f - AlNum ));
        PCanvas.SetAlpha(AlNum);

    }

    bool PushFlag = false;
    public void btPush()
    {
        if (Gauge.IsGaugeFull() && !rhythmManager.IsSlow && !Gauge.IsPayCost && !PushFlag)
        {
            Gauge.PayCost();
            bpush = true;
            PushFlag = true;
        }
        else
        {
            Gauge.PenaCost();
            PushFlag = false;
        }
        Invoke("btPull", 0.001f);

    }
    public void btPull()
    {
        bpush = false;
    }

}
