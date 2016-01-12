using UnityEngine;
using System.Collections;

public class ScreenFadeout : MonoBehaviour {
	
	[SerializeField]
	float Alpha = 0.0f;
	[SerializeField]
	float AlphaSpeed = 0.1f;
	[SerializeField]
	bool Enable = false;
	[SerializeField]
	Color col;
	[SerializeField,Range(.0f,1.0f)]
	float limit = 1.0f;

	public void FadeEnable(){
		Enable = true;
	}
	public bool IsFadeComplete(){
		return (Alpha >= limit);
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
		if (Alpha < limit)
            Alpha = Mathf.Min(Alpha + AlphaSpeed * Time.deltaTime, limit);
		GetComponent<CanvasRenderer> ().SetAlpha(Alpha);
	}
}
