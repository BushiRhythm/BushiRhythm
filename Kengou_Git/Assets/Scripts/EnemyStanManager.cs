using UnityEngine;
using System.Collections;

public class EnemyStanManager : MonoBehaviour {

	[SerializeField]
	bool StanIsEnable;

	bool isStan;

	[SerializeField]
	int maxStan = 8;

	public int MaxStan
	{
		get
		{
			return maxStan;
		}
	
	}

	public bool IsStan
	{
		get
		{
			return isStan;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		isStan = false;
	}

	public void Stan()
	{
		if (StanIsEnable)
			isStan = true;
	}
}
