using UnityEngine;
using System.Collections;

public class TempoSakurahubuki : MonoBehaviour {

     [SerializeField]
    AnimationCurve posX;



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

    [SerializeField]
    float MAX_XValue;

    [SerializeField]
    float MAX_YValue;
    [SerializeField]
    float MAX_Time;

    float MAXTime;
    float alphaTime;

    float upTime;
    float XValue;
    float YValue;

    float randX;


    private Vector3 pos;
    private Vector3 scale;
    private Quaternion rote;

    SpriteRenderer sakuraRenderer;
    Color sakuraColor;

    // Use this for initialization
    void Start()
    {
        pos = this.transform.localPosition;
        scale = this.transform.localScale;
        rote = this.transform.localRotation;

        SetStatus();

        sakuraRenderer = this.GetComponent<SpriteRenderer>();
        sakuraColor = sakuraRenderer.color;
        sakuraColor.a = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

        float Pos = staticscript.rhythmManager.FixedRhythm.Pos;
        Vector3 p = pos;
        Vector3 s = scale;
        Quaternion q = rote;

        p.x += posX.Evaluate((Pos * 0.5f) + randX) * XValue;
        p.y += (MAXTime - alphaTime) * YValue;

        
        if(upTime < 0)
        {
            
            sakuraColor.a = 0.5f * alphaTime / MAXTime;
            sakuraRenderer.color = sakuraColor;
            alphaTime -= Time.deltaTime;
            if (alphaTime < 0)
                SetStatus();
        }
        else
        {
            sakuraColor.a = 0.5f * (3.0f - upTime) / 3.0f;
            sakuraRenderer.color = sakuraColor;
            upTime -= Time.deltaTime;
        }

        


        




        this.transform.localPosition = p;
        this.transform.localScale = s;
        this.transform.localRotation = q;

    }

    private void SetStatus()
    {
        int ran = UnityEngine.Random.Range(1, (int)MAX_Time+1);
        MAXTime = ran * 4;
        alphaTime = MAXTime;
        ran = UnityEngine.Random.Range((int)(-MAX_XValue * 100), (int)(MAX_XValue * 100)+1);
        XValue = (float)(ran * 0.002f);
        ran = UnityEngine.Random.Range(0, (int)(MAX_YValue * 100) + 1);
        YValue = -(float)(ran * 0.001f);

        ran = UnityEngine.Random.Range(0, 101);
        randX = ran * 0.01f;

        upTime = 3.0f;
    }
}
