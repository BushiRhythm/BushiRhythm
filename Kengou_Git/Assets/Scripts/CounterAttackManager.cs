using UnityEngine;
using System.Collections;

public abstract class CounterAttackManager : MonoBehaviour 
{
	protected ArrayList CounterAttacks;

	[SerializeField]
	AnimationCurve zAxisPos;

	[SerializeField]
	AnimationCurve xyAxisPos;

	[SerializeField]
	AnimationCurve upPos;

	public void CounterAttackEntry(AttackCounterEffecter val)
	{
		//アニメーションカーブを代入
		val .zAxisPos = zAxisPos;

		val .xyAxisPos = xyAxisPos;

		val .upPos = upPos;

		////着弾地点を設定
		SetEndPos( val );

		//リストに追加
		CounterAttacks .Add( val );
	}

	protected void Start()
	{
		CounterAttacks = new ArrayList( 5 );
	}

	abstract protected void CounterEndAction( AttackCounterEffecter val );

	abstract protected void SetEndPos( AttackCounterEffecter val );

	abstract public bool GetEnemyViewPort( ref Vector2 ViewPort );

	protected void Update()
	{
		//foreach (int index in CounterAttacks)
		for (int i = 0; i < CounterAttacks.Count; i++)
		{
			AttackCounterEffecter counterAttack = CounterAttacks[i] as AttackCounterEffecter;
			if (counterAttack == null)
			{
				CounterAttacks .RemoveAt( i );
				i--;
				continue;
			}
			if (counterAttack.IsEnd)
			{
				//終了したカウンター攻撃を精算する
				CounterEndAction( counterAttack );
				counterAttack .EnableAutoDestruct();
				CounterAttacks .RemoveAt( i );
				i--;
			}
		}
	}

}
