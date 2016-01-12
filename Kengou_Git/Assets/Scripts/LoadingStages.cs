using UnityEngine;
using System.Collections;

public class LoadingStages : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}

    public StageSelectMain _SelectMain;
    public StageSelectMain SelectMain
    {
        get
        {
            if (!_SelectMain) _SelectMain = FindObjectOfType<StageSelectMain>();
            return _SelectMain;
        }
    }

    [SerializeField]
    GameObject CMR;

    bool flag = false;
    bool CDelete = false;

    AsyncOperation LA;

	// Update is called once per frame
	void Update () {
        if(!flag && SelectMain){
            
            LA = Application.LoadLevelAsync("Stage" + (SelectMain.selectStage + 1).ToString());
            flag = true;
        }
        if (flag)
        {
            CDelete = LA.progress >= 0.9f;
            LA.allowSceneActivation = CDelete;
            if (CDelete) SelectMain.CameraDelete();
            
        }
    }
}
