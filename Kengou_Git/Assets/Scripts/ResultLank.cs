using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultLank : MonoBehaviour {
    [SerializeField]
    Sprite[] sprites;
    Image img;

    int rank;
    int Showrank;
	// Use this for initialization
	void Start () {
        img = this.GetComponent<Image>();
	}

    public void SetRank(int Rank) {
        rank = Rank;
    }
    void SetRankSprite() {
        if (rank != Showrank)
        {
            img.sprite = sprites[rank];
            Showrank = rank;
        }
    }

    public void SetRankToStageSelect(int Score, int Stage)
    {
        rank = GetRank( Score, Stage);
    }

    public int GetRank(int Score, int Stage)
    {
        switch (Stage)
        {
            case 0:
                if (Score > 15000) return 0;
                else if (Score > 12000) return 1;
                else if (Score > 4000) return 2;
                break;
            case 1:
                if (Score > 60000) return 0;
                else if (Score > 30000) return 1;
                else if (Score > 15000) return 2;
                break;
            case 2:
                if (Score > 120000) return 0;
                else if (Score > 80000) return 1;
                else if (Score > 30000) return 2;
                break;
        }
        return 3;
    }

	// Update is called once per frame
	void Update () {
        SetRankSprite();
	}
}
