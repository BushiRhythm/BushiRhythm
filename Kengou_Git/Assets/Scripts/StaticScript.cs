using UnityEngine;
using System.Collections;

public class StaticScript : MonoBehaviour {

	RhythmManager _rhythmManager;

	public RhythmManager rhythmManager
	{
		get
		{
			if (!_rhythmManager)
				_rhythmManager = Object .FindObjectOfType<RhythmManager>();
			return _rhythmManager;
		}
	}

	StaticMarkerEmitter _staticMarkerEmitter;

	public StaticMarkerEmitter staticMarkerEmitter
	{
		get
		{
			if (!_staticMarkerEmitter)
				_staticMarkerEmitter = Object .FindObjectOfType<StaticMarkerEmitter>();
			return _staticMarkerEmitter;
		}
	}

	EnemyLines _enemyLines;

	public EnemyLines enemyLines
	{
		get
		{
			if (!_enemyLines)
				_enemyLines = Object .FindObjectOfType<EnemyLines>();
			return _enemyLines;
		}
	}

	FlickManager _FlickManager;

	public FlickManager FlickManager
	{
		get
		{
			if (!_FlickManager)
				_FlickManager = FindObjectOfType<FlickManager>();
			return _FlickManager;
		}
	}

	BulletResultManager _bulletResultManager;

	public BulletResultManager bulletResultManager
	{
		get
		{
			if (!_bulletResultManager)
			{
				_bulletResultManager = FindObjectOfType<BulletResultManager>();
			}
			return _bulletResultManager;
		}
	}

	Sword _sword;

	public Sword sword
	{
		get
		{
			if (!_sword)
			{
				_sword = FindObjectOfType<Sword>();
			}
			return _sword;
		}
	}

	BGMData _bgmData;

	public BGMData bgmData
	{
		get
		{
			if (!_bgmData)
				_bgmData = Object .FindObjectOfType<BGMData>();
			return _bgmData;
		}
	}

	EnemyStanManager _enemyStanManager;

	public EnemyStanManager enemyStanManager
	{
		get
		{
			if (!_enemyStanManager)
				_enemyStanManager = Object .FindObjectOfType<EnemyStanManager>();
			return _enemyStanManager;
		}
	}

	StaticEnemyAlg _staticEnemyAlg;

	public StaticEnemyAlg staticEnemyAlg
	{
		get
		{
			if (!_staticEnemyAlg)
				_staticEnemyAlg = Object .FindObjectOfType<StaticEnemyAlg>();
			return _staticEnemyAlg;
		}
	}

	StaticSwordEnemyAlg _staticSwordEnemyAlg;

	public StaticSwordEnemyAlg staticSwordEnemyAlg
	{
		get
		{
			if (!_staticSwordEnemyAlg)
				_staticSwordEnemyAlg = Object .FindObjectOfType<StaticSwordEnemyAlg>();
			return _staticSwordEnemyAlg;
		}
	}


	EnemyAttackManager _enemyAttackManager;

	public EnemyAttackManager enemyAttackManager
	{
		get
		{
			if (!_enemyAttackManager)
				_enemyAttackManager = Object .FindObjectOfType<EnemyAttackManager>();
			return _enemyAttackManager;
		}
	}

	FrontMoverManager _frontMoverManager;

	public FrontMoverManager frontMoverManager
	{
		get
		{
			if (!_frontMoverManager)
				_frontMoverManager = Object .FindObjectOfType<FrontMoverManager>();
			return _frontMoverManager;
		}

	}

	TopEnemyManager _topEnemyManager;

	public TopEnemyManager topEnemyManager
	{
		get
		{
			if (!_topEnemyManager)
				_topEnemyManager = Object .FindObjectOfType<TopEnemyManager>();
			return _topEnemyManager;
		}
	}

	ActionTymingManager _actionTymingManager;
	public ActionTymingManager actionTymingManager
	{
		get
		{
			if (!_actionTymingManager)
				_actionTymingManager = Object .FindObjectOfType<ActionTymingManager>();
			return _actionTymingManager;
		}

	}

	SmallwaveManager _smallwaveManager;
	public SmallwaveManager smallwaveManager
	{
		get
		{
			if (!_smallwaveManager)
				_smallwaveManager = Object .FindObjectOfType<SmallwaveManager>();
			return _smallwaveManager;
		}

	}

	MarkerManager _markerManager;

	public MarkerManager markerManager
	{
		get
		{
			if (!_markerManager)
				_markerManager = Object .FindObjectOfType<MarkerManager>();
			return _markerManager;
		}
		
	}

	StaticMarkerAlg _staticMarkerAlg;

	public StaticMarkerAlg staticMarkerAlg
	{
		get
		{
			if (!_staticMarkerAlg)
				_staticMarkerAlg = Object .FindObjectOfType<StaticMarkerAlg>();
			return _staticMarkerAlg;
		}

	}


	MarkerColorManager _markerColorManager;

	public MarkerColorManager markerColorManager
	{
		get
		{
			if (!_markerColorManager)
				_markerColorManager = Object .FindObjectOfType<MarkerColorManager>();
			return _markerColorManager;
		}

	}

	DebugCode _debugCode;

	public DebugCode debugCode
	{
		get
		{
			if (!_debugCode)
				_debugCode = Object .FindObjectOfType<DebugCode>();
			return _debugCode;
		}

	}


	Canvas _markerCanvas;

	public Canvas markerCanvas
	{
		get
		{
			if (!_markerCanvas)
				_markerCanvas = GameObject.Find("MarkerCanvas").GetComponent<Canvas>();
			return _markerCanvas;
		}
	}

	CanvasRenderer _markerCanvasRenderer;

	public CanvasRenderer markerCanvasRenderer
	{
		get
		{
			if (!_markerCanvasRenderer)
				_markerCanvasRenderer = markerCanvas .GetComponent<CanvasRenderer>();
			return _markerCanvasRenderer;
		}
	}

	Canvas _uiCanvas;

	public Canvas uiCanvas
	{
		get
		{
			if (!_uiCanvas)
				_uiCanvas = GameObject .Find( "UICanvas" ) .GetComponent<Canvas>();
			return _uiCanvas;
		}
	}


	CanvasRenderer _uiCanvasRenderer;

	public CanvasRenderer uiCanvasRenderer
	{
		get
		{
			if (!_uiCanvasRenderer)
				_uiCanvasRenderer = uiCanvasRenderer .GetComponent<CanvasRenderer>();
			return _uiCanvasRenderer;
		}
	}

	GoalStarter _goalstarter;

	public GoalStarter goalstarter
	{
		get
		{
			if(!_goalstarter)
			{
				_goalstarter = Object .FindObjectOfType<GoalStarter>();
			}
			return _goalstarter;
		}
	}

	Push _push;

	public Push push
	{
		get
		{
			if (!_push)
			{
				_push = Object .FindObjectOfType<Push>();
			}
			return _push;
		}
	}

	GoalMain _goalMain;

	public GoalMain goalMain
	{
		get
		{
			if (!_goalMain)
			{
				_goalMain = Object .FindObjectOfType<GoalMain>();
			}
			return _goalMain;
		}
	}

	BackMusic _backMusic;

	public BackMusic backMusic
	{
		get
		{
			if (!_backMusic)
			{
				_backMusic = Object .FindObjectOfType<BackMusic>();
			}
			return _backMusic;
		}
	}

	FrontMoverManagerData _frontMoverManagerData;

	public FrontMoverManagerData frontMoverManagerData
	{
		get
		{
			if (!_frontMoverManagerData)
			{
				_frontMoverManagerData = Object .FindObjectOfType<FrontMoverManagerData>();
			}
			return _frontMoverManagerData;
		}
	}

	TopRoutePoint _TopRoutePoint;

	public TopRoutePoint topRoutePoint
	{
		get
		{
			if (!_TopRoutePoint)
			{
				_TopRoutePoint = Object .FindObjectOfType<TopRoutePoint>();
			}
			return _TopRoutePoint;
		}
	}

	GameOverStarter _gameOverStarter;

	public GameOverStarter gameOverStarter
	{
		get
		{
			if (!_gameOverStarter)
			{
				_gameOverStarter = Object .FindObjectOfType<GameOverStarter>();
			}
			return _gameOverStarter;
		}
	}


	GameOverSystem _gameOverSystem;

	public GameOverSystem gameOverSystem
	{
		get
		{
			if (!_gameOverSystem)
			{
				_gameOverSystem = Object .FindObjectOfType<GameOverSystem>();
			}
			return _gameOverSystem;
		}
	}

	MoveButton _moveButton;

	public MoveButton moveButton
	{
		get
		{
			if (!_moveButton)
			{
				_moveButton = Object .FindObjectOfType<MoveButton>();
			}
			return _moveButton;
		}
	}

	EnemySEManager _enemySEManager;

	public EnemySEManager enemySEManager
	{
		get
		{
			if (!_enemySEManager)
			{
				_enemySEManager = Object .FindObjectOfType<EnemySEManager>();
			}
			return _enemySEManager;
		}
	}

	PlayerSE _playerSE;

	public PlayerSE playerSE
	{
		get
		{
			if (!_playerSE)
			{
				_playerSE = Object .FindObjectOfType<PlayerSE>();
			}
			return _playerSE;
		}
	}

	EnemyAdjustStep _enemyAdjustStep;

	public EnemyAdjustStep enemyAdjustStep
	{
		get
		{
			if (!_enemyAdjustStep)
			{
				_enemyAdjustStep = Object .FindObjectOfType<EnemyAdjustStep>();
			}
			return _enemyAdjustStep;
		}
	}

	Timer _timer;

	public Timer timer
	{
		get
		{
			if (!_timer)
			{
				_timer = FindObjectOfType<Timer>();
			}
			return _timer;
		}
    }

	Score _score;

	public Score score
	{
		get
		{
			if (!_score)
			{
				_score = FindObjectOfType<Score>();
			}
			return _score;
		}
	}

	ResultData _resultData;

	public ResultData resultData
	{
		get
		{
			if (!_resultData)
			{
				_resultData = FindObjectOfType<ResultData>();
			}
			return _resultData;
		}
	}

	EffectTest _effectTest;

	public EffectTest effectTest
	{
		get
		{
			if (!_effectTest)
			{
				_effectTest = FindObjectOfType<EffectTest>();
			}
			return _effectTest;
		}
	}

	UnityStandardAssets .ImageEffects.Blur _blur;

	public UnityStandardAssets .ImageEffects .Blur blur
	{
		get
		{
			if (!_blur)
			{
				_blur = FindObjectOfType<UnityStandardAssets .ImageEffects .Blur>();
			}
			return _blur;
		}
	}

	UICollection _uICollection;

	public UICollection uICollection
	{
		get
		{
			if (!_uICollection)
			{
				_uICollection = FindObjectOfType<UICollection>();
			}
			return _uICollection;
		}
	}

	FulScreenMesh _fulScreenMesh;

	public FulScreenMesh fulScreenMesh
	{
		get
		{
			if (!_fulScreenMesh)
			{
				_fulScreenMesh = FindObjectOfType<FulScreenMesh>();
			}
			return _fulScreenMesh;
		}
	}

	TouchPos _touchPos;

	public TouchPos touchPos
	{
		get
		{
			if (!_touchPos)
			{
				_touchPos = FindObjectOfType<TouchPos>();
			}
			return _touchPos;
		}
	}

	ColorScreen _colorScreen;

	public ColorScreen colorScreen
	{
		get
		{
			if (!_colorScreen)
			{
				_colorScreen = FindObjectOfType<ColorScreen>();
			}
			return _colorScreen;
		}
	}


	TimeLimit _timeLimit;

	public TimeLimit timeLimit
	{
		get
		{
			if (!_timeLimit)
			{
				_timeLimit = FindObjectOfType<TimeLimit>();
			}
			return _timeLimit;
		}
	}

	SpawnParticle _spawnParticle;

	public SpawnParticle spawnParticle
	{
		get
		{
			if (!_spawnParticle)
			{
				_spawnParticle = FindObjectOfType<SpawnParticle>();
			}
			return _spawnParticle;
		}
	}

	GameMainCamera _gameMainCamera;

	public GameMainCamera gameMainCamera
	{
		get
		{
			if (!_gameMainCamera)
			{
				_gameMainCamera = FindObjectOfType<GameMainCamera>();
			}
			return _gameMainCamera;
		}
	}

	TimeOverStarter _timeOverStarter;

	public TimeOverStarter timeOverStarter
	{
		get
		{
			if (!_timeOverStarter)
			{
				_timeOverStarter = FindObjectOfType<TimeOverStarter>();
			}
			return _timeOverStarter;
		}
	}
}
