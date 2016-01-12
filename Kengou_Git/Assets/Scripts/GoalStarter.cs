using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoalStarter : MonoBehaviour {

	bool IsRun = false;

	bool IsMarkerEmited = false;

	float time = .0f;

	bool CameraRotStart = false;

	float RotTime = .0f;

	float uIAlpha = .0f;

	bool CameraChanged = false;

	bool Se = false;

	int FireWorkCount = 0;

	[SerializeField]
	Vector3 AdjustGoalEnemy;

	[SerializeField]
	SE GoalTaiko;

	Quaternion BeforeRot;
	Quaternion AfterRot;

	public void FireCountUp()
	{
		FireWorkCount++;
	}

	public float UIAlpha
	{
		get
		{
			return uIAlpha;
		}
	}

	public bool IsRuned
	{
		get
		{
			return IsRun;
		}
	}

	Camera _goalCamera;

	Camera goalCamera
	{
		get
		{
			if (!_goalCamera)
				_goalCamera = GameObject .Find( "Goal Camera" ).GetComponent<Camera>();
			return _goalCamera;
		}
	}

	[SerializeField]
	AnimationCurve RendererAlpha;

	[SerializeField]
	AnimationCurve CameraCurve;

	[SerializeField]
	Button[] buttons;

	[SerializeField]
	CanvasRenderer[] renderers;

	[SerializeField]
	Marker GoalMarker;

	[SerializeField]
	GoalEnemy goalEnemy;

	GoalEnemy EmitEnemy;

	Marker EmitMarker;

	StaticScript _staticScript;

	StaticScript staticScript
	{
		get
		{
			if (!_staticScript)
				_staticScript = GameObject .FindGameObjectWithTag( "StaticScript" ) .GetComponent<StaticScript>();
			return _staticScript;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!IsMarkerEmited)
		{
			GameObject emitobj = Instantiate( goalEnemy .gameObject , transform.position , Quaternion .Euler( new Vector3( 0 , 180 , 0 ) ) ) as GameObject;
			EmitEnemy = emitobj.GetComponent<GoalEnemy>();
			EmitMarker = staticScript .staticMarkerEmitter .Emit( GoalMarker.gameObject , new Vector2( 0 , 0 ) , new Vector3( 0 , 0 , 0 ) , EmitEnemy , true );
			EmitMarker .LoopSetting();
			EmitMarker .IsCanSwordAttackDisable();
			IsMarkerEmited = true;
		}
		if (EmitEnemy != null)
			EmitEnemy .transform .position = staticScript .frontMoverManager .GetPosFromStep( staticScript .frontMoverManager .GoalPos ) + AdjustGoalEnemy;


		staticScript .goalMain.transform.position = staticScript .frontMoverManager.GetPosFromStep( staticScript .frontMoverManager.GoalPos);

		if (staticScript .frontMoverManager .IsGoal && EmitMarker != null)
		{
			EmitMarker .IsCanSwordAttackEneble();
		}
		if(EmitMarker== null)
		{
			if (!IsRun)
			{
				staticScript .push .enabled = false;
				staticScript .timer .Stop();
				staticScript .score .Stop();
				staticScript .resultData .Stop();

				staticScript .actionTymingManager .Disable();
				//staticScript .backMusic .Stop();

				staticScript .topEnemyManager .Stop();

				staticScript .rhythmManager .Stop();

				foreach (Button button in buttons)
				{
					button .enabled = false;
				}

			}
			IsRun = true;
		}
		if(IsRun)
		{
            time += Time.unscaledDeltaTime;
			if(/*uIAlpha <= .0f &&*/ !CameraChanged)
			{
                goalCamera.transform.position = Camera.main.transform.position;
				AfterRot = Camera.main.transform.rotation;
                Camera.main.enabled = false;
                goalCamera.enabled = true;
                //staticScript.goalMain.enabled = true;
				CameraChanged = true;
			}
			goalCamera .transform .forward = ( EmitEnemy .EmitFireWork .transform .position - goalCamera.transform .position ) .normalized;

			if(FireWorkCount  >= 1)
			{
				if(!CameraRotStart)
				{
					BeforeRot = goalCamera .transform .rotation;
					CameraRotStart = true;
					staticScript .goalMain .Osyou .SetActive( true );
					Destroy( EmitEnemy .gameObject );
				}
				else
				{
					RotTime += Time .deltaTime;
					float work = CameraCurve.Evaluate(RotTime);

					if (work > 1.0f)
						work = 1.0f;
					goalCamera .transform .rotation = Quaternion .Slerp( BeforeRot , AfterRot , work );

					if(work == 1.0f)
					{
						staticScript .goalMain .enabled = true;
						if(!Se)
						{
							Instantiate( GoalTaiko .gameObject );
							Se = true;
						}
					}
				}
			}

		}
			uIAlpha = RendererAlpha .Evaluate( time );	
		foreach (CanvasRenderer renderer in renderers)
		{
			renderer .SetAlpha( renderer .GetAlpha() * uIAlpha );
		}
	}
}
