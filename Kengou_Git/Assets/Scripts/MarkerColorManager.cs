using UnityEngine;
using System.Collections;

public class MarkerColorManager : MonoBehaviour {

	[SerializeField]
	Color[] ColorPattern;

	public Color GetColor(int Rhythm)
	{
		Rhythm %= ColorPattern .Length;
		return ColorPattern[Mathf.Abs(Rhythm)];
	}
}
