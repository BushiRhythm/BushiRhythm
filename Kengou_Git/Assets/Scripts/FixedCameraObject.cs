using UnityEngine;
using System.Collections;

public class FixedCameraObject : MonoBehaviour {


	//親のオブジェクトとの相対位置を維持したまま引越します
	bool IsMoving = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsMoving)
		{

			if(Camera.main == null)
				return;
			Transform mainCamera = Camera.main.transform;
			while (transform .childCount != 0)
			{
				Transform child = transform.GetChild(0);
				Vector3 LocalPos = child .localPosition;
				child .parent = mainCamera;
				child .localPosition = LocalPos;
			}
			IsMoving = false;
		}
	}
}
