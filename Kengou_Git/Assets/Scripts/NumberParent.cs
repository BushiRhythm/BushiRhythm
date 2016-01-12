using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NumberParent : MonoBehaviour {

    [SerializeField, HeaderAttribute("数値")]
    int num = 0;
    public int Num {
        get {
            return num;
        }
    }

    [SerializeField, HeaderAttribute("桁数")]
    int MaxDigit = 1;

    int MaxNum = 10;

    [SerializeField, HeaderAttribute("余白")]
    int Space = 32;

    [SerializeField]
    GameObject NumObject;

    [SerializeField]
    GameObject[] NumObjects;

    [SerializeField]
    UINumbers[] NObj;

    [SerializeField, HeaderAttribute("チェックすると０で埋める")]
    bool NullSpaceZero = false;
	Image _image;

	Image Image
	{
		get
		{
			if(!_image)
			{
				_image = GetComponent<Image>();
			}
			return _image;
		}
	}


    CanvasRenderer _Cren;
    CanvasRenderer Cren {
        get {
            if (!_Cren) _Cren = GetComponent<CanvasRenderer>();
            return _Cren;
        }
    }
    CanvasRenderer[] ChildCren;


    RectTransform _RTrans;
    RectTransform RTrans
    {
        get
        {
            if (!_RTrans) _RTrans = GetComponent<RectTransform>();
            return _RTrans;
        }
    }
    RectTransform[] ChildRTrans;
	// Use this for initialization
    void Start()
    {
        NumObjects = new GameObject[MaxDigit];
        NObj = new UINumbers[MaxDigit];
        ChildCren = new CanvasRenderer[MaxDigit];
        ChildRTrans = new RectTransform[MaxDigit];
        for (int i = 0; i < MaxDigit; i++)
        {
            NumObjects[i] = Instantiate(NumObject, transform.position + new Vector3(-Space * i, 0, 0), transform.rotation) as GameObject;
            NumObjects[i].transform.parent = this.transform;
            NObj[i] = NumObjects[i].GetComponent<UINumbers>();
            NObj[i].SetDigit(i);
            ChildCren[i] = NumObjects[i].GetComponent<CanvasRenderer>();
            ChildRTrans[i] = NumObjects[i].GetComponent<RectTransform>();
        }
        SetNum(num);
	}

    public void SetColor(Color Col)
    {
        Image.color = Col;
    }
    public void SetNum(int Value) {
        num = Value;
        int val = Value;
        int dig = GetDigit();

        if (NullSpaceZero)
        {
            for (int i = 0; i < MaxDigit; i++)
                NObj[i].SetSprite(0);
        }
        else {
            for (int i = 0; i < MaxDigit; i++)
                NObj[i].SetNullSprite();
        }

        for (int i = 0; i < MaxDigit; i++)
        {
            if (val == 0)
            {
                if (num < 0) NObj[i].SetMinusSprite();
                if (i == 0) NObj[i].SetSprite(0);
                break;
            }
            NObj[i].SetSprite(val % 10);
            val = val / 10;
        }
    }
    public int GetDigit()
    {
        int n = Mathf.Abs(Num);
        int i = 0;
        while(n > 0){
            n = n / 10;
            i++;
        }
        return i;
    }

    [SerializeField, HeaderAttribute("画面サイズに対する幅の調整(基本1280)")]
    float WidthFix = 1280.0f;

    [SerializeField, HeaderAttribute("カメラで取ってる時は調整いらないのでtrue")]
    bool IsCameraSpace = false;
    void Update()
    {
        float alp = Cren.GetAlpha();
        for (int i = 0; i < MaxDigit; i++)
        {
            ChildCren[i].SetAlpha(alp);

            Vector3 ps = RTrans.position;
            if (IsCameraSpace) {
                ps.x += (-Space * i * 0.25f);
                ChildRTrans[i].position = ps;
            }
            else
            {
                ps.x += (-Space * i) * Screen.width / WidthFix;
                ChildRTrans[i].position = ps;
            }

            ps = RTrans.localScale;
            ChildRTrans[i].localScale = ps;
        }
	}
}
