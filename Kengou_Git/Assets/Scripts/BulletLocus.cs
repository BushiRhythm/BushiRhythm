using UnityEngine;
using System.Collections;

public class BulletLocus : MonoBehaviour {


    [SerializeField, HeaderAttribute("軌跡の大きさ")]
    private float locusSize = 1f;
	// Use this for initialization
    Vector3 scale;

	void Start () {

        scale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        TrailRenderer TR = this.GetComponent<TrailRenderer>();

        TR.startWidth = scale.x * locusSize;
        TR.endWidth = TR.startWidth * 0.25f;
	}
}
