using UnityEngine;
using System.Collections;

public class ResultPanelMove : MonoBehaviour {


    FlickManager _FManager;
    FlickManager FManager
    {
        get
        {
            if (!_FManager) _FManager = GetComponent<FlickManager>();
            return _FManager;
        }
    }

	// Use this for initialization
	void Start () {
	
	}

    Vector3 MousePos;
    public Vector3 GetMousePos
    {
        get
        {
            return MousePos;
        }
    }

    Vector3 FStart;
    Vector3 FEnd;
    public Vector3 FlickVector()
    {
        Vector3 v = (FEnd - FStart);

        return v;
    }

    bool FlickComplete = false;


    //0なら総評、1なら詳細
    int ShowData = 0;
    public int GetShowData
    {
        get
        {
            return ShowData;
        }
    }
	// Update is called once per frame
	void Update () {

        if (FManager.IsEndFlick())
        {
            if (FEnd.y > Screen.height * 0.25f)
            {
                if (FlickVector().x > -10.0f)
                {
                    if (ShowData == 1) ShowData = 0;
                }
                else if (FlickVector().x < 10.0f)
                {
                    if (ShowData == 0) ShowData = 1;
                }
            }
            FStart.x = .0f;
            FStart.y = .0f;
            FStart.z = .0f;
            FEnd.x = .0f;
            FEnd.y = .0f;
            FEnd.z = .0f;
        }
        else
        {
            if (FManager.IsFlick())
            {
                FStart = FManager.FlickStartPos;
                FEnd = FManager.FlickEndPos;
            }
        }
        MousePos = Input.mousePosition;
	}
}
