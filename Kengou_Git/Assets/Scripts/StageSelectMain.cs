using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageSelectMain : MonoBehaviour
{

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

    [SerializeField]
    Button[] StartButtons;

    OptionManager _optionManager;
    OptionManager optionManager
    {
        get
        {
            if (!_optionManager) _optionManager = FindObjectOfType<OptionManager>();
            return _optionManager;
        }
    }

    SelectMainMove _selectMainMove;
    SelectMainMove selectMainMove
    {
        get
        {
            if (!_selectMainMove) _selectMainMove = GetComponent<SelectMainMove>();
            return _selectMainMove;
        }
    }


    bool _OnStageSelect = false;

    public bool OnStageSelect
    {
        get { return _OnStageSelect; }
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
    void Start()
    {
        _OnStageSelect = false;
        DontDestroyOnLoad(this);

    }
    AsyncOperation async;

    StageSelectWhiteText _stageSelectWhiteText;
    StageSelectWhiteText stageSelectWhiteText
    {
        get
        {
            if (!_stageSelectWhiteText) _stageSelectWhiteText = StartEffect.GetComponent<StageSelectWhiteText>();
            return _stageSelectWhiteText;
        }
    }

    bool LoadScene = false;

    void SelectButtonSet()
    {
        for (int i = 0; i < StartButtons.Length; i++)
        {
            StartButtons[i].interactable = false;
        }
        StartButtons[SelectStage].interactable = true;
    }

    // Update is called once per frame
    int Wait = 0;
    void Update()
    {
        if (LoadScene) return;
        if (!OnStageSelect)
        {
            SelectButtonSet();
            SelectSound();
            return;
        }
        StopSound();
        if (!Instanced && stageSelectWhiteText.IsComplete())
        {
            StopSound();
            async = Application.LoadLevelAsync("LoadStage");
            Instanced = true;
            for (int i = 0; i < DeleteObject.Length; i++) if (DeleteObject[i]) DeleteObject[i].SetActive(false);
        }
        if (!Instanced) Wait++;
        if (Instanced && !LoadScene)
        {
            async.allowSceneActivation = async.progress >= 0.9f;
        }
    }

    public void SelectOn(bool Enabled)
    {
        if (!_OnStageSelect && selectMainMove.IsStartPush())
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
