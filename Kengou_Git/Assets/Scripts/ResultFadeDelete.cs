using UnityEngine;
using System.Collections;

public class ResultFadeDelete : MonoBehaviour {

    ResultDataShow _RDShow;

    public ResultDataShow RDShow
    {
        get
        {
            if (!_RDShow)
                _RDShow = FindObjectOfType<ResultDataShow>();
            return _RDShow;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (RDShow.IsMoveComplete()) Destroy(this.gameObject);
	}
}
