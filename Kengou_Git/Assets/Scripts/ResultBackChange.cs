using UnityEngine;
using System.Collections;

public class ResultBackChange : MonoBehaviour {

    [SerializeField]
    Sprite[] StageBack;

    public StageSelectMain _SelectMain;
    public StageSelectMain SelectMain
    {
        get
        {
            if (!_SelectMain) _SelectMain = FindObjectOfType<StageSelectMain>();
            return _SelectMain;
        }
    }

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = StageBack[SelectMain.selectStage];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
