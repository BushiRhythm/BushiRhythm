using UnityEngine;
using UnityEngine .UI;
using System.Collections;

public class GameOverUI : MonoBehaviour {

	BulletResultManager _bulletResultManager;

	public BulletResultManager bulletResultManager
	{
		get
		{
			if (!_bulletResultManager)
				_bulletResultManager = FindObjectOfType<BulletResultManager>();
			return _bulletResultManager;
		}
	}

	Text _text;

	Text text
	{
		get
		{
			if (!_text)
				_text = GetComponent<Text>();
			return _text;
		}

	}

	
	public GameObject _FadeOutObj;
	ScreenFadeout _screenFadeOut;
	
	public ScreenFadeout screenFadeOut
	{
		get
		{
			if (!_screenFadeOut)
				_screenFadeOut = _FadeOutObj.GetComponent<ScreenFadeout>();
			return _screenFadeOut;
		}
	}
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		text.enabled = bulletResultManager.IsGameOver;
        if (bulletResultManager.IsGameOver) {
            screenFadeOut.FadeEnable();
            _FadeOutObj.SetActive(true);
        }
	}
}
