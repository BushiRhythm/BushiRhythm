using UnityEngine;
using System.Collections;

public class FrontMoverManager : MonoBehaviour {

    Rigidbody _rigidBody;
    
    Rigidbody rigidBody
    {
        get
        {
            if (!_rigidBody)
            {
                _rigidBody = GetComponent<Rigidbody>();
            }
            return _rigidBody;

        }
    }


	StaticScript _staticScript;

	StaticScript staticScript
	{
		get
		{
			if (!_staticScript)
				_staticScript = GameObject .FindGameObjectWithTag( "StaticScript" ) .GetComponent<StaticScript>();
			return _staticScript;
		}
	}
    //  前にセットした位置
    Vector3 previousSetPos = new Vector3(0,0,0);
    Vector3 startPos = new Vector3(0, 0, 0);
    //  移動時間
    float moveTime = 0;
    float timer = -1;

    float progress = 1.0f;

    //移動しているかどうか
    bool moveFlag = false;
    [SerializeField, HeaderAttribute("一回の移動距離")]
    private float moveDistance = 10.0f;

    private float nowMoveDistance;

    [SerializeField, HeaderAttribute("クリア距離(歩数)")]
    private int goalPos = 10;

	public int GoalPos
	{
		get
		{
			return goalPos;
		}
	}

    private int stepCount = 0;

	public int StepCount
	{
		get
		{
			return stepCount;
		}
	}


    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
        nowMoveDistance = moveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(staticScript.frontMoverManagerData != null)
		{
			moveDistance = staticScript .frontMoverManagerData .moveDistance;
			goalPos = staticScript .frontMoverManagerData .goalPos;
			goalPos += staticScript .enemyAdjustStep .Step;

		}
        
        //  進行度が１００％未満なら
        if (progress < 1.0f)
        {
            progress = timer / moveTime;
            Move();
            timer += Time.deltaTime;


        }
        else if (moveFlag)
        {

            //  進行度１００％
            progress = 1.0f;
            Move();
            moveFlag = false;
            stepCount++;

        }

        
    }

	public bool  IsGoal
	{
		get
		{
			return stepCount >= goalPos;
		}
	
	}

	public int RestStep
	{
		get
		{
			int rest = goalPos - stepCount  ;
			if (rest < 0)
				rest = 0;
			return rest;
		}

	}

    private void Move()
    {
        Vector3 forward = transform.forward - Vector3.up * Vector3.Dot(transform.forward, Vector3.up);

        if (moveDistance != nowMoveDistance)
        {
            Vector3 vec = previousSetPos - startPos;
            float leng = vec.magnitude;
            leng /= nowMoveDistance;
            previousSetPos = startPos + forward * moveDistance * leng;

        }

        Vector3 pos = previousSetPos;
        Vector3 pos2,pos3;
        pos = previousSetPos + forward * moveDistance * progress;
        pos2 = startPos + forward * moveDistance * stepCount + forward * moveDistance * progress;
        pos3 = pos2 - pos;
        pos += pos3 * progress;

        transform.position = pos;

        nowMoveDistance = moveDistance;
    }


    public void SetFrontMoveTime(float time)
    {
        moveFlag = true;
        moveTime = time;
        timer = 0;
        progress = 0;
        previousSetPos = transform.position;
        
    }

    public Vector3 GetPos
    {
        get { return transform.position; }

    }

	public Vector3 GetPosFromStep(int Step)
	{
		return startPos + transform .forward * nowMoveDistance * Step;
	}

	public Vector3 GetPosFromStep( float Step )
	{
		return startPos + transform .forward * nowMoveDistance * Step;
	}


    public float GetProgress
    {
        get { return progress; }
    }

    public int GetStep
    {
        get { return stepCount; }
    }

}
