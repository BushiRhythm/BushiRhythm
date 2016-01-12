using UnityEngine;
using System.Collections;

public class fingerLR : MonoBehaviour {


    OptionManager _optionManager;

    OptionManager optionManager
    {
        get
        {
            if (!_optionManager)
                _optionManager = FindObjectOfType<OptionManager>();
            return _optionManager;
        }

    }


	// Use this for initialization
	void Start () {
	
        if(optionManager.GetLR())
        {
            Vector3 scale = this.transform.localScale;
            scale.x = -scale.x;
            this.transform.localScale = scale;
        }else
        {

        }

        

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
