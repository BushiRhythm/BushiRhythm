using UnityEngine;
using System.Collections;

public class TopCounterAttackManager : CounterAttackManager {



	[SerializeField]
	Transform NoTargetCAPoint;

	TopEnemyManager _topEnemyManager;

	TopEnemyManager topEnemyManager
	{
		get{
			if(!_topEnemyManager)
				_topEnemyManager = FindObjectOfType<TopEnemyManager>();
			return _topEnemyManager;
		}
	}



	// Use this for initialization
	void Start()
	{
		base .Start();
	}

	override protected void SetEndPos( AttackCounterEffecter val )
	{
		//攻撃できる前の敵を探す
		Enemy enemy = topEnemyManager .SpawnEnemy;
		if (enemy == null || enemy.IsVirtualDeath)
		{
			//敵がいない場合は奥に飛ばす
			val .EndTrans = NoTargetCAPoint;

		}
		else
		{
			//弱点を終点に設定
			val .EndTrans = enemy .WeakPoint;
			//敵をターゲットに設定
			val .TargetEnemy = enemy;

			enemy .VirtualDamage( 1 );
		}
	}

	override public bool GetEnemyViewPort( ref Vector2 ViewPort )
	{
		//攻撃できる前の敵を探す
		Enemy enemy = topEnemyManager .SpawnEnemy;

		if (enemy == null)
			return false;

		ViewPort = Camera .main .WorldToViewportPoint( enemy .WeakPoint .transform .position );
		return true;
	}
	

	override protected void CounterEndAction( AttackCounterEffecter val )
	{
		if (val .TargetEnemy != null)
			val .TargetEnemy .Damage( 1 );
	}



	// Update is called once per frame
	void Update()
	{
		base .Update();
	}

}
