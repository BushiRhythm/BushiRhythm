using UnityEngine;
using System.Collections;

public class GoalEnemy : Enemy {

	[SerializeField]
	FireWorkShooter goalFireWork;

	FireWorkShooter emitFireWork;

	[SerializeField]
	NumberSet numberSet;

	[SerializeField]
	SpriteRenderer Cutrender;

	StaticScript _staticScript;

	protected StaticScript staticScript
	{
		get
		{
			if (!_staticScript)
				_staticScript = GameObject .FindGameObjectWithTag( "StaticScript" ) .GetComponent<StaticScript>();
			return _staticScript;
		}
	}
	

	public FireWorkShooter EmitFireWork
	{
		get
		{
			return emitFireWork;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	void Update()
	{
		int restStep = staticScript.frontMoverManager.RestStep;
		if(restStep > 0)
		{
			numberSet .gameObject .SetActive( true );
			Cutrender .gameObject .SetActive( false );

			numberSet .SetNum( restStep );
		}
		else
		{
			numberSet .gameObject .SetActive( false );
			Cutrender .gameObject .SetActive( true );

			numberSet .SetNum( restStep );
		}
	}
	override public void MarkerEmitAction()
	{

	}

	override public void MarkerDestroyAction()
	{
		GameObject obj = Instantiate( goalFireWork.gameObject , transform .position , transform .rotation ) as GameObject;

		emitFireWork = obj .GetComponent<FireWorkShooter>();
	}
}
