using UnityEngine;
using System.Collections;

public class TempoYama : MonoBehaviour {
    bool IsLoad = false;
    AnimationCurve MoveX;
    AnimationCurve MoveY;

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
    float YValue;



    private Vector3 pos;

	// Use this for initialization
	void Start () {
        pos = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {

        if (!IsLoad)
        {
            if (staticscript.smallwaveManager == null)
                return;
            MoveX = staticscript.smallwaveManager.XPos;
            MoveY = staticscript.smallwaveManager.YPos;
            IsLoad = true;
        }

        float Pos = staticscript.rhythmManager.FixedRhythm.Pos;
        Vector3 p = pos;
        
        float speed = 1.0f;
        if (rhythmManager.IsSlow)
            speed = 0.5f;

        p.z -= pos.z + MoveX.Evaluate(Pos) * speed * XValue;
        p.y -= pos.y + MoveY.Evaluate(Pos) * speed * YValue;

        this.transform.localPosition = p;

	}
}
