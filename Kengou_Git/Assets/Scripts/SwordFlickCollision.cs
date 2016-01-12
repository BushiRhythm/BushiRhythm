using UnityEngine;
using System.Collections;

public class SwordFlickCollision : MonoBehaviour {

    public bool IsCollision = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
                       
        IsCollision = true;
        

    }

}
