using UnityEngine;
using System .Collections;

public class LoadRotate : MonoBehaviour
{
    float i = 0;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		Quaternion q = transform .rotation;
		q .w = 3.14f + Mathf .Sin( i );
		q .z = 1.0f;
		transform .rotation = q;
		i += 0.015f;
	}
}
