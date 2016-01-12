using UnityEngine;
using System.Collections;

public class StaticSwordEnemyAlg : MonoBehaviour {
	//横の幅
	[SerializeField]
	float _Width = 1.0f;

	[SerializeField]
	Vector2 _rightMarkerPos;

	[SerializeField]
	GameObject _marker;


	public Vector2 RightMarkerPos
	{
		get
		{
			return _rightMarkerPos;
		}
	}


	public GameObject Marker
	{
		get
		{
			return _marker;
		}
	}



	public Vector3 GetWidth( bool IsRight )
	{
		Vector3 Right = new Vector3( 1.0f , .0f , .0f );

		if (!IsRight)
		{
			Right = -Right;
		}
		return Right * _Width;

	}

	//移動開始距離
	[SerializeField]
	int _moveStartDistance = 20;

	//攻撃開始距離
	[SerializeField]
	int _attackDistance = 10;

	[SerializeField]
	float _stepTime  = 0.5f;

	[SerializeField]
	float _escapeDistance = 4;


	public bool IsCanAttack( int disrtance )
	{
		return disrtance <= _attackDistance;
	}

	public bool IsCanMove( int disrtance )
	{
		return disrtance <= _moveStartDistance;
	}

	public bool IsEnemyEscape( int disrtance )
	{
		return disrtance <= _escapeDistance;
	}

	public float StepTime
	{
		get{
			return _stepTime;

		}

	}






}
