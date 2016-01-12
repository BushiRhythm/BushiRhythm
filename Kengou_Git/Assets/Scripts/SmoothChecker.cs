using UnityEngine;
using System.Collections;

public class SmoothChecker : MonoBehaviour {

	static SmoothChecker _instance;

	public static SmoothChecker instance
	{
		get
		{
			if(_instance == null)
			{
				GameObject obj = new GameObject();
				_instance = obj .AddComponent<SmoothChecker>();
				obj .name = "SmoothChecker";
				DontDestroyOnLoad( obj );
			}
			return _instance;

		}
	
	}

	private SmoothChecker()
	{
	}

	float[] time;
	float avgTime = float.MaxValue;

	public float AvgTime
	{
		get
		{
			return avgTime;
		}
	}

	// Use this for initialization
	void Start () {
		time = new float[20];
	}
	
	// Update is called once per frame
	void Update () {
		float NewTime = Time .unscaledDeltaTime;

		for (int i = time.Length - 1;i >= 1;i--)
		{
			time[i] = time[i - 1];
		}
		time[0] = NewTime;

		int cnt = 0;

		avgTime = .0f;

		for (cnt = 0;cnt < time .Length;cnt++)
		{
			if (time[cnt] == .0f)
				break;
			avgTime += time[cnt];
		}
		if (cnt != 0)
		{
			avgTime /= cnt;
		}
	}
}
