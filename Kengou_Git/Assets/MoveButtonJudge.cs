using UnityEngine;
using System.Collections;

public class MoveButtonJudge : MonoBehaviour {

    ActionTymingManager _actionManager;
    ActionTymingManager actionManager
    {
        get
        {
            if (!_actionManager) _actionManager = FindObjectOfType<ActionTymingManager>();
            return _actionManager;
        }
    }

    ActionSetter _actionSetter;
    ActionSetter actionSetter
    {
        get
        {
            if (!_actionSetter) _actionSetter = FindObjectOfType<ActionSetter>();
            return _actionSetter;
        }
    }

    RhythmManager _rhythmManager;

    RhythmManager rhythmManager
    {
        get
        {
            if (!_rhythmManager)
                _rhythmManager = Object.FindObjectOfType<RhythmManager>();
            return _rhythmManager;
        }

    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame

    bool ASSucsess = false;
    bool ASFailure = false;
    bool AMFailure = false;
    bool APerfect = false;

    public bool actionSetterIsStepActionSucsess
    {
        get
        {
            return ASSucsess;
        }
    }

    public bool actionSetterIsStepActionFailure
    {
        get
        {
            return ASFailure;
        }
    }

    public bool actionManagerIsFailure
    {
        get
        {
            return AMFailure;
        }
    }

    public bool IsActionPerfect
    {
        get
        {
            return APerfect;
        }
    }

    void Update()
    {
        if (actionSetter && actionManager) {
            ASSucsess = actionSetter.IsStepActionSuccess;
            ASFailure = actionSetter.IsStepActionFailure;
            AMFailure = actionManager.IsFailure();
        }
    }
}
