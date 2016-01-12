using UnityEngine;
using System.Collections;

public class MarkerParticleEmitter : MonoBehaviour {

	StaticScript _staticScript;

	StaticScript staticScript
	{
		get
		{
			if (!_staticScript)
				_staticScript = GameObject .FindGameObjectWithTag( "StaticScript" ) .GetComponent<StaticScript>();
			return _staticScript;
		}
	}

	[SerializeField]
	ParticleSystem[] CounterParticle;

	[SerializeField]
	ParticleSystem[] PulseParticle;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if(staticScript.bulletResultManager.PulseResultDataCount == 0)
			return;

		Vector3 forward = staticScript.sword.SwordDirection3D;
		Vector3 front = Vector3.forward * 0.0f;
		//Vector3 right = Vector3.Cross(Vector3.up,forward);
		//Vector3 up = Vector3.Cross(forward,right);
		//right.Normalize();
		//up.Normalize();

		Quaternion ParticleDirection =Quaternion.LookRotation(forward);

		for (int i = 0;i < staticScript .bulletResultManager .PulseResultDataCount;i++)
		{
			BulletResultData data = staticScript .bulletResultManager .GetPulseResultData( i );
			if (data .Miss)
				return;


			Instantiate( CounterParticle[data.ResultRank] , data .MarkerWorldPos + front , ParticleDirection );
			if(!data .IsCounterEmited)
				Instantiate( PulseParticle[data .ResultRank] , data .MarkerWorldPos + front , Quaternion .identity );
		}
	}
}
