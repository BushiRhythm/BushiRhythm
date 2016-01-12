using UnityEngine;
using System.Collections;

public class FrontMoveCamera : MonoBehaviour {

    Rigidbody _rigidBody;

    Rigidbody rigidBody
    {
        get
        {
            if(!_rigidBody)
            {
                _rigidBody = GetComponent<Rigidbody>();
            }
            return _rigidBody;

        }
    }
    public float speed = 0.1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 pos = transform.position;
        //Vector3 forward = transform.forward - Vector3.up * Vector3.Dot(transform.forward, Vector3.up);
        //pos += forward * speed * Time.deltaTime;
         //transform.position = pos;
	}

    void FixedUpdate()
    {
        Vector3 forward = transform.forward - Vector3.up * Vector3.Dot(transform.forward, Vector3.up);
        rigidBody.velocity = forward * speed;

    }
}
