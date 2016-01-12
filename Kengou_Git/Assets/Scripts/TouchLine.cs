using UnityEngine;
using System.Collections;

public class TouchLine : MonoBehaviour {

	[SerializeField]
	LineRenderer line;

	StaticScript _staticScript;

	[SerializeField]
	int numVertexes;

	Vector3[] pos;

	// Use this for initialization
	void Start()
	{
		line .SetVertexCount(numVertexes);
		pos = new Vector3[numVertexes];
	}

	// Update is called once per frame
	void Update()
	{
		if (!Input .GetKey( KeyCode .Mouse0 ))
		{
			line .enabled = false;
			return;
		}
		line .enabled = true;
		Vector3 pos = Input .mousePosition;
		pos .x -= Screen .width / 2;
		pos .y -= Screen .height / 2;

		//for (int i = 0;i < numVertexes;i++)
		//{
		//	line.
		//}


	}
}
