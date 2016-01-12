using UnityEngine;
using System.Collections;

public class GameOverMain : MonoBehaviour {
	
	public GameObject _Screen;
	public ScreenFadeout FScreen{
		get{
			return _Screen.GetComponent<ScreenFadeout>();
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

	int Select = 0;
	public void SelectRetry(){
		Select = 0;
	}
	public void SelectEnd(){
		Select = 1;
	}
	// Use this for initialization
	void Start () {
        Instanced = false;
	}

    // Update is called once per frame
    bool Instanced = false;
	void Update () {
        if (FScreen.IsFadeComplete() && !Instanced)
        {
            if (Select == 0)
            {
                Application.LoadLevelAsync("LoadStage");
            }
            if (Select == 1)
            {
                if(SelectMain)SelectMain.Delete();
                Application.LoadLevelAsync("StageSelect");
            }
            Instanced = true;
		}
	}
}
