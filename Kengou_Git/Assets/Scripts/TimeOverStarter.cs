using UnityEngine;
using System.Collections;

public class TimeOverStarter : MonoBehaviour {

	StaticScript _staticScript;

	protected StaticScript staticScript
	{
		get
		{
			if (!_staticScript)
				_staticScript = GameObject .FindGameObjectWithTag( "StaticScript" ) .GetComponent<StaticScript>();
			return _staticScript;
		}
	}

	[SerializeField]
	DynamicRenderTexture dynamicRenderTexture;

	bool IsRun = false;

	public bool IsRuned
	{
		get
		{
			return IsRun;
		}
	}

	bool IsPrinted = false;

	bool IsSwitch = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (staticScript .goalstarter .IsRuned)
			return;
		if (staticScript .gameOverStarter .IsRuned)
			return;
		if (!IsRun)
		{
			if(staticScript.timeLimit.IsTimeOver)
			{
				staticScript .actionTymingManager .Disable();
				staticScript .touchPos .gameObject .SetActive( false );
				staticScript .gameOverSystem .TimeOver();
				staticScript .playerSE .TimeOver();
				staticScript .rhythmManager .ForceStop();
				
				staticScript .bulletResultManager .GameOver();
				IsRun = true;
			}
		}
		else
		{
			staticScript .bgmData .MulSpeed(.0f);
			if (!IsSwitch && IsPrinted)
			{
				staticScript .fulScreenMesh .CreateMesh();
				staticScript .fulScreenMesh .SetTexture( dynamicRenderTexture .renderTexture );
				staticScript .fulScreenMesh .transform .parent = null;
				staticScript .markerCanvas .gameObject .SetActive( false );
				staticScript .gameOverSystem .Run();
				staticScript .uiCanvas .gameObject .SetActive( false );
				Camera .main .enabled = false;
				staticScript .gameOverSystem .MyCamera .enabled = true;
				IsSwitch = true;
			}
			if(IsSwitch)
			{
				staticScript .fulScreenMesh .CreateMesh();
				staticScript .fulScreenMesh .SetTexture( dynamicRenderTexture .renderTexture );
				foreach (CanvasRenderer renderer in staticScript .uICollection .renderers)
				{
					renderer .SetAlpha(.0f);
				}
	
			}
			if (!IsPrinted)
			{
				dynamicRenderTexture .Print();
				IsPrinted = true;
			}

		}
	}
}
