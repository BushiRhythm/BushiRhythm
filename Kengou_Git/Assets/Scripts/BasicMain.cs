using UnityEngine;
using System.Collections;


using UnityEngine.UI;
public class BasicMain : MonoBehaviour {

	// Use this for initialization
    bool AllLoadSucsess = false;

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

    [SerializeField]
    float LoadingNum;

    AsyncOperation[] LoadData;
	void Start () {
        LoadData = new AsyncOperation[6];
        LoadData[0] = Application.LoadLevelAdditiveAsync("BasicMoveStage");
        LoadData[1] = Application.LoadLevelAdditiveAsync("BasicMusicManager");
        LoadData[2] = Application.LoadLevelAdditiveAsync("Frick");
        LoadData[3] = Application.LoadLevelAdditiveAsync("UI");
        LoadData[4] = Application.LoadLevelAdditiveAsync("Goal");
		LoadData[5] = Application .LoadLevelAdditiveAsync( "GameOver" );
        for (int i = 0; i < 6; i++) LoadData[i].allowSceneActivation = false;
	}
	
	// Update is called once per frame
    void Update()
    {
        LoadingNum = (LoadData[0].progress + LoadData[1].progress + LoadData[2].progress + LoadData[3].progress + LoadData[4].progress + LoadData[5].progress) / 6.0f;
        if (AllLoadSucsess)
        {
            return;
        }
        AllLoadSucsess = !(LoadData[0].progress < 0.9f && LoadData[1].progress < 0.9f &&
            LoadData[2].progress < 0.9f && LoadData[3].progress < 0.9f && LoadData[4].progress < 0.9f && LoadData[5].progress < 0.9f);
        if (AllLoadSucsess)
        {
            if (SelectMain) SelectMain.NotActive();
            for (int i = 0; i < 6; i++) LoadData[i].allowSceneActivation = true;
        }
	}
}
