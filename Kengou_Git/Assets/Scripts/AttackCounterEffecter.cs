using UnityEngine;
using System.Collections;

public class AttackCounterEffecter : MonoBehaviour {

	public AnimationCurve zAxisPos;

	public AnimationCurve xyAxisPos;

	public AnimationCurve upPos;

	public Vector3 StartPos;

	[SerializeField]
	public Transform EndTrans;

	[SerializeField]
	float Angle;

	[SerializeField]
	GameObject Particle;

	[SerializeField]
	public Vector3 StartAxis;

	public float MaxTime;

	public Enemy TargetEnemy; //特定の敵に攻撃するときのみ使う

	float time;

	bool Fixed;

	TrailRenderer _trailRenderer;

	TrailRenderer trailRenderer
	{
		get
		{
			if (!_trailRenderer)
				_trailRenderer = GetComponent<TrailRenderer>();
			return _trailRenderer;
		}
	}

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

	public bool IsEnd
	{
		get
		{
			return time > MaxTime;
		}
	}



	// Use this for initialization
	void Start () {
		StartPos = transform .position;
		trailRenderer .autodestruct = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!EndTrans && !Fixed)
		{
			EnableAutoDestruct();
			Fixed = true;
			Instantiate( Particle , transform .position , Quaternion .identity );
		}
		if (Fixed)
			return;
		//time += rhythmManager .DynamicRhythm .DeltaTime;

		time += staticScript.rhythmManager .DynamicRhythm .DeltaTime;

		float Progress = time / MaxTime;

		if (Progress > 1.0f)
		{
			Progress = 1.0f;
		}

		Vector3 ZAxis = EndTrans .position - StartPos;

		//Vector3 Right = Vector3 .Cross( Vector3 .up , ZAxis );

		//Vector3 Up = Vector3 .Cross(ZAxis , Right );

		//Right.Normalize();

		//Up.Normalize();

		//Vector3 XYAxis = Right * Mathf .Sin( Angle ) + Up * Mathf .Sin( Angle );

		Vector3 XYAxis = StartAxis;

		XYAxis .Normalize();
		XYAxis *= ZAxis .magnitude;

		Vector3 Up = Vector3 .up * ZAxis .magnitude;

		transform .position = StartPos + ZAxis * zAxisPos .Evaluate( Progress ) + XYAxis * xyAxisPos .Evaluate( Progress ) + Up * upPos .Evaluate( Progress );

		if (IsEnd)
		{
			Fixed = true;
			Instantiate( Particle , transform .position , Quaternion .identity );
		}
	}

	public void EnableAutoDestruct()
	{
		trailRenderer .autodestruct = true;
	
	}
}
