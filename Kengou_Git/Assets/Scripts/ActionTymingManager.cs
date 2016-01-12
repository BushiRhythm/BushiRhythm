using UnityEngine;
using System.Collections;

enum ACTION_PATTERN
{
    NOACTION,
    BLADE,
    STEP,
    SLOW,
	Failure,
}

public class ActionTymingManager : MonoBehaviour {

    //アクションを予約できる時間
    [SerializeField]
    float[] CanTime;

	//失敗扱いにする時間
	[SerializeField]
	float[] FailureTime;

	bool IsSwitch = false;

	public void Enable()
	{
		IsSwitch = true;
	}

	public void Disable()
	{
		IsSwitch = false;
	}

	float CurRestTime;

	//極は割り込み可能
	bool SlowActionSet;

	//極は割り込み可能
	bool SlowActiond;

	//最後にアクションを実行したタイミング
	int SlowLastActionTyming = -1; 


    //アクションを予約可能
    //bool CanSetAction;

	bool CanSetAction(ACTION_PATTERN pattern)
	{
		return CurRestTime < CanTime[(int)pattern];
	}

	//bool FailureAction;

	bool FailureAction( ACTION_PATTERN pattern )
	{
		return !( CurRestTime < CanTime[(int)pattern] ) && (CurRestTime < CanTime[(int)pattern] + FailureTime[(int)pattern]);
	}

    //実行された瞬間
    bool Actiond;

    //現在のタイミング
    int CurTyming;

    //最後にアクションを実行したタイミング
    int LastActionTyming = -1; 

    //アクションの種類
    ACTION_PATTERN actionPattern;

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
	void Start () {
        actionPattern = ACTION_PATTERN.NOACTION;
    }

    private bool IsActiond(ACTION_PATTERN action)
    {
        return 
            (actionPattern == action
			&& Actiond && IsSwitch );
    }

    //  斬アクションが実行されたか
    public bool IsBladeActiond() 
    {
        return IsActiond(ACTION_PATTERN.BLADE);
    }

    //  進アクションが実行されたか
    public bool IsStepActiond()
    {
        return IsActiond(ACTION_PATTERN.STEP);
    }
    //  スローアクションが実行されたか
    public bool IsSlowActiond()
    {
		return SlowActiond && IsSwitch;
    }

	public bool IsFailured()
	{
		return IsActiond( ACTION_PATTERN .Failure );
	}

	public bool IsFailure()
	{
		return actionPattern == ACTION_PATTERN .Failure;
	}

	public bool IsStepAction()
	{
		return actionPattern == ACTION_PATTERN .STEP;
	}


    //  アクションを予約
    private bool SetAction(ACTION_PATTERN action)
    {
		if (!CanSetAction( action ))
            return false;

		if (!IsSwitch)
			return false;

        //すでにそのタイミングでアクションが実行されていればfalse
        if (CurTyming == LastActionTyming)
            return false;

        //すでにほかのアクションが入っていればfalse
        if (actionPattern != ACTION_PATTERN.NOACTION)
            return false;

        actionPattern = action;
        return true;
    }
    
    public bool SetBladeAction()   //失敗するとfalse
    {
		return SetAction( ACTION_PATTERN .BLADE );	
    }

    public bool SetStepAction()     //失敗するとfalse
    {
        if(SetAction(ACTION_PATTERN.STEP))
		{
			return true;
		}
		if (FailureAction( ACTION_PATTERN .STEP ))
		{
			if (!IsSwitch)
				return false;

			//すでにそのタイミングでアクションが実行されていればfalse
			if (CurTyming == LastActionTyming)
				return false;

			//すでにほかのアクションが入っていればfalse
			if (actionPattern != ACTION_PATTERN .NOACTION)
				return false;

			actionPattern = ACTION_PATTERN .Failure;
		}
		return false;
			
    }

    public bool SetSlowAction()     //失敗するとfalse
    {
		if (!CanSetAction( ACTION_PATTERN .SLOW ))
			return false;

		if (!IsSwitch)
			return false;

		//すでにそのタイミングでアクションが実行されていればfalse
		if (CurTyming == SlowLastActionTyming)
			return false;

		//すでにほかのアクションが入っていればfalse
		if (SlowActionSet)
			return false;

		SlowActionSet = true;
		return true;
    }

	// Update is called once per frame
	void Update () {

        //現在のタイミングを取得
        int Tyming = (int)(rhythmManager.FixedRhythm.Pos + 0.5f);

        //見ているタイミングが変化した場合
        //アクションをリセット
        if (Tyming != CurTyming)
        {
            actionPattern = ACTION_PATTERN.NOACTION;
			SlowActionSet = false;
        }
        CurTyming = Tyming;

		CurRestTime = Mathf .Abs( rhythmManager .FixedRhythm .Time - ( CurTyming * rhythmManager .OnTempoTime ) );
        //操作を受け付けるタイミングなのか
		//if (Mathf.Abs(rhythmManager.FixedRhythm.Time - (CurTyming * rhythmManager.OnTempoTime)) < CanTime)
		//{
		//	CanSetAction = true;
		//	FailureAction = false;
		//}
		//else if (Mathf .Abs( rhythmManager .FixedRhythm .Time - ( CurTyming * rhythmManager .OnTempoTime ) ) < CanTime + FailureTime)
		//{
		//	CanSetAction = false;
		//	FailureAction = true;
		//}
		//else
		//{
		//	CanSetAction = false;
		//	FailureAction = false;
		//}

        //Actiondは1フレームしかtrueにできない
        Actiond = false;
		SlowActiond = false;

        //アクションが予約されているときは実行できるか確認
        if (LastActionTyming != CurTyming && 
            actionPattern != ACTION_PATTERN.NOACTION)
        {
            //タイミングを通過した後か
            bool IsTymingStay =
                (CurTyming <= rhythmManager.FixedRhythm.Pos);

            switch (actionPattern)
            {
                case ACTION_PATTERN.BLADE:
                    //無条件で実行
                    Actiond = true;
                    break;
                case ACTION_PATTERN.STEP:
                    //タイミング通過後なら実行
                    if (IsTymingStay)
                    {
                        Actiond = true;
                    }
                    break;
                case ACTION_PATTERN.SLOW:
                    //タイミング通過後なら実行
                    if (IsTymingStay)
                    {
                        Actiond = true;
                    }
                    break;
				case ACTION_PATTERN .Failure:
					//無条件で実行
					Actiond = true;
					break;
                default:
                    break;
            }


            if(Actiond)
            {
                LastActionTyming = CurTyming;
            }


        }
		  if (SlowLastActionTyming != CurTyming && 
            SlowActionSet)
		  {
			  bool IsTymingStay =
				( CurTyming <= rhythmManager .FixedRhythm .Pos );	
			  if (SlowLastActionTyming != CurTyming && SlowActionSet)
			{
				if(IsTymingStay)
				{
					SlowActiond = true;
				}
			
			}
			  if (SlowActiond)
			{
				SlowLastActionTyming = CurTyming;
			}

		  }







	}
}
