using UnityEngine;
using System.Collections;

public class TempoSakura : MonoBehaviour {

    [SerializeField]
    AnimationCurve Angle;

    [SerializeField]
    private float roteValue;

    [SerializeField]
    AnimationCurve SakuraScale;

    [SerializeField]
    private float SakuraScaleValue;

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

        int deg = (int)(Angle.Evaluate(Pos*2) * roteValue);
        Quaternion qhoge = Quaternion.AngleAxis(deg, this.transform.right);
        q = rote * qhoge;

        Vector3 vec = new Vector3(1, 1, 1);
        s += vec * SakuraScale.Evaluate(Pos) * SakuraScaleValue;

        this.transform.localPosition = p;
        this.transform.localScale = s;
        this.transform.localRotation = q;
	}
}
