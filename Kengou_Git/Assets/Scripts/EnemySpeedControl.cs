using UnityEngine;
using System.Collections;

public class EnemySpeedControl : MonoBehaviour {

	[SerializeField]
	AnimationCurve RhythmScale;

	[SerializeField]
	bool Scaling = false;

	float LastPos;


	StaticScript _staticScript;

	StaticScript staticScript
	{
		get
		{
			if (!_staticScript)
				_staticScript = Object .FindObjectOfType<StaticScript>();
			return _staticScript;
		}
	}

    Animator _animator;

    Animator animator
    {
        get
        {
            if (!_animator)
                _animator = GetComponent<Animator>();
            return _animator;
        }
    }
	bool IsPlay;

	public void Stop()
	{
		IsPlay = false;
	}

	public void Play()
	{
		IsPlay = true;
	}

	public void StopScaling()
	{
		Scaling = false;
	}

	public void PlayScaling()
	{
		Scaling = true;
	}

	// Use this for initialization
	void Start () {
		IsPlay = true;
		LastPos = staticScript .rhythmManager .DynamicRhythm .Pos;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 scale =transform.localScale;
		if (Scaling)
		{
			scale.y = scale.x * RhythmScale.Evaluate( staticScript.rhythmManager .FixedRhythm.Progress);
		}
		else
		{
			scale .y = scale .x;
		}
		transform .localScale = scale;
		if (IsPlay)
		{
			if (animator)
			{
				//前回の更新までにどれほど進んだのかを計算
				float AddTime = animator .speed * Time .unscaledDeltaTime;

				if(LastPos > staticScript .rhythmManager .DynamicRhythm .Pos)
				{
					animator .speed = .0f;
				}
				else
				{
					animator .speed = staticScript.rhythmManager .DynamicRhythm .DeltaPos / Time .unscaledDeltaTime * 0.5f;
					LastPos = staticScript .rhythmManager .DynamicRhythm .Pos;
				}

			}
		}
		else
		{
			if (animator)
				animator .speed = .0f;
		}
	}
}
