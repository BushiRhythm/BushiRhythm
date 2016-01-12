using UnityEngine;
using System.Collections;
using System .Collections .Generic;


public class MarkerManager : MonoBehaviour {

	List<Marker> markerList;
	// Use this for initialization
	void Start () {
		markerList = new List<Marker>();
	}

	public Marker[] MarkerList	 //foreach推奨
	{
		get
		{
			return markerList .ToArray();
		}
	}
	

	public void Add(Marker marker)
	{
		markerList .Add( marker );
	}
	// Update is called once per frame
	void Update () {
		for (int i = 0;i < markerList.Count;i++)
		{
			if(markerList[i] == null)
			{
				markerList .RemoveAt( i );
				i--;
			}
		}
	}
}
