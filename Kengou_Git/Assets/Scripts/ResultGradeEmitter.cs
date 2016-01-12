using UnityEngine;
using System.Collections;

public class ResultGradeEmitter : MonoBehaviour {

	[SerializeField]
	ResultGrade emit;

	[SerializeField]
	ResultGuideEffecter Effecter;

	[SerializeField]
	Firework[] FireWork;

	[SerializeField]
	bool FireWorkEmit;

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
		for (int i = 0;i < staticScript.bulletResultManager.PulseResultDataCount;i++)
		{
			BulletResultData data = staticScript .bulletResultManager .GetPulseResultData( i );
			if (!data .Miss && FireWorkEmit)
				Instantiate(FireWork[Mathf.Abs(data.ResultRank)].gameObject,data .MarkerWorldPos , Quaternion .identity );

			GameObject tmp = ( Instantiate( emit.gameObject , data .MarkerWorldPos , Quaternion .identity )  as GameObject );

			ResultGrade grade = tmp .GetComponent<ResultGrade>();
			grade .score .SetNum( data .Score );
			grade .SetSprite( Mathf .Abs( data .ResultRank ) );
			grade .transform .parent = staticScript .markerCanvas .transform;
			if(Effecter != null)
			{
				GameObject tmp2 = ( Instantiate( Effecter .gameObject , data .MarkerWorldPos , Quaternion .identity ) as GameObject );
				ResultGuideEffecter guideeffecter = tmp2 .GetComponent<ResultGuideEffecter>();
				guideeffecter .transform .parent = staticScript .markerCanvas .transform;
				guideeffecter .SetSprite( grade .sprite );

			}
		}
	}
}
