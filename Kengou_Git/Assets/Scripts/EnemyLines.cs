using UnityEngine;
using System.Collections;

public class EnemyLines : MonoBehaviour {

	[SerializeField]
	EnemyLineManager[] lines;

	[SerializeField]
	Transform[] swordEnemyGoalPoa;



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public EnemyLineManager this[int idx]
	{
		get
		{
			return lines[idx];
		}
	}

	public Transform GetSwordEnemyGoalPos(int idx)
	{
		return swordEnemyGoalPoa[idx];
	}

}
