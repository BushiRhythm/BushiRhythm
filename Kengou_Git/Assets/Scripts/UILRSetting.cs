using UnityEngine;
using System.Collections;

public class UILRSetting : MonoBehaviour {

	[SerializeField]
	//デフォルトでは右利き
	bool LRFlag = false;

	void LRChange(){
		LRFlag = !LRFlag;
	}
	public bool GetLRFlag(){
		return LRFlag;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
