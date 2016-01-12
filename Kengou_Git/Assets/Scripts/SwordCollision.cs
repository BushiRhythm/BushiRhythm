using UnityEngine;
using System.Collections;

public class SwordCollision : MonoBehaviour {

    public Vector3 fastPos;
    public Vector3 secondPos;

    ActionTymingManager _actionTymingManager;
    ActionTymingManager actionTymingManager
    {
        get
        {
            if (!_actionTymingManager)
                _actionTymingManager = Object.FindObjectOfType<ActionTymingManager>();
            return _actionTymingManager;
        }

    }

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

    FlickManager _flickManager;
    FlickManager flickManager
    {
        get
        {
            if (!_flickManager)
                _flickManager = Object.FindObjectOfType<FlickManager>();
            return _flickManager;
        }

    }

    RectTransform MarkerCanvasRectTransform;


	// Use this for initialization
	void Start () {
        fastPos = Vector3.zero;
        secondPos = Vector3.zero;
        MarkerCanvasRectTransform = GameObject.Find("MarkerCanvas").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerStay(Collider other)
    {
        if (actionTymingManager.IsBladeActiond())
        {
            Vector2 size = MarkerCanvasRectTransform.sizeDelta;
            Vector3 scale = other.transform.localScale;
            scale.x /= size.x;
            scale.y /= size.y;

            Vector3 pos = other.transform.position;
            pos.x +=  pos.x * scale.x;
            pos.y +=  pos.y * scale.y;
            //pos.x = ((pos.x * 0.5f) + 0.5f) * Camera.main.pixelWidth;
            //pos.y = ((pos.y * 0.5f) + 0.5f) * Camera.main.pixelHeight;

            pos.z = 0;

            Vector2 fstartpos = flickManager.FlickStartPos;
            fstartpos = sword.ScreenPos(fstartpos);

            Vector3 flickPos = new Vector3(fstartpos.x, fstartpos.y, 0);


            Vector3 p1 = pos - flickPos;
            Vector3 p2 = fastPos - flickPos;

            if (p1.sqrMagnitude > p2.sqrMagnitude)
                secondPos = pos;
            else
            {
                secondPos = fastPos;
                fastPos = pos;
            }

            
        }


    }

}
