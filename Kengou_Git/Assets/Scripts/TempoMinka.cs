using UnityEngine;
using System.Collections;

public class TempoMinka : MonoBehaviour
{
    bool IsLoad = false;

    AnimationCurve ScaleX;
    

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
    float XValue;


    [SerializeField]
    private float zure;



    private Vector3 pos;
    private Vector3 scale;
    private Quaternion rote;

    // Use this for initialization
    void Start()
    {
        pos = this.transform.localPosition;
        scale = this.transform.localScale;
        rote = this.transform.localRotation;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsLoad)
        {
            if (staticscript.smallwaveManager == null)
                return;
            ScaleX = staticscript.smallwaveManager.YPos;
            IsLoad = true;
        }

        float Pos = staticscript.rhythmManager.FixedRhythm.Pos;
        Vector3 p = pos;
        Vector3 s = scale;
        Quaternion q = rote;

        float speed = 1.0f;
        //if (rhythmManager.IsSlow)
        //    speed = 0.5f;
        //p.y -= XValue / 2; 
        //p.y += pos.y + ScaleX.Evaluate(Pos) * speed * XValue / 3.2f;
        s.y += ScaleX.Evaluate(Pos) * speed * XValue;

        //int deg = (int)(ScaleY.Evaluate(Pos*2) * 15);
        //Quaternion qhoge = Quaternion.AngleAxis(deg, this.transform.right);
        //q = rote * qhoge;
        

        this.transform.localPosition = p;
        this.transform.localScale = s;
        this.transform.localRotation = q;

    }
}
