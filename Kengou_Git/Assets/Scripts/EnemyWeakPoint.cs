using UnityEngine;
using System.Collections;

public class EnemyWeakPoint : MonoBehaviour {


	[SerializeField]
	Transform _weakPoint;

	public Transform weakPoint
	{
		get
		{
			return _weakPoint;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
