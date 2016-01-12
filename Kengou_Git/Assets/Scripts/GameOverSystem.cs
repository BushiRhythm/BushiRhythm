using UnityEngine;
using System.Collections;

public class GameOverSystem : MonoBehaviour {
	[SerializeField]
	Transform child;

	[SerializeField]
	public Camera MyCamera;

	bool isTimeOver;

	public bool IsTimeOver
	{
		get
		{
			return isTimeOver;
		}	
	}

	public void TimeOver()
	{
		isTimeOver = true;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Run()
	{
		child .gameObject .SetActive( true );
	}
}
