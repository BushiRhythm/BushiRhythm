using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyLineManager : MonoBehaviour {
	List<Enemy> EnemyList;


	// Use this for initialization
	void Start () {
		EnemyList = new List<Enemy>();
	}

	public void AddList(Enemy val)
	{
		EnemyList .Add( val );

	}

	public Enemy GetCanAttackFrontEnemy()
	{
		for (int i = 0;i < EnemyList.Count;i++)
		{
			if (!EnemyList[i] .IsVirtualDeath)
				return EnemyList[i];
		}
		return null;
	
	}

	public Enemy GetActiveFrontEnemy()
	{
		for (int i = 0;i < EnemyList .Count;i++)
		{
			if (!EnemyList[i] .IsDeath)
				return EnemyList[i];
		}
		return null;

	}



	// Update is called once per frame
	void Update () {
		for (int i = 0;i < EnemyList .Count;i++)
		{
			if (EnemyList[i]  == null || EnemyList[i] .IsDeath)
			{
				EnemyList.RemoveAt(i);
				i--;

			}
		}

		
	}
}
