using UnityEngine;
using System.Collections;

public class LoadingObjects : MonoBehaviour {

    [SerializeField]
    GameObject LoadObject;
	// Use this for initialization
	void Start () {
        Instantiate(LoadObject, LoadObject.transform.position, LoadObject.transform.rotation);
        Destroy(gameObject);
	}
	
}
