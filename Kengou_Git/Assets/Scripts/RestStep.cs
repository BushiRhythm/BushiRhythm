using UnityEngine;
using UnityEngine .UI;
using System.Collections;

public class RestStep : MonoBehaviour {

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

	FrontMoverManager _frontMoverManager;

	FrontMoverManager frontMoverManager
	{
		get
		{
			if (!_frontMoverManager)
				_frontMoverManager = Object .FindObjectOfType<FrontMoverManager>();
			return _frontMoverManager;
		}

	}

	// Use this for initialization

	// Update is called once per frame
	void Update()
	{
		int Reststep = frontMoverManager.RestStep;
		if(Reststep > 0)
		{
			text .text = System .String .Format( "ゴールまで残り{0}m" , Reststep );
		}
		else if (frontMoverManager.IsGoal)
		{
			text .text = System .String .Format( "ゴール判定" , Reststep );

		}
		
	}

}
