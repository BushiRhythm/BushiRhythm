using UnityEngine;
using System.Collections;

public class FireWorkShooter : MonoBehaviour {


	float time;

	bool IsEmit = false;

	[SerializeField]
	float MaxTime = 1.0f;

	[SerializeField]
	FireWorkExplosion explosion;

	[SerializeField]
	ParticleSystem particle;


	FireWorkExplosion EmitExplosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 pos = transform .position;

		if (MaxTime > time)
			pos += Vector3 .up * 6.0f * Time .deltaTime;
		else
		{
			if(!IsEmit)
			{
				GameObject obj = Instantiate( explosion .gameObject , transform .position , transform .rotation ) as GameObject;
				EmitExplosion = obj .GetComponent<FireWorkExplosion>();

				particle .enableEmission = false;
				IsEmit = true;
			}

		
		}
		time += Time .deltaTime;
		transform .position = pos;
	}
}
