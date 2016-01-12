using UnityEngine;
using System.Collections;

public class TitleButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(this.GetComponent<FlickManager> ().IsFlick()){
			this.GetComponent<Animator> ().SetTrigger ("deathflag");
			Invoke ("Push",1.0f);
		}
	}
	public void Push(){
		Application.LoadLevel ("StageSelect");
	}
}
