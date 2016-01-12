using UnityEngine;
using System.Collections;

public class TempoSakuraSyouAlpha : MonoBehaviour {

    [SerializeField]
    AnimationCurve Alpha;


    private float timar;

    private Color col;

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

    int step = 0;

    float timeNum = 0;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

        float Pos = staticscript.rhythmManager.FixedRhythm.Pos;

        col.a = Alpha.Evaluate(Pos) * 0.8f;

        if (col.a > -0.8f)
            col.a = 0.8f;


        //float t = (staticscript.rhythmManager.OnTempoTime - timar) / staticscript.rhythmManager.OnTempoTime;
        //switch (step)
        //{
        //    case 0:
        //
        //        timar = staticscript.rhythmManager.OnTempoTime;
        //        step++;
        //        break;
        //    case 1:                
        //        col.a = t;
        //        timar -= Time.deltaTime;
        //
        //        if (timar < 0)
        //        {
        //            col.a = 1.0f;
        //            timar = staticscript.rhythmManager.OnTempoTime;
        //            step++;
        //        }
        //        break;
        //
        //    case 2:
        //
        //        timar -= Time.deltaTime;
        //
        //        if (timar < 0)
        //        {
        //            timar = staticscript.rhythmManager.OnTempoTime;
        //            step++;
        //        }
        //        break;
        //    case 3:
        //
        //        col.a = 1.0f - (1.0f - t) * 0.25f;
        //        timar -= Time.deltaTime;
        //
        //        if (timar < 0)
        //        {
        //            timar = staticscript.rhythmManager.OnTempoTime;
        //            col.a = 0.75f;
        //            step++;
        //        }
        //        break;
        //    case 4:
        //
        //        col.a = 0.75f - (1.0f - t) * 0.75f;
        //        timar -= Time.deltaTime;
        //
        //        if (timar < 0)
        //        {
        //            timar = staticscript.rhythmManager.OnTempoTime;
        //            col.a = 0;
        //            step = 0;
        //        }
        //        break;
        //
        //}
        
	}


    public float GetSakuraAlpha()
    {
        return col.a;
    }

}
