using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageSelectWhiteText : MonoBehaviour {

    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;
    int selectStage = 0;

    bool show = false;
    [SerializeField]
    float Per = .0f;
    [SerializeField]
    float Per2 = .0f;
    Vector3 ScaleBase;

    [SerializeField]
    GameObject Dodon;
    [SerializeField]
    public AnimationCurve curve;
    [SerializeField]
    public AnimationCurve DeleteCurve;

    [SerializeField]
    Vector3 Move;
    CanvasRenderer _CR;
    CanvasRenderer CR {
        get {
            if(!_CR)_CR = GetComponent<CanvasRenderer>();
            return _CR;
        }
    }

    StageSelectMain _SelectMain;
    StageSelectMain SelectMain
    {
        get
        {
            if (!_SelectMain)
                _SelectMain = Object.FindObjectOfType<StageSelectMain>();
            return _SelectMain;
        }
    }

	// Use this for initialization
    void Start()
    {
        GetComponent<CanvasRenderer>().SetAlpha(0);
        ScaleBase = transform.localScale;
	}
    // Use this for initialization
    public bool IsComplete()
    {
        return (Per2 >= 1.0f);
    }

    void PicUpdate() {
        switch (SelectMain.selectStage)
        {
            case 0: GetComponent<Image>().sprite = sprite0; break;
            case 1: GetComponent<Image>().sprite = sprite1; break;
            case 2: GetComponent<Image>().sprite = sprite2; break;
        }
        selectStage = SelectMain.selectStage;
    }
	// Update is called once per frame

    bool dodonflag = false;
    int wait = 0;

	void Update () {
        if (SelectMain.selectStage != selectStage) PicUpdate();

        if (Per >= 1.0f)
        {
            if (!dodonflag) {
                if (Dodon) Instantiate(Dodon, transform.position, transform.rotation);
                dodonflag = true;
            }
            Per = 1.0f;
            wait++;
            if(wait > 60)Per2 += 0.5f * Time.deltaTime;
            if (Per2 > 1.0f) Per2 = 1.0f;
            transform.localScale = transform.localScale + Move;
            Move = Move * 0.5f;
            CR.SetAlpha(1.0f - (DeleteCurve.Evaluate(Per2)) );
        }
        if (SelectMain.OnStageSelect)
            if (Per < 1.0f)
            {
                Per += 1.5f * Time.deltaTime;
                CR.SetAlpha(curve.Evaluate(Per));
                Vector3 Scl = transform.localScale;

                Scl.x = ScaleBase.x + 1.0f - curve.Evaluate(Per);
                Scl.y = ScaleBase.y + 1.0f - curve.Evaluate(Per);

                transform.localScale = Scl;
            }

	}
}
