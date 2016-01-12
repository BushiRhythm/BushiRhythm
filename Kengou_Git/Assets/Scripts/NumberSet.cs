using UnityEngine;
using System.Collections;

public class NumberSet : MonoBehaviour {

	[SerializeField]
	Sprite[] Numbers;

	[SerializeField]
	SpriteRenderer[] Num;

	int Curnum = 0;

	[SerializeField]
	bool ZeroDisp = true;

	[SerializeField]
	bool LeftRender = false;

	// Use this for initialization
	void Start () {
		SpriteChange();
	}
	


	void SpriteChange()
	{
		int RenderCnt = 0;
		if (LeftRender)
		for (int i = 0;i < Num .Length;i++)
		{
			Num[i] .enabled = false;
		}

		for (int i = 0; i < Num.Length; i++)
		{
			int work = Curnum;

			for (int j = 0; j < Num.Length - i - 1; j++)
			{
				work /= 10;
			}
			if (work == 0 && i != Num .Length - 1 && !ZeroDisp)
			{
				if (!LeftRender)
					Num[i] .enabled = false;
			}
			else
			{
				
			
				work %= 10;

				
				if(!LeftRender)
				{
					Num[i] .enabled = true;
					Num[i] .sprite = Numbers[work];
				}
				else
				{
					Num[RenderCnt] .enabled = true;
					Num[RenderCnt] .sprite = Numbers[work];
					RenderCnt++;
				}
			}
		}
	
	}

	public void SetNum(int num)
	{
		if(Curnum ==num)
			return;

		Curnum = num ;

		SpriteChange();

	}
}
