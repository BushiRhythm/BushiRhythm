using UnityEngine;
using System.Collections;

public struct Judge
{
    private float _ScoreMultiple;
    private float _Time;

    public void Setting(Keyframe frame)
    {
         _ScoreMultiple = frame.value;
         _Time = -(frame.time);
    }

    public float ScoreMultiple
    {
        get
        {
            return _ScoreMultiple;
        }
    }

    public float Time
    {
        get
        {
            return _Time;
        }
    }


}

public class JudgeBar : MonoBehaviour {

    [SerializeField]
    AnimationCurve Curve;

    Judge[] judges;

	public int MaxResultRank;

    int _BestJudgeIndex;

	// Use this for initialization
	void Start () {
		Setting();
	}

	public void Setting()
	{
		//ジャッジ数の指定
		judges = new Judge[Curve .length];

		//ジャッジの設定 & ベストタイミングの検索
		_BestJudgeIndex = -1;

		for (int i = 0;i < Curve .length;i++)
		{
			judges[i] .Setting( Curve[Curve .length - 1 - i] );
			//ベストタイミングの検索
			if (_BestJudgeIndex == -1)
			{
				if (judges[i] .Time >= .0f)
					_BestJudgeIndex = i;
			}
		}
        
	}
	
	// Update is called once per frame
	void Update () {
        //float Value = Curve[0].value;
        //float time = Curve[0].time;
	}

    public int NumJudge
    {
        get
        {
            return Curve.length;
        }
    }

    public int BestJudgeIndex
    {
        get
        {
            return _BestJudgeIndex;
        }
    }

    public int Judge(int index,float time)
    {
        if (index < 1 || NumJudge < index)
            return index;

        if(judges[index -1].Time >= time)
        {
            return index - 1;
        }
        else
        {
            return index;
        }
    }

	public float GetScoreMultiple(int index)
	{
        if (index < 1 || NumJudge < index)
            return .0f;

		return judges[index] .ScoreMultiple;

	}
}
