using UnityEngine;
using System.Collections;

public class ScreenFadeIn : MonoBehaviour {

	[SerializeField]
	float Alpha = 0.0f;
	[SerializeField]
	float AlphaSpeed = 0.1f;
	[SerializeField]
	bool Enable = false;
	[SerializeField]
	Color col;
	
	public void FadeEnable(){
		Enable = true;
	}
	public bool IsFadeComplete(){
		return (Alpha >= 1.0f);
	}
	
	// Use this for initialization
	void Start () {
		Alpha = 0;
		GetComponent<CanvasRenderer> ().SetAlpha(Alpha);
	}
	
	// Update is called once per frame
	void Update () {
		if (!Enable)
			return;
		if (Alpha < 1.0f)
			Alpha = Mathf.Min (Alpha - AlphaSpeed, 1.0f);
		GetComponent<CanvasRenderer> ().SetAlpha(Alpha);
	}
}
