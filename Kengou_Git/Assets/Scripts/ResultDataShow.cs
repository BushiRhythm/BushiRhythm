using UnityEngine;
using System.Collections;

public class ResultDataShow : MonoBehaviour {

	bool show = false;
	[SerializeField]
	float DelayTime = .0f;
    float Per = .0f;

    [SerializeField]
    GameObject Sound;

	Vector3 ScaleBase;

	[SerializeField]
	public AnimationCurve curve;
    CanvasRenderer Cr;
	// Use this for initialization
	void Start () {
        Cr = GetComponent<CanvasRenderer>();
        Cr.SetAlpha(0);
		ScaleBase = transform.localScale;
		Invoke ("ShowSwitch",DelayTime);
	}
    public bool IsMoveComplete()
    {
        return (Per >= 1.0f);
	}

    bool Snd = false;

	// Update is called once per frame
    void Update()
    {
        Cr.SetAlpha(0);
		if(!show) return;
		if (Per < 1.0f)
            Per += 1.5f * Time.deltaTime;
		if (Per > 1.0f)
			Per = 1.0f;
        Cr.SetAlpha(curve.Evaluate(Per));
		Vector3 Scl = transform.localScale;

		Scl.x = ScaleBase.x + 1.0f - curve.Evaluate (Per);
		Scl.y = ScaleBase.y + 1.0f - curve.Evaluate (Per);

		transform.localScale = Scl;

        if (Sound && Per == 1.0f && !Snd)
        {
            Instantiate(Sound, transform.position, transform.rotation);
            Snd = true;
        }

	}
	void ShowSwitch () {
		show = true;
	}
}
