using UnityEngine;
using System.Collections;

public class OptionManager : MonoBehaviour {
    [SerializeField]
    bool LROption = false;
    int Stage = 0;

    [SerializeField]
    int[] Score;

    bool LR = false;


    public bool GetLR()
    {
        return LROption;
    }
    public int GetStage()
    {
        return Stage;
    }
    // Use this for initialization
    public void SetLROption(bool b)
    {
        LROption = b;

        int i = 0;
        if (LROption) i = 1;
        PlayerPrefs.SetInt("LROPtion", i);
        PlayerPrefs.Save();
    }

    public void SetStage(int s)
    {
        Stage = s;
    }

    public int GetScore(int s)
    {
        return Score[s];
    }



    public bool SetHighScore(int stage, int score)
    {
        string stg = "High";
        if      (stage == 0) stg = "High1";
        else if (stage == 1) stg = "High2";
        else if (stage == 2) stg = "High3";
        else return false;

        if (Score[stage] < score)
        {
            Score[stage] = score;
            PlayerPrefs.SetInt(stg, score);
            PlayerPrefs.Save();
            return true;
        }
        return false;


    }
	void Start () {
        Score = new int[3];
        DontDestroyOnLoad(this);

        Score[0] = PlayerPrefs.GetInt("High1", 0);
        Score[1] = PlayerPrefs.GetInt("High2", 0);
        Score[2] = PlayerPrefs.GetInt("High3", 0);

        int i = PlayerPrefs.GetInt("LROPtion", 0);
        if(i == 1)SetLROption(true);
	}
	
}
