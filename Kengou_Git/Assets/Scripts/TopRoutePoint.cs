using UnityEngine;
using System.Collections;

public class TopRoutePoint : MonoBehaviour {

	[SerializeField]
	Transform leftTopRoutePoint;

	public Transform LeftTopRoutePoint
	{
		get
		{
			return leftTopRoutePoint;
		}
	}



	[SerializeField]
	Transform rightTopRoutePoint;

	public Transform RightTopRoutePoint
	{
		get
		{
			return rightTopRoutePoint;
		}
	}

}
