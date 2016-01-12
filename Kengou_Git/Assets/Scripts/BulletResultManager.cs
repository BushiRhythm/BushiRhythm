using UnityEngine;
using System.Collections;

//斬った結果を3時限の位置、二次元の位置、実際のタイミング、結果で表示

public class BulletResultData //譜面の結果の最小単位データ
{
    private Vector3     _WorldPos;
	private Vector3		_MarkerWorldPos;		
    private Vector2     _ScreenPos;
    private int         _ResultRank;     //0がベスト正の値が早い負の値が遅い
    private int         _Score;				//タイミングだけではスコアは分からないので保存
	private bool		_Miss;
	public bool		IsCanCounterEmit;
	public bool		IsCounterEmited;
	public bool		IsSlowly;
	public bool		IsEarly;


   


    public Vector3	WorldPos {		get		{	return _WorldPos;		}	}
	public Vector3 MarkerWorldPos
	{
		get
		{
			return _MarkerWorldPos;
		}
	}
    public Vector2	ScreenPos{	get		{	return _ScreenPos;		}	}
    public int			ResultRank{	get		{	return _ResultRank;	}	}
    public int			Score		{	get		{	return	_Score;			}	}
	public bool			Miss			{	get		{	return	_Miss;			}	}


	public void Setting( Marker marker , Vector3 MarkerBullet , bool Miss , bool isCanCounterEmit  , StaticScript staticScript )
	{
		//ワールド座標の代入
		_WorldPos = MarkerBullet;

		_MarkerWorldPos = marker.transform.position;		

		//スクリーン座標の代入
		_ScreenPos = marker .PosOnScreen;

		JudgeBar UseBar;
		int UseIndex;

		if(staticScript .rhythmManager.IsSlow)
		{
			UseBar =  staticScript .bulletResultManager .SlowJudgeBar;
			UseIndex = marker .SlowJudgeIndex;
		}
		else
		{
			
			UseBar =  staticScript .bulletResultManager .NormalJudgeBar;
			UseIndex  = marker.JudgeIndex;
		}

		//ResultRankの代入
		_ResultRank = Mathf .Abs( UseIndex - UseBar .BestJudgeIndex ) + staticScript .bulletResultManager .MaxRank - UseBar .MaxResultRank;

		IsSlowly = ( _ResultRank ) < 0;

		IsEarly = ( _ResultRank ) > 0;

		//スコアの代入
		_Score = (int)( UseBar .GetScoreMultiple( UseIndex ) * 100 );

		_Miss = Miss;

		IsCanCounterEmit = isCanCounterEmit;
		
		IsCounterEmited = false;
	}
}

//class BulletResultDynamicData	　//ゲーム上に一定時間表示するためのデータ
//{
//	private BulletResultData	_BaseData;
//	private int						_RestTime;	//残り表示時間



//}

//今は習慣的な履歴のみを保存
public class BulletResultManager : MonoBehaviour {

	int[] NumBullet;

	int[] NumSlowlyBullet;

	int[] NumEarlyBullet;

	[SerializeField]
	JudgeBar _normalJudgeBar;

	[SerializeField]
	JudgeBar _slowJudgeBar;

	int maxRank;

	int MissCount;

	int enemyKill;

	private ArrayList _PulseResultData; //瞬間的な結果データ

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

	public void GameOver()
	{
		_isGameOver = true;
	}

	public int MaxRank
	{
		get
		{
			return maxRank;
		}
	}


	public JudgeBar NormalJudgeBar
	{
		get
		{
			return _normalJudgeBar;
		}
	}


	public JudgeBar SlowJudgeBar
	{
		get
		{
			return _slowJudgeBar;
		}
	}

	bool _isGameOver;

	public bool IsGameOver
	{
		get
		{
			return _isGameOver;
		}
	}

	public int PulseResultDataCount
	{
		get{
			return _PulseResultData .Count;
		}
	}

	public BulletResultData GetPulseResultData(int index)
	{
		return _PulseResultData[index] as BulletResultData;
	}

	public int GetCount(int Rank)
	{
		return NumBullet[Rank];
	}

	public int GetSlowlyCount( int Rank )
	{
		return NumSlowlyBullet[Rank];
	}

	public int GetEarlyCount( int Rank )
	{
		return NumEarlyBullet[Rank];
	}

	public int GetMissCount()
	{
		return MissCount;
	}

	public int GetEnemyKill()
	{
		return enemyKill;
	}

	public void AddEnemyKill()
	{
		enemyKill++;
	}


	// Use this for initialization
	void Start () {
		NormalJudgeBar .Setting();
		SlowJudgeBar .Setting();

		if (maxRank < NormalJudgeBar .MaxResultRank)
		{
			maxRank = NormalJudgeBar .MaxResultRank;
		}

		if (maxRank < SlowJudgeBar .MaxResultRank)
		{
			maxRank = SlowJudgeBar .MaxResultRank;
		}

		NumBullet = new int[maxRank];

		NumSlowlyBullet = new int[maxRank];

		NumEarlyBullet = new int[maxRank];

		_PulseResultData = new ArrayList( 3 );
		DontDestroyOnLoad(this);
		_isGameOver = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		for (int i = 0;i < PulseResultDataCount;i++)
		{
			BulletResultData data = GetPulseResultData(i);
			if( data.Miss)
			{
				MissCount++;
			}
			else
			{
				if (data .ResultRank < NumBullet .Length && data .ResultRank >= 0)
				{
					NumBullet[data .ResultRank ]++;

					if(data.IsSlowly)
						NumSlowlyBullet[data .ResultRank]++;

					if (data .IsEarly)
						NumEarlyBullet[data .ResultRank]++;

				}
			}
		}
		//リストの削除
		List_Clear();
	}

	void List_Clear()
	{
		_PulseResultData .Clear();
	}

	public void Push_Result( Marker marker  , Vector3 MarkerBullet , bool Miss , bool IsCanCounterEmit )
	{

		BulletResultData data = new BulletResultData();
		data .Setting( marker , MarkerBullet , Miss , IsCanCounterEmit , staticScript );
		_PulseResultData .Add( data );
	
	}
}
