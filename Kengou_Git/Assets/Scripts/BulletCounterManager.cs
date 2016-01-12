using UnityEngine;
using System.Collections;

public class BulletCounterManager : MonoBehaviour {


	BulletResultManager _bulletResultManager;

	BulletResultManager bulletResultManager
	{
		get
		{
			if(!_bulletResultManager)
			{
				_bulletResultManager = FindObjectOfType<BulletResultManager>();
			}
			return _bulletResultManager;
		}
	}

	Sword _sword;

	Sword sword
	{
		get
		{
			if (!_sword)
			{
				_sword = FindObjectOfType<Sword>();
			}
			return _sword;
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//はじく処理追加
		for (int i = 0;i < bulletResultManager.PulseResultDataCount;i++)
		{


		}
	}
}
