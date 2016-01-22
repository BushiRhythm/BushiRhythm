using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {

    public GameObject _Screen;
    public ScreenFadeout FScreen
    {
        get
        {
            return _Screen.GetComponent<ScreenFadeout>();
        }
    }

    ResultData _ResultData;
    ResultData resultData
    {
        get
        {
            if (!_ResultData)
                _ResultData = Object.FindObjectOfType<ResultData>();
            return _ResultData;
        }
    }

    OptionManager _optionManager;
    OptionManager optionManager
    {
        get
        {
            if (!_optionManager)
                _optionManager = Object.FindObjectOfType<OptionManager>();
            return _optionManager;
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

    BulletResultManager _bulletResultManager;

    public BulletResultManager bulletResultManager
    {
        get
        {
            if (!_bulletResultManager)
                _bulletResultManager = FindObjectOfType<BulletResultManager>();
            return _bulletResultManager;
        }
    }

    ResultLank _RRank;
    ResultLank RRank {
        get {
            if (!_RRank) _RRank = FindObjectOfType<ResultLank>();
            return _RRank;
        }
    }


    [SerializeField]
    int[] BulletRank;
    [SerializeField]
    NumberParent[] ValueTextObject;
    [SerializeField]
    NumberParent[] ScoreTextObject;



    [SerializeField]
    NumberParent DefeatValueObject;
    [SerializeField]
    NumberParent DefeatScoreObject;

    [SerializeField]
    bool GokuSwitch = false;
    [SerializeField]
    GameObject[] SwitchingUnSet;
    [SerializeField]
    GameObject SwitchingMoveObject;
    [SerializeField]
    Vector3 SwitchingMoveValue;

    [SerializeField]
    NumberParent MinValueObject;
    [SerializeField]
    NumberParent SecValueObject;
    [SerializeField]
    NumberParent mScValueObject;

    [SerializeField]
    NumberParent TimeScoreObject;
    [SerializeField]
    NumberParent TimeScoreObject2;


    [SerializeField]
    NumberParent FullScore;
    [SerializeField]
    NumberParent FullScore2;

    int[] BulletScore = { 100, 80, 30, 20, -20 };
    // Use this for initialization
    static int MissCountID = 4;
    int Select = 0;
    bool Instanced = false;
    int TotalScore = 0;
    void Start()
    {
        Instanced = false;
        if (bulletResultManager && resultData)
        {
            for (int i = 0; i < 4; i++)
                ValueTextObject[i].SetNum(bulletResultManager.GetCount(i));
            for (int i = 0; i < 4; i++)
                ScoreTextObject[i].SetNum(bulletResultManager.GetCount(i) * BulletScore[i]);

            ValueTextObject[MissCountID].SetNum(bulletResultManager.GetMissCount());
            ScoreTextObject[MissCountID].SetNum(bulletResultManager.GetMissCount() * BulletScore[4]);

            DefeatValueObject.SetNum(bulletResultManager.GetEnemyKill());
            DefeatScoreObject.SetNum(bulletResultManager.GetEnemyKill() * 500);

            float CT = resultData.ClearTime;
            float CS = resultData.FinalScore;
            MinValueObject.SetNum((int)Mathf.Floor(CT / 60f));
            SecValueObject.SetNum((int)Mathf.Floor(CT % 60f));
            mScValueObject.SetNum((int)Mathf.Floor(CT % 1 * 10));
    
            TimeScoreObject.SetNum(1);
    
            int ScoreBoost = ResultTimeSet();
            TimeScoreObject.SetNum(ScoreBoost % 10);
            TimeScoreObject2.SetNum((ScoreBoost / 10) + 1);
    
            for (int i = 0; i < 3; i++)
                CS = CS + bulletResultManager.GetCount(i) * BulletScore[i];
            CS = CS + bulletResultManager.GetEnemyKill() * 500;
            CS = CS * (1.0f + ((float)ScoreBoost * 0.1f));
    
            FullScore.SetNum((int)CS);
            FullScore2.SetNum((int)CS);
            optionManager.SetHighScore(optionManager.GetStage(),(int)CS);
            TotalScore = (int)CS;
            RRank.SetRank(GetRank(TotalScore));
        }

        if (!GokuSwitch) {
            for (int i = 0; i < SwitchingUnSet.Length; i++) {
                SwitchingUnSet[i].SetActive(false);
            }
            SwitchingMoveObject.transform.position += SwitchingMoveValue;
        }
        
	}
    int GetRank(int CS)
    {
        if (!SelectMain) return 0;
        switch (SelectMain.selectStage)
        {
            case 0:
                if (CS > 15000) return 0;
                else if (CS > 12000) return 1;
                else if (CS > 4000) return 2;
                break;
            case 1:
                if (CS > 60000) return 0;
                else if (CS > 30000) return 1;
                else if (CS > 15000) return 2;
                break;
            case 2:
                if (CS > 120000) return 0;
                else if (CS > 80000) return 1;
                else if (CS > 30000) return 2;
                break;
        }
        return 3;
    }
    int ResultTimeSet()
    {
        if (!SelectMain) return 0;
        float CT = resultData.ClearTime;
        switch (SelectMain.selectStage)
        {
            case 0:
                if (CT < 23.0f) return 20;
                else if (CT < 33.0f) return 15;
                else if (CT < 73.0f) return 10;
                break;
            case 1:
                if (CT < 37.0f) return 20;
                else if (CT < 52.0f) return 15;
                else if (CT < 67.0f) return 10;
                break;
            case 2:
                if (CT < 62.0f) return 20;
                else if (CT < 77.0f) return 15;
                else if (CT < 87.0f) return 10;
                break;
        }
        return 0;
    }

    public void SelectRetry()
    {
        Select = 0;
    }
    public void SelectEnd()
    {
        Select = 1;
    }
	// Update is called once per frame
    void Update()
    {
        if (FScreen.IsFadeComplete() && !Instanced)
        {
            if (Select == 0)
            {
                Application.LoadLevelAsync("LoadStage");
            }
            if (Select == 1)
            {
                if (SelectMain) SelectMain.Delete();
                Application.LoadLevelAsync("StageSelect");
            }
            Instanced = true;
        }
	}
}
