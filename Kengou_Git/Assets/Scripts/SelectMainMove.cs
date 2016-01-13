using UnityEngine;
using System.Collections;

public class SelectMainMove : MonoBehaviour
{

    bool PushFlag = false;

    FlickManager _FlickManager;
    FlickManager FlickManager
    {
        get
        {
            if (!_FlickManager) _FlickManager = GetComponent<FlickManager>();
            return _FlickManager;
        }
    }
    StageSelectMain _stageSelectMain;
    StageSelectMain stageSelectMain
    {
        get
        {
            if (!_stageSelectMain) _stageSelectMain = FindObjectOfType<StageSelectMain>();
            return _stageSelectMain;
        }
    }

    // Use this for initialization
    void Start()
    {

    }
    [SerializeField]
    Vector3 Test;


    Vector3 MousePos;
    public Vector3 GetMousePos
    {
        get
        {
            return MousePos;
        }
    }


    Vector3 FStart;
    Vector3 FEnd;
    public Vector3 FlickVector()
    {
        Vector3 v = (FEnd - FStart);

        Test = v;
        return v;
    }
    public bool IsStartPush()
    {
        return FlickVector().x > -2.0f && FlickVector().x < 2.0f;
    }

    bool FlickComplete = false;
    // Update is called once per frame
    void Update()
    {
        if (FlickManager.IsEndFlick())
        {
            if (FEnd.y > Screen.height * 0.25f)
            {
                if (FlickVector().x > 12.5f)
                {
                    if (!FlickComplete) stageSelectMain.BackStage();
                }
                else if (FlickVector().x < -12.5f)
                {
                    if (!FlickComplete) stageSelectMain.NextStage();
                }
            }
            FStart.x = .0f;
            FStart.y = .0f;
            FStart.z = .0f;
            FEnd.x = .0f;
            FEnd.y = .0f;
            FEnd.z = .0f;
        }
        else
        {
            if (FlickManager.IsFlick())
            {
                FStart = FlickManager.FlickStartPos;
                FEnd = FlickManager.FlickEndPos;
            }
        }
        MousePos = Input.mousePosition;
        Test = (FEnd - FStart);
    }
}
