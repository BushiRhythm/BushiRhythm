using UnityEngine;
using System.Collections;

public class EnemyFirePoint : MonoBehaviour {


	[SerializeField]
	Transform FirePoint;

	public Transform firePoint
	{
		get
		{
			return FirePoint;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
