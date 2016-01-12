using UnityEngine;
using System.Collections;

public class TempoTuki : MonoBehaviour {

    [SerializeField]
    AnimationCurve Scale;

    [SerializeField]
    AnimationCurve Rotation;
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
    private float ScaleValue;


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

        Vector3 vec = new Vector3(1, 1, 1);

        s += vec * Scale.Evaluate(Pos) * ScaleValue;
        int deg = (int)(Rotation.Evaluate(Pos) * 30 * (int)Pos);
        Quaternion qhoge = Quaternion.AngleAxis(deg, this.transform.forward);
        q = rote * qhoge;

        this.transform.localPosition = p;
        this.transform.localScale = s;
        this.transform.localRotation = q;
	}
}
