using UnityEngine;
using System.Collections;

public class SpawnParticle : MonoBehaviour {

	[SerializeField]
	PulseParticle spawnParticle;

	public void Emit(Vector3 pos)
	{
		Instantiate( spawnParticle.gameObject , pos,Quaternion.identity );
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
