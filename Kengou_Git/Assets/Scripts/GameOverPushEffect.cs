using UnityEngine;
using System.Collections;

public class GameOverPushEffect : MonoBehaviour {
	
	[SerializeField]
	float Alpha = 1.0f;
	[SerializeField]
	float AlphaSpeed = 0.1f;
	[SerializeField]
	Color col;
	[SerializeField]
	float ScaleSpeed = 0.2f;
	[SerializeField]
	bool EffEnable = false;
	// Use this for initialization

    CanvasRenderer _CR;
    CanvasRenderer CR {
        get {
            if (!_CR) _CR = GetComponent<CanvasRenderer>();
            return _CR;
        }
    }

	void Start () {
		Alpha = 1.0f;
        CR.SetAlpha(0);
	}
	public void EffectEnable(){
		EffEnable = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (EffEnable) {
			Alpha = Mathf.Max (Alpha - AlphaSpeed, .0f);
            CR.SetAlpha(Alpha);
			Vector3 scale = transform.localScale;
			scale.x = scale.x + ScaleSpeed;
			scale.y = scale.y + ScaleSpeed;
			transform.localScale = scale;
			if (Alpha == .0f)
				Destroy (this.gameObject);
		}
	}
}
