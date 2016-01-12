using UnityEngine;
using System.Collections;

public class ButtomCounterAttackManager : CounterAttackManager
{

	[SerializeField]
	Transform CAPoint;

	EnemyStanManager _enemyStanManager;

	EnemyStanManager enemyStanManager
	{
		get
		{
			if (!_enemyStanManager)
				_enemyStanManager = Object .FindObjectOfType<EnemyStanManager>();
			return _enemyStanManager;
		}
	}

	// Use this for initialization
	void Start () {
		base .Start();
	}

	override protected void SetEndPos( AttackCounterEffecter val )
	{
		val .EndTrans = CAPoint;
	}

	override public bool GetEnemyViewPort( ref Vector2 ViewPort)
	{
		return false;
	}

	override protected void CounterEndAction( AttackCounterEffecter val )
	{
		//地面が揺れる
		enemyStanManager .Stan();
	}



	// Update is called once per frame
	void Update () {
		base .Update();
	}
}
