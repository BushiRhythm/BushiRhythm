using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GaugeTest : MonoBehaviour {

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


    // Use this for initialization
    BulletResultManager _bulletResultManager;

    public BulletResultManager bulletResultManager
    {
        get
        {
            if (!_bulletResultManager)
                _bulletResultManager = FindObjectOfType<BulletResultManager>();
            return _bulletResultManager;
        }
    }

    [SerializeField]
    Image delayimage;

    [SerializeField]
    CanvasRenderer NotUsedGoku;
    float NoUseAlpha = .0f;

    bool isStart = false;
    void Start()
	{
        rectTransform = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        gaugelength = 250.0f;
	}
	RectTransform rectTransform;

    public bool IsGaugeFull() {
        return (bool)(gaugelength >= GaugeMax * 0.25f);
    }

	// Update is called once per frame
    private float gaugelength = .0f;
    public float Gaugelength
    {
        get { return gaugelength; }
        set { gaugelength = value; }
    }

    [SerializeField, TooltipAttribute(""), HeaderAttribute("ゲージ最大の数値")]
    private float GaugeMax = 1000.0f;

	public Image img;

    public void AddGauge(int value) {
        gaugelength = gaugelength + value;
    }
    public void PayCost()
    {
        gaugelength = gaugelength - GaugeMax * 0.25f;
        SlowCost = true;
    }
    public void PenaCost()
    {
        gaugelength = gaugelength - GaugeMax * 0.05f;
    }

    bool SlowCost = false;//極のコストを支払ったか
    public bool IsPayCost
    {
        get {
            return SlowCost;
        }
    }

	void Update () {
        if (!isStart) {
           gaugelength = 250.0f;
            isStart = true;
        }
        //if (Input.GetKey(KeyCode.UpArrow)) { gaugelength += 0.1f; }
        //if (Input.GetKey(KeyCode.DownArrow)) { gaugelength -= 0.1f; }
        //if (btn.GetComponent<Push>().Bpush) { gaugelength -= 1.0f; }

        //極ボタンが押されている時の状態
        if (rhythmManager.IsSlow)
        {
            NoUseAlpha = .0f;
            NotUsedGoku.SetAlpha(NoUseAlpha);
            if (rhythmManager.FixedRhythm.Tyming)
            delayimage.fillAmount = delayimage.fillAmount - (0.25f / 16.0f);

        }
        else
        {

            if (IsGaugeFull())
            {
                NoUseAlpha = NoUseAlpha - 5.0f * Time.deltaTime;
            }
            else {
                NoUseAlpha = NoUseAlpha + 5.0f * Time.deltaTime;
            }
            NotUsedGoku.SetAlpha(NoUseAlpha);
            SlowCost = false;
        }
        if (!SlowCost)
            delayimage.fillAmount = img.fillAmount;
        if (gaugelength >= GaugeMax)
            gaugelength = GaugeMax;

        if (gaugelength < 0.0f)
            gaugelength = .0f;

        if (NoUseAlpha >= 1.0f)
            NoUseAlpha = 1.0f;

        if (NoUseAlpha < 0.0f)
            NoUseAlpha = .0f;

		float FA = 0.0f;
		FA = gaugelength / GaugeMax;
		img.fillAmount = FA;

	}
}
