using UnityEngine;
using System.Collections;

public class EnemySpawnAlg : MonoBehaviour {

	[SerializeField]
	GameObject Model; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject EnemySpawn()
	{
		return Instantiate( Model , transform .position , transform .rotation ) as GameObject;
	}
}
