using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugCode : MonoBehaviour {

	Text _text;

	Text text
	{
		get
		{
			if (!_text)
				_text = GetComponent<Text>();
			return _text;
		}
	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
			text.text = "";
	}

	public void AddText(string add)
	{
		text .text += add;
		text .text += "\n";
	}
}
