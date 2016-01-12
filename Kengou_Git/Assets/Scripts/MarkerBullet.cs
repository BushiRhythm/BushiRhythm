using UnityEngine;
using System.Collections;

public class MarkerBullet : MonoBehaviour {

    [SerializeField]
         Marker C_marker;
    [SerializeField]
        MarkerFirePoint C_markerFirePoint;

	StaticScript _staticScript;

	protected StaticScript staticScript
	{
		get
		{
			if (!_staticScript)
				_staticScript = GameObject .FindGameObjectWithTag( "StaticScript" ) .GetComponent<StaticScript>();
			return _staticScript;
		}
	}

    private Vector3 _length;

    public Vector3 Length
    {
        get
        {
            return _length;
        }
        set
        {
            _length = value;
            transform.forward = value.normalized;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform .position = C_markerFirePoint .transform .position + Length * staticScript.staticMarkerAlg.GetProgress(C_marker .Progress);
	
	}
}
