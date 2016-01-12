using UnityEngine;
using System.Collections;

public class FrontMoverManagerData : MonoBehaviour {

	[SerializeField , HeaderAttribute( "一回の移動距離" )]
	public float moveDistance = 10.0f;

	[SerializeField , HeaderAttribute( "クリア距離(歩数)" )]
	public int goalPos = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
