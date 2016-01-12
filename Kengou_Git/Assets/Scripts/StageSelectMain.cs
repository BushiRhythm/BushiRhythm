using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageSelectMain : MonoBehaviour {

    [SerializeField]
    AudioSource[] SelectMusic;

	[SerializeField]
    int SelectStage = 0;
    int SelectSoundID = -1;
    [SerializeField]
    int StageMax = 3;
    [SerializeField]
    GameObject LButton;
    [SerializeField]
    GameObject RButton;
    [SerializeField]
    GameObject StartSound;
    [SerializeField]
    GameObject StartEffect;

    [SerializeField]
    GameObject[] DeleteObject;
    [SerializeField]
    GameObject DeleteObject2;
    [SerializeField]
    GameObject DeleteCamera;
    [SerializeField]
    GameObject DeleteCanvas;

    OptionManager _optionManager;
    OptionManager optionManager {
        get {
            if (!_optionManager) _optionManager = FindObjectOfType<OptionManager>();
            return _optionManager;
        }
    }

    bool _OnStageSelect = false;

	public bool OnStageSelect{
		get{return _OnStageSelect;}
	}
    public int selectStage
    {
        get { return SelectStage; }
    }

    bool Instanced = false;
    // Use this for initialization
    public void Delete()
    {
        Destroy(gameObject);
    }
    public void NotActive()
    {
        Invoke("_NotActive", 1.0f);
    }
    public void CameraDelete()
    {
        if (DeleteCamera) Destroy(DeleteCamera);
        if (DeleteCanvas) Destroy(DeleteCanvas);
    }
    public void _NotActive()
    {
        DeleteObject2.SetActive(false);
    }
	// Use this for initialization
	void Start () {
        _OnStageSelect = false;
        DontDestroyOnLoad(this);
        
    }
    AsyncOperation async;

    StageSelectWhiteText _SSWT;
    StageSelectWhiteText SSWT {
        get {
            if (!_SSWT) _SSWT = StartEffect.GetComponent<StageSelectWhiteText>();
            return _SSWT;
        }
    }

    bool LoadScene = false;

	// Update is called once per frame
    int Wait = 0;
    void Update()
    {
        if (LoadScene) return;
        if (!OnStageSelect) {
            SelectSound();
            return;
        }
        StopSound();
        if (!Instanced && SSWT.IsComplete())
        {
            StopSound();
            async = Application.LoadLevelAsync("LoadStage");
            Instanced = true;
            for (int i = 0; i < DeleteObject.Length; i++) if(DeleteObject[i])DeleteObject[i].SetActive(false);
        }
        if (!Instanced) Wait++;
        if (Instanced && !LoadScene)
        {
            async.allowSceneActivation = async.progress >= 0.9f;
        } 
	}

	public void SelectOn(bool Enabled){
        if (!_OnStageSelect)
        {
            optionManager.SetStage(SelectStage);
            _OnStageSelect = true;
            //StartSound = Instantiate(StartSound, transform.position, transform.rotation) as GameObject;
        }
    }

    public void SelectSound()
    {
        if (SelectStage != SelectSoundID)
        {
            StopSound();
            SelectMusic[SelectStage].Play();
            SelectSoundID = SelectStage;
        }
    }

    public void StopSound()
    {
        for (int i = 0; i < SelectMusic.Length; i++) SelectMusic[i].Stop();
    }

    public void NextStage()
    {
        if (SelectStage < StageMax && !OnStageSelect)
        SelectStage++;
    }
    public void BackStage()
    {
        if (SelectStage > 0 && !OnStageSelect)
            SelectStage--;
    }
}
