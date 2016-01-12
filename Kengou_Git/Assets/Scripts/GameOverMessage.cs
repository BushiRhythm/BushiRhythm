using UnityEngine;
using UnityEngine .UI;
using System .Collections;

public class GameOverMessage : MonoBehaviour
{

	[SerializeField]
	Sprite[] LeftSprite;
	[SerializeField]
	Sprite TimeOverSprite;

	[SerializeField]
	int StageSize;

	BulletResultManager _bulletResultManager;
	public BulletResultManager bulletResultManager
	{
		get
		{
			if (!_bulletResultManager)
				_bulletResultManager = FindObjectOfType<BulletResultManager>();
			return _bulletResultManager;
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

	TimeLimit _timeLimit;

	TimeLimit timeLimit
	{
		get
		{
			if (!_timeLimit)
				_timeLimit = FindObjectOfType<TimeLimit>();
			return _timeLimit;
		}

	}

	GameOverSystem _gameOverSystem;

	GameOverSystem gameOverSystem
	{
		get
		{
			if (!_gameOverSystem)
				_gameOverSystem = FindObjectOfType<GameOverSystem>();
			return _gameOverSystem;
		}

	}

	// Use this for initialization
	void Start()
	{
		StageSize = frontMoverManager .GoalPos;
		Mes = GetComponent<Image>();
	}


	int DeathPhase = 0;
	bool TextSet = false;

	Image Mes;


	// Update is called once per frame
	void Update()
	{
		if (TextSet)
			return;
		if (bulletResultManager .IsGameOver)
		{
			DeathPhase = 0;
			int length = StageSize / 6;
			int val = ( StageSize - frontMoverManager .RestStep );
			while (DeathPhase < 5)
			{
				val = val - length;
				if (val <= 0)
					break;
				DeathPhase++;
			}
			Mes .sprite = LeftSprite[DeathPhase];
			if (gameOverSystem .IsTimeOver)
				Mes .sprite = TimeOverSprite;
			TextSet = true;
		}
	}
}
