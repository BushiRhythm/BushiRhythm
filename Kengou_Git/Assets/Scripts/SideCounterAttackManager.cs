using UnityEngine;
using System.Collections;

public class SideCounterAttackManager : CounterAttackManager
{
	[SerializeField]
	EnemyLineManager enemyLineManager;

	[SerializeField]
	Transform NoTargetCAPoint;



	// Use this for initialization
	void Start () {
		base .Start();
	}

	override protected void SetEndPos( AttackCounterEffecter val )
	{
		//攻撃できる前の敵を探す
		Enemy enemy = enemyLineManager .GetCanAttackFrontEnemy();
		if(enemy == null)
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

	override protected void CounterEndAction( AttackCounterEffecter val )
	{
		if(val.TargetEnemy != null)
			val .TargetEnemy .Damage( 1 );
	}

	override public bool GetEnemyViewPort( ref Vector2 ViewPort )
	{
		//攻撃できる前の敵を探す
		Enemy enemy = enemyLineManager .GetCanAttackFrontEnemy();

		if(enemy == null)
			return false;

		ViewPort = Camera .main .WorldToViewportPoint( enemy .WeakPoint .transform .position);
		return true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		base .Update();
	}

}
