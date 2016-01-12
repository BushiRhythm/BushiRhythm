using UnityEngine;
using System.Collections;

public class RenameText : MonoBehaviour {
	[SerializeField]
	TextMesh textmesh;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetText(string text)
	{
		if (text != textmesh .text)
			textmesh .text = text;
	}
}
