using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectHandedness : MonoBehaviour {

	[SerializeField]
	bool TrueIsLeft = false;
	public Sprite LeftSprite;
	public Sprite RightSprite;

    OptionManager _Option;
    OptionManager Option
    {
        get
        {
            if (!_Option) _Option = FindObjectOfType<OptionManager>();
            return _Option;
        }
    }
	// Use this for initialization
    void Start()
    {
        TrueIsLeft = Option.GetLR();
        if (TrueIsLeft)
            SetSprite(LeftSprite);
        if (!TrueIsLeft)
            SetSprite(RightSprite);
	}
	
	public void SetHandedness () {
		TrueIsLeft = !TrueIsLeft;
		if (TrueIsLeft)
			SetSprite (LeftSprite);
		if (!TrueIsLeft)
			SetSprite (RightSprite);
        Option.SetLROption(TrueIsLeft);
	}
	public void SetSprite(Sprite sprite){
		this.GetComponent<Image>().sprite = sprite;
	}
}
