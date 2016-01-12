using UnityEngine;
using System.Collections;

public class StaticMarkerAlg : MonoBehaviour {
	[SerializeField]
		AnimationCurve MarkerProgress;

	public float GetProgress(float time)
	{

		return MarkerProgress .Evaluate(time);

	}

}
