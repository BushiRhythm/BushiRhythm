using UnityEngine;
using System.Collections;

public class ResultPanelPos : MonoBehaviour {

    [SerializeField]
    int Num = 0;

    [SerializeField]
    bool MoveFlag = false;
	// Use this for initialization
	void Start () {
	    
	}


    FlickManager _FManager;
    FlickManager FManager
    {
        get
        {
            if (!_FManager) _FManager = FindObjectOfType<FlickManager>();
            return _FManager;
        }
    }

    ResultPanelMove _resultPanelMove;
    ResultPanelMove resultPanelMove
    {
        get
        {
            if (!_resultPanelMove) _resultPanelMove = FindObjectOfType<ResultPanelMove>();
            return _resultPanelMove;
        }
    }

    int ObjSpace = 1064;

    bool IsMoveOk()
    {
        float p = (resultPanelMove.GetMousePos.x);
        float targ = ((Num - resultPanelMove.GetShowData) * (float)(ObjSpace)) * (0.8f) + (resultPanelMove.GetMousePos.x - FManager.FlickStartPos.x) * 0.25f;
        return Mathf.Abs(p - targ) > 2.0f;
    }

	// Update is called once per frame
	void Update () {

        //if (false) return;
        Vector3 Pos = transform.localPosition;
        //if (true)
        //{
        Pos.x = Pos.x * 0.8f + ((Num - resultPanelMove.GetShowData) * (float)(ObjSpace)) * (0.8f) * 0.2f;
            if (FManager.IsFlick())
            {
                if (IsMoveOk()) Pos.x = ((Num - resultPanelMove.GetShowData) * (float)(ObjSpace)) * (0.8f) + (resultPanelMove.GetMousePos.x - FManager.FlickStartPos.x) * 0.25f;
            }
        //}
        transform.localPosition = Pos;

	}
}
