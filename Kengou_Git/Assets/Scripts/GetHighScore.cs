using UnityEngine;
using System.Collections;

public class GetHighScore : MonoBehaviour {


    [SerializeField]
    ResultLank result;

    [SerializeField]
    int number = 0;
    [SerializeField]
    NumberParent _Np;
    NumberParent Np {
        get
        {
            if (!_Np) _Np = GetComponent<NumberParent>();
            return _Np;
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

    bool SetScore = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
    void Update()
    {
        if (!SetScore && Np) {
            Np.SetNum(optionManager.GetScore(number));
            result.SetRankToStageSelect(optionManager.GetScore(number), number);
            SetScore = true;
        }
	}
}
