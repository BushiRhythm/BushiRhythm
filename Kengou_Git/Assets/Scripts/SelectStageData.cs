using UnityEngine;
using System.Collections;

public class SelectStageData : MonoBehaviour {
    [SerializeField]
    int Stage = 0;
    [SerializeField]
    bool MoveFlag = false;

    FlickManager _FManager;
    FlickManager FManager
    {
        get
        {
            if (!_FManager) _FManager = FindObjectOfType<FlickManager>();
            return _FManager;
        }
    }

    SelectMainMove _selectMainMove;
    SelectMainMove selectMainMove
    {
        get
        {
            if (!_selectMainMove) _selectMainMove = FindObjectOfType<SelectMainMove>();
            return _selectMainMove;
        }
    }

    StageSelectMain _SelectMain;

    StageSelectMain SelectMain
    {
        get
        {
            if (!_SelectMain)
                _SelectMain = Object.FindObjectOfType<StageSelectMain>();
            return _SelectMain;
        }
    }

    // Use this for initialization
    public void Delete()
    {
        Destroy(this.gameObject);
    }
    // Use this for initialization
    public void StartButtonPush()
    {
        SelectMain.SelectOn(true);
    }

    float ImageSpace = 0.9f;
    float SpaceValue = 1.0f;

	// Update is called once per frame
    bool IsMoveOk()
    {
        float p = (selectMainMove.GetMousePos.x);
        float targ = ((Stage - SelectMain.selectStage) * (float)(834) / SpaceValue) * (ImageSpace) + (selectMainMove.GetMousePos.x - FManager.FlickStartPos.x) * 0.25f;
        return Mathf.Abs(p-targ) > 2.0f;
    }
	void Update () {
        if (MoveFlag == false) return;
        Vector3 Pos = transform.localPosition;
        if (!SelectMain.OnStageSelect) {
            Pos.x = Pos.x * 0.8f + ((Stage - SelectMain.selectStage) * (float)(834) / SpaceValue) * (ImageSpace) * 0.2f;
            if(FManager.IsFlick()){
                if (IsMoveOk()) Pos.x = ((Stage - SelectMain.selectStage) * (float)(834) / SpaceValue) * (ImageSpace) + (selectMainMove.GetMousePos.x - FManager.FlickStartPos.x) * 0.25f;
            }
        }
        transform.localPosition = Pos;
	}
}
