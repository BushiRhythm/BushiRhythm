using UnityEngine;
using System.Collections;

public class DynamicRenderTexture : MonoBehaviour {

	public RenderTexture renderTexture;
	// Use this for initialization
	void Start () {
		renderTexture = new RenderTexture( Screen .width , Screen .height , 24);
	}

	bool IsPrinted;

	public void Print()
	{
		IsPrinted = true;
		Camera .main .targetTexture = renderTexture;
	}
	
	// Update is called once per frame
	void Update () {
		if (IsPrinted)
		{
			Camera .main .targetTexture = null;
			IsPrinted = false;
	
		}
	
	}


}
