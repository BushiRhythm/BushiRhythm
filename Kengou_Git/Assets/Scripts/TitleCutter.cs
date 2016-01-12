using UnityEngine;
using System.Collections;

public class TitleCutter : MonoBehaviour {

	TitleSystem _titleSystem;

	TitleSystem titleSystem
	{
		get
		{
			if(!_titleSystem)
			{
				_titleSystem = FindObjectOfType<TitleSystem>();
			}
			return _titleSystem;
		
		}
	
	}

	[SerializeField]
	MeshCutterAttacher attacher;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform .position;
		pos += attacher .CutDir * titleSystem.MoveSpeed;
		transform .position = pos;
	}
}
