using UnityEngine;
using UnityEngine .EventSystems;
using System.Collections;

public class SceneMug : MonoBehaviour {
    Push _push;
    Push push
    {
        get
        {
            if(!_push)
            {
                _push = Object.FindObjectOfType<Push>();
            }
            return _push;
        }
    }

    MoveButton _moveButton;
    MoveButton moveButton
    {
        get
        {
            if (!_moveButton)
            {
                _moveButton = Object.FindObjectOfType<MoveButton>();
            }
            return _moveButton;
        }
    }

    RhythmManager _rhythmManager;

    RhythmManager rhythmManager
    {
        get
        {
        if(!_rhythmManager)
             _rhythmManager = Object.FindObjectOfType<RhythmManager>();
        return _rhythmManager;
        }

    }


    FrontMoverManager _frontMoverManager;
    FrontMoverManager frontMoverManager
    {
        get
        {
            if (!_frontMoverManager)
                _frontMoverManager = Object.FindObjectOfType<FrontMoverManager>();
            return _frontMoverManager;
        }

    }


    ActionTymingManager _actionTymingManager;

    ActionTymingManager actionTymingManager
    {
        get
        {
            if (!_actionTymingManager)
                _actionTymingManager = Object.FindObjectOfType<ActionTymingManager>();
            return _actionTymingManager;
        }

    }

    AudioSource _audioSource;
    AudioSource audioSource
    {
        get
        {
            if(!_audioSource)
            {
                _audioSource = GameObject.FindWithTag("MainMusic").GetComponent<AudioSource>();
            }
            return _audioSource;
        }

    }

    FlickManager _flickManager;
    FlickManager flickManager
    {
        get
        {
            if (!_flickManager)
                _flickManager = Object.FindObjectOfType<FlickManager>();
            return _flickManager;
        }

    }

    bool IsLoad = false;
	// Use this for initialization
	void Start () {
		//Application .targetFrameRate = 60;
	}
	
	// Update is called once per frame
	void Update () {
        if(!IsLoad)
        {
            Application.LoadLevelAdditive("UI");
            Application.LoadLevelAdditive("MoveStage");
            Application.LoadLevelAdditive("MusicManager");
            Application.LoadLevelAdditive("Frick");
			Application .LoadLevelAdditive("Goal" );

            IsLoad = true;

        }
        else
        {
            if (push.Bpush)
            {
                actionTymingManager.SetSlowAction();
            }
            if(actionTymingManager.IsSlowActiond())
                rhythmManager.Slow(16);


            //if (moveButton.Bpush)
            if (flickManager.IsMove())
            {
                actionTymingManager.SetStepAction();
            }
            if (actionTymingManager.IsStepActiond())
                frontMoverManager.SetFrontMoveTime(rhythmManager.OnTempoTime / 2);


			//if( rhythmManager.IsSlow)
			//{
			//	audioSource.volume = 0.3f;
			//}
			//else
			//{
			//	audioSource.volume = 0.6f;
			//}

        }

	}
}
