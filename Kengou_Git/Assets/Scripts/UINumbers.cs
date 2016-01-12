using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UINumbers : MonoBehaviour {

    [SerializeField]
    Sprite[] VarSprite;
    [SerializeField]
    Sprite NullSprite;
    [SerializeField]
    Sprite MinusSprite;

    //現在の数値
    int num = -1;
    //自身の桁数
    int Digit = 1;

    NumberParent _numberParent;
    NumberParent numberParent {
        get {
            if (!_numberParent) _numberParent = transform.parent.GetComponent<NumberParent>();
            return _numberParent;
        }
    }

    Image _image;
    Image image {
        get{
            if (!_image) _image = GetComponent<Image>();
            return _image;
        }
    }
    [SerializeField]
    Image ParentImage;

	// Use this for initialization
    public void SetDigit(int Val)
    {
        Digit = Val;
    }
    // Use this for initialization
    public int GetDigitVal()
    {
        int dig = 1;
        for (int i = 0; i < Digit; i++)
        {
            dig = dig * 10;
        }
        return dig;
    }

	void Start () {
	}

    public void SetSprite(int Num) {
        image.sprite = VarSprite[Mathf.Abs(Num)];
        num = Mathf.Abs(Num);
    }

    public void SetNullSprite()
    {
        image.sprite = NullSprite;
    }
    public void SetMinusSprite()
    {
        image.sprite = MinusSprite;
    }

    int GetDigitNum(int value)
    {
        return ( value / GetDigitVal() ) % 10;
    }
    void ColorUpdate()
    {
        if (!ParentImage && transform.parent.GetComponent<Image>()) ParentImage = transform.parent.GetComponent<Image>();
        if (ParentImage) image.color = ParentImage.color;
    }

	// Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1,1,1);

        if (!transform.parent) return;
        ColorUpdate();
	}
}
