using UnityEngine;
using System.Collections;

public class PulseParticle : MonoBehaviour {

    ParticleSystem myParticleSystem;

    ParticleSystem MyParticleSystem
    {
        get { if (!myParticleSystem) myParticleSystem = GetComponent<ParticleSystem>();
        return myParticleSystem;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (MyParticleSystem != null && MyParticleSystem.particleCount == 0)
        {
            Destroy(this.gameObject);
        }
    }

	

}
