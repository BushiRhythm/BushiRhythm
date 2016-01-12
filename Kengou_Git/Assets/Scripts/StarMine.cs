using UnityEngine;
using System.Collections;

public class StarMine : MonoBehaviour {

	[SerializeField]
	float StartSize = 0.1f;

	[SerializeField]
	AnimationCurve AddSize;

	[SerializeField]
	AnimationCurve AlphaCurve;
	
	float Val = .0f;

	Color col = new Color (Random.Range(70,100) * 0.01f,Random.Range(70,100) * 0.01f,Random.Range(70,100) * 0.01f);

	// Use this for initialization
	void Start () {
		Quaternion rot = transform.rotation;
		rot.z = Random.Range (-100,100) * 0.01f;
		transform.rotation = rot;

	}
	
	// Update is called once per frame
	void Update () {
		if (Val < 1.0f) {
            Val += 0.6f * Time.deltaTime;
		} else {
			Destroy(this.gameObject);
			return;
		}
		Vector3 pos = transform.position;
		pos.y -= Val * 0.01f;
		transform.position = pos;

		Vector3 size = transform.localScale;
		size.x = StartSize + AddSize.Evaluate(Val) * 3.5f;
		size.y = StartSize + AddSize.Evaluate(Val) * 3.5f;
		size.z = StartSize + AddSize.Evaluate(Val) * 3.5f;
		transform.localScale = size;
		Color alp = col;
		alp.a = AlphaCurve.Evaluate(1.0f-Val);
		this.GetComponent <SpriteRenderer> ().color = alp;

	}
}
