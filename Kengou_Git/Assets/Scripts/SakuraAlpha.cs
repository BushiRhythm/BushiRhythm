using UnityEngine;
using System.Collections;

public class SakuraAlpha : MonoBehaviour {

    TempoSakuraSyouAlpha _tempoSakuraSyouAlpha;
    TempoSakuraSyouAlpha tempoSakuraSyouAlpha
    {
        get
        {
            if (!_tempoSakuraSyouAlpha)
                _tempoSakuraSyouAlpha = Object.FindObjectOfType<TempoSakuraSyouAlpha>();
            return _tempoSakuraSyouAlpha;
        }

    }


    SpriteRenderer sakuraRenderer;
    Color sakuraColor;
	// Use this for initialization
	void Start () {
        sakuraRenderer = this.GetComponent<SpriteRenderer>();
        sakuraColor = sakuraRenderer.color;
        
	}
	
	// Update is called once per frame
	void Update () {
        sakuraColor.a = tempoSakuraSyouAlpha.GetSakuraAlpha();
        sakuraRenderer.color = sakuraColor;

        
        
	}
}
