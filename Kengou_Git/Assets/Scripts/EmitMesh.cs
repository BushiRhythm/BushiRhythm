using UnityEngine;
using System.Collections;

public class EmitMesh : MonoBehaviour {
    public GameObject EmitterPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.childCount == 0)
            Destroy(this.gameObject);

		if(EmitterPoint.transform.position.z - Camera.main.transform.position.z < -3.0f)
			Destroy( this .gameObject );
	}
}
