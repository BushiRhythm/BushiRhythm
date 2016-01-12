using UnityEngine;
using System.Collections;

public class BasicLoader : MonoBehaviour {

	BulletResultManager _bulletResultManager;

	BulletResultManager bulletResultManager
	{
		get
		{
			if (!_bulletResultManager)
			{
				_bulletResultManager = FindObjectOfType<BulletResultManager>();
			}
			return _bulletResultManager;
		}
	}

	ResultData _resultData;

	ResultData resultData
	{
		get
		{
			if (!_resultData)
			{
				_resultData = FindObjectOfType<ResultData>();
			}
			return _resultData;
		}
	}



	// Use this for initialization
	void Start () {
		if (bulletResultManager != null)
		{
			Destroy( bulletResultManager .gameObject );
		}
		if (resultData != null)
		{
			Destroy( resultData .gameObject );
		}
        Application.LoadLevelAdditiveAsync("BasicMain");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
