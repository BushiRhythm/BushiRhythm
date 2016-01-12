using UnityEngine;
using System.Collections;

public class EnemySEManager : MonoBehaviour {
	[SerializeField]
	SE damage;

	[SerializeField]
	SE swordAttackSE;

	[SerializeField]
	SE swordAttackSE2;

	[SerializeField]
	SE swordEnemyDeath;

	[SerializeField]
	SE syurikenThrow;

	[SerializeField]
	SE ShooterAttack;

	[SerializeField]
	SE ShooterDamage;

	[SerializeField]
	SE TakoDamege;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Damage()
	{
		Instantiate( damage );
	}

	public void SwordAttackSE()
	{
		Instantiate( swordAttackSE );
	}

	public void SwordAttackSE2()
	{
		Instantiate( swordAttackSE2 );
	}

	public void SyurikenThrow()
	{
		Instantiate( syurikenThrow );
	}

	public void SwordEnemyDeath()
	{
		Instantiate( swordEnemyDeath );
	}

	public void ShooterAttackSE()
	{
		Instantiate( ShooterAttack );
	}

	public void ShooterDamageSE()
	{
		Instantiate( ShooterDamage );
	}

	public void EnemyVoices()
	{
		switch(Random.Range(0,3))
		{
			case 0:
				Instantiate( ShooterDamage );
				break;
			case 1:
				Instantiate( swordEnemyDeath );
				break;
			case 2:
				Instantiate( TakoDamege );
				break;
		}
		
	}


	public void TakoDamegeSE()
	{
		Instantiate( TakoDamege );
	}


}
