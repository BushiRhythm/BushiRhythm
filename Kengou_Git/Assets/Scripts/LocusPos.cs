using UnityEngine;
using System.Collections;

public class LocusPos : MonoBehaviour
{

    [SerializeField]
    private float dist = 1.0f;

    private TrailRenderer TR;
    float MAXtime;
    float timar = 0;

    Sword _sword;

    Sword sword
    {
        get
        {
            if (!_sword)
                _sword = Object.FindObjectOfType<Sword>();
            return _sword;
        }
    }

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


    // Use this for initialization
    void Start()
    {
        TR = this.GetComponent<TrailRenderer>();
         MAXtime = TR.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (sword.GetAttackCheck || sword.GetInterpolationCheck)
        {
            Transform t = GameObject.Find("ha").transform;
            this.transform.position = t.position + t.up * dist;
        }
        if(sword.HitStopFlag)
        {
            timar = rhythmManager.OnTempoTime / 2;
            TR.time = MAXtime + timar;
        }


        if(timar <  0)
        {
            TR.time = MAXtime;
            
        }
        timar -= Time.deltaTime;
        //TR.isVisible = false;

    }
}
