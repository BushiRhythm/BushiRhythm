using UnityEngine;
using System.Collections;

public class Kaze : MonoBehaviour {

   
    
    private Vector3 kazeScale;

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

    private bool kazeFrag = false;
    private float timar = 0;

    private int slowCheck;

    private bool startflg = true;

    [SerializeField]
    private float kazeTime = 0.2f;


	// Use this for initialization
	void Start () {

        kazeScale = this.transform.localScale * 2;
        this.transform.localScale = Vector3.zero;

    }
	
	// Update is called once per frame
	void Update () {


        if (rhythmManager.FixedRhythm.Tyming || startflg)
        {
            startflg = false;
            if (rhythmManager.IsSlow)
                slowCheck++;
            else
                slowCheck+=2;

            if (slowCheck %2 == 0)
            {
                kazeFrag = true;
                if (rhythmManager.IsSlow)
                    timar = kazeTime * 2;
                else
                    timar = kazeTime;
            }
        }

       if(kazeFrag)
       {
           timar -= Time.deltaTime;
           if (rhythmManager.IsSlow)
               this.transform.localScale = Vector3.Slerp(kazeScale, Vector3.zero, timar / kazeTime * 2);
           else
               this.transform.localScale = Vector3.Slerp(kazeScale, Vector3.zero, timar / kazeTime);
           
           if (timar <= 0)
           {
               kazeFrag = false;
               this.transform.localScale = Vector3.zero;
               timar = 0;
           }
           
       }

        
        
        
	}
}
