using UnityEngine;
using System.Collections;

public class TempoSakurahubukiSyou : MonoBehaviour {

    bool IsLoad = false;


    [SerializeField]
    AnimationCurve ScaleX;

    [SerializeField]
    AnimationCurve hubuki;
    [SerializeField]
    AnimationCurve hubukiY;

    [SerializeField]
    AnimationCurve sakurascale;
    [SerializeField]
    AnimationCurve sakuraposX;
    [SerializeField]
    AnimationCurve sakuraposY;

    [SerializeField]
    private float sakurascaleValue;
    [SerializeField]
    private float sakuraposXValue;
    [SerializeField]
    private float sakuraposYValue;


    

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

    




    private Vector3 pos;
    private Vector3 scale;
    private Quaternion rote;

    [SerializeField]
    private Vector3 MAXSize = Vector3.zero;

    [SerializeField]
    private float XValue;
    [SerializeField]
    private float YValue;

    private int step;

    private float sakuraAlpha;
    private Color sakuraColor;
    private Renderer sakuraRenderer;


    private float timar = 0;

    // Use this for initialization
    void Start()
    {
        pos = this.transform.localPosition;
        scale = this.transform.localScale;
        rote = this.transform.localRotation;

        sakuraRenderer = transform.FindChild("hanabira").transform.FindChild("grid24").GetComponent<Renderer>();
        transform.FindChild("hanabira").transform.FindChild("grid24").GetComponent<Renderer>().material.color = new Color(1, 1, 1, 1);
        transform.FindChild("hanabira").transform.FindChild("grid24").transform.localPosition = Vector3.zero;


        int a = sakuraRenderer.GetInstanceID();
        sakuraColor = sakuraRenderer.material.color;
        sakuraColor.a = 0;
        sakuraAlpha = 0;

        sakuraRenderer.material.SetColor(a, sakuraColor);

        step = 0;
       
    }

    // Update is called once per frame
    void Update()
    {

        //float Pos = staticscript.rhythmManager.FixedRhythm.Pos;
        Vector3 p = pos;
        Vector3 s = scale;
        Quaternion q = rote;


        SakuraHubuki0(ref p, ref s, ref q);


        


        //
        //float speed = 1.0f;
        //if (rhythmManager.IsSlow)
        //    speed = 0.5f;
        //p.y -= XValue / 2; 
        //p.y += pos.y + ScaleX.Evaluate(Pos) * speed * XValue / 3.2f;
        //s.y += ScaleX.Evaluate(Pos) * speed * XValue;

        //int deg = (int)(ScaleY.Evaluate(Pos * 2) * XValue);
        //Quaternion qhoge = Quaternion.AngleAxis(deg, this.transform.right);
        //q = rote * qhoge;


        this.transform.localPosition = p;
        this.transform.localScale = s;
        this.transform.localRotation = q;
        //sakuraRenderer.material.color = sakuraColor;
        //transform.FindChild("hanabira").transform.FindChild("grid24").GetComponent<Renderer>().material.color = sakuraColor;


    }

    private void SakuraHubuki0(ref Vector3 p, ref Vector3 s, ref Quaternion q)
    {
        float Pos = staticscript.rhythmManager.FixedRhythm.Pos;
        Vector3 v = new Vector3(1,1,1);
        s += v * sakurascale.Evaluate(Pos) * sakurascaleValue;

        p.x += sakuraposX.Evaluate(Pos) * sakuraposXValue;
        p.y -= sakuraposY.Evaluate(Pos) * sakuraposYValue;


    }

    private void SakuraHubuki1(ref Vector3 p, ref Vector3 s, ref Quaternion q)
    {
        float t = (staticscript.rhythmManager.OnTempoTime - timar) / staticscript.rhythmManager.OnTempoTime;
        switch (step)
        {
            case 0:
                s = Vector3.zero;
                timar = staticscript.rhythmManager.OnTempoTime;
                step++;
                break;
            case 1:

                s = ScaleX.Evaluate(t) * MAXSize * 1.0f;
                timar -= Time.deltaTime;

                if (timar < 0)
                {
                    s = new Vector3(1, 1, 1);
                    timar = staticscript.rhythmManager.OnTempoTime;
                    step++;
                }
                break;

            case 2:

                p += hubuki.Evaluate(t) * this.transform.right * XValue;

                timar -= Time.deltaTime;

                if (timar < 0)
                {
                    timar = staticscript.rhythmManager.OnTempoTime;
                    step++;
                }
                break;
            case 3:
                p += hubuki.Evaluate(1.0f) * this.transform.right * XValue;
                p -= hubuki.Evaluate(t) * this.transform.right * XValue;
                p += hubukiY.Evaluate(t) * -this.transform.up * YValue;
                timar -= Time.deltaTime;
                //s = new Vector3(1,1,1) * (1.0f - (1.0f - t) * 0.25f);
                if (timar < 0)
                {
                    timar = staticscript.rhythmManager.OnTempoTime;
                    //s = new Vector3(0.75f, 0.75f, 0.75f);
                    step++;
                }
                break;
            case 4:
                p += hubuki.Evaluate(t) * this.transform.right * XValue;
                p += hubukiY.Evaluate(1) * -this.transform.up * YValue;

                timar -= Time.deltaTime;

                //s = new Vector3(1, 1, 1) * (0.75f - (1.0f - t) * 0.75f);
                //s = new Vector3(1, 1, 1) * (1-t);

                if (timar < 0)
                {
                    timar = staticscript.rhythmManager.OnTempoTime;
                    s = new Vector3(0, 0, 0);
                    step = 0;
                }
                break;

        }

    }

    private void SakuraHubuki2(ref Vector3 p, ref Vector3 s, ref Quaternion q)
    {
        float t = (staticscript.rhythmManager.OnTempoTime - timar) / staticscript.rhythmManager.OnTempoTime;
        switch (step)
        {
            case 0:
                s = Vector3.zero;
                timar = staticscript.rhythmManager.OnTempoTime;
                step++;
                break;
            case 1:

                s = ScaleX.Evaluate(t) * MAXSize * 1.0f;
                timar -= Time.deltaTime;

                if (timar < 0)
                {
                    s = new Vector3(1, 1, 1);
                    timar = staticscript.rhythmManager.OnTempoTime;
                    step++;
                }
                break;

            case 2:

                p += t * -this.transform.up * YValue * 0.5f;

                timar -= Time.deltaTime;

                if (timar < 0)
                {
                    timar = staticscript.rhythmManager.OnTempoTime;
                    step++;
                }
                break;
            case 3:
                p += (1+t) * -this.transform.up * YValue * 0.5f;

                timar -= Time.deltaTime;

                if (timar < 0)
                {
                    timar = staticscript.rhythmManager.OnTempoTime;
                    step++;
                }
                break;
            case 4:
                p += (2 + t) * -this.transform.up * YValue * 0.5f;

                timar -= Time.deltaTime;
                //s = new Vector3(1,1,1) * (1.0f - (1.0f - t) * 0.25f);
                if (timar < 0)
                {
                    timar = staticscript.rhythmManager.OnTempoTime;
                    //s = new Vector3(0.75f, 0.75f, 0.75f);
                    step=0;
                }
                break;

        }

    }



}
