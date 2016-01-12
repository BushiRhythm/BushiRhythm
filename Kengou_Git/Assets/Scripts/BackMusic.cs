using UnityEngine;
using System.Collections;

public class BackMusic : MonoBehaviour {

    [SerializeField]
    GameObject FrontSE;


    [SerializeField]
    GameObject BackSE;

	private bool SEPlay = true;

     public void PlayFront()
    {
        Instantiate(FrontSE);
    }

    public void PlayBack()
    {
        Instantiate(BackSE);
    }


	RhythmManager _rhythmManager;

	RhythmManager rhythmManager
	{
		get
		{
			if (!_rhythmManager)
				_rhythmManager = Object .FindObjectOfType<RhythmManager>();
			return _rhythmManager;
		}
	}


	public void Stop()
	{
		SEPlay = false;
	}

	public void Play()
	{
		SEPlay = true;
	}

	public bool IsPlay
	{
		get
		{
			return SEPlay;
		}
	}

	void Update()
	{
		if (!IsPlay)
			return;
		if (rhythmManager .ReadyFixedRhythm .Tyming)
		{
			PlayFront();
		}
	}
}
