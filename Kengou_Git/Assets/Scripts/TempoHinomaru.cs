using UnityEngine;
using System.Collections;

public class TempoHinomaru : MonoBehaviour {

    [SerializeField]
    AnimationCurve Scale;

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

    FrontMoverManager _frontMoverManager;
    FrontMoverManager frontMoverManager
    {
        get
        {
            if (!_frontMoverManager)
                _frontMoverManager = Object.FindObjectOfType<FrontMoverManager>();
            return _frontMoverManager;
        }

    }

    private Vector3 pos;
    private Vector3 scale;
    private Quaternion rote;

    [SerializeField]
    private float ScaleValue;
    [SerializeField]
    private float upValue;

    [SerializeField]
    private float rightValue;

	// Use this for initialization
	void Start () {
        pos = this.transform.localPosition;
        scale = this.transform.localScale;
        rote = this.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        float Pos = staticscript.rhythmManager.FixedRhythm.Pos;
        Vector3 p = pos;
        Vector3 s = scale;
        Quaternion q = rote;

        Vector3 vec = new Vector3(1, 1, 1);
        if (frontMoverManager.GetProgress == 1.0f)
        {
            s += vec * Scale.Evaluate(Pos) * ScaleValue;
        }else
        {
            s += vec * Scale.Evaluate(Pos) * ScaleValue * 2;
        }

        
        p.y += frontMoverManager.GetStep * upValue + frontMoverManager.GetProgress * upValue;
        p.x += frontMoverManager.GetStep * rightValue + frontMoverManager.GetProgress * rightValue;


        this.transform.localPosition = p;
        this.transform.localScale = s;
        this.transform.localRotation = q;
	
	}
}
