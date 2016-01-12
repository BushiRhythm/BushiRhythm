using UnityEngine;
using System.Collections;

public class alphacolortest : MonoBehaviour {

	[SerializeField]
	MeshFilter filter;

	[SerializeField]
	Color color;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0;i < filter .mesh .colors32.Length;i++)
		{
			filter .mesh .colors[i] = color;
			filter .mesh .colors32[i] = color;
		}
		

	}
}
