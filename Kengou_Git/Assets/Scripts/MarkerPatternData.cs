using UnityEngine;
using System.Collections;

[System .Serializable]
public class MarkerData
{
	[SerializeField]
	int _tyming;
	[SerializeField]
	Vector2 _screenPos;

	public int Tyming
	{
		get
		{
			return _tyming;
		}
	}

	public Vector2 ScreenPos
	{
		get
		{
			return _screenPos;
		}
	}



}

public class MarkerPatternData : MonoBehaviour {

	[SerializeField]
	int _maxRhythm;

	[SerializeField]
	MarkerData[] markerData;

	public int MaxRhythm
	{
		get
		{
			return _maxRhythm;
		}
	}
	
	public bool IsTyming(int tyming, ref Vector2 Pos)
	{
		tyming %= MaxRhythm;
		for (int i = 0;i < markerData .Length;i++)
		{
			if (markerData[i] .Tyming== tyming)
			{
				Pos = markerData[i].ScreenPos;
				return true;
			}
		}
		return false;
	}
}
