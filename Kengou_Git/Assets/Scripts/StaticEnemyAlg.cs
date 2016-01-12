using UnityEngine;
using System.Collections;

//固定化された敵の行動アルゴリズムを取得する
public class StaticEnemyAlg : MonoBehaviour {

	//横の幅
	[SerializeField]
	float _Width = 1.0f;

	[SerializeField]
	float adjustHeight = .0f;

	public Vector3 AdjustHeight
	{
		get
		{
			return new Vector3( .0f , adjustHeight , .0f );
		}
	}

	public Vector3 GetWidth(bool IsRight)
	{
		Vector3 Right = new Vector3(1.0f,.0f,.0f);

		if(!IsRight)
		{
			Right = -Right;
		}
		return Right * _Width;
	
	}

	//はける距離
    [SerializeField]
    int _escapeDistance = 2;
	
	//攻撃開始距離
    [SerializeField]
    int _attackDistance = 10;

	//出現距離
	[SerializeField]
	int _spawnDistance = 10;

	public bool IsCanAttack(int disrtance)
	{
		return   disrtance <= _attackDistance;
	}

	public bool IsEnemyEscape(int disrtance)
	{
		return	disrtance <= _escapeDistance;
	}

	public bool IsEnemySpawn( int disrtance )
	{
		return	disrtance <= _spawnDistance;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
