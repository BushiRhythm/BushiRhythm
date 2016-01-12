using UnityEngine;
using System.Collections;

public class GoalMain : MonoBehaviour {


	[SerializeField]
	AnimationCurve CameraCurve;
	float Val = .0f;
	
	Quaternion OrgAng;

    public GameObject StarMine;
    public GameObject FadeOutScreen;
    public GameObject Sokomade;

	public GameObject Osyou;

	ScreenFadeout _FScreen;
	
	ScreenFadeout FScreen
	{
		get
		{
			if (!_FScreen)
				_FScreen = Object.FindObjectOfType<ScreenFadeout>();
			return _FScreen;
		}
	}

    RhythmManager _RManager;
    RhythmManager RManager
    {
        get
        {
            if (!_RManager)
                _RManager = Object.FindObjectOfType<RhythmManager>();
            return _RManager;
        }
    }

	// Use this for initialization
	void Start () {
		OrgAng = Camera.main.transform.rotation;
        FadeOutScreen.SetActive(false);
        Sokomade.SetActive(false);
        
	}

    bool SceneFlag = false;

	// Update is called once per frame
    float DTWait = 0;
    int Wait = 0;
    void Update()
    {
		if (Val < 1.0f) {
            if (Sokomade) Sokomade.SetActive(true);
            Val = Mathf.Min(Val + 0.75f * Time.deltaTime, 1.0f);
		}
		Quaternion ang = OrgAng;
		ang.x = ang.x - CameraCurve.Evaluate (Val) * 0.4f;
		Camera.main.transform.rotation = ang;

		Vector3 MinePos = new Vector3(Random.Range(-30,30),35.0f + Random.Range(-10,10),30.0f);

		Quaternion rot = transform.rotation;
        DTWait = DTWait + Time.deltaTime;
        if (DTWait>1.0f) {
            DTWait = .0f;
            Wait++;
        }
        if (Wait % 2 == 0)
        {
            Instantiate(StarMine, MinePos, rot);
            Wait++;
        }
        if (Wait > 3)
        {
            FadeOutScreen.SetActive(true);

			FScreen.FadeEnable();
			if(FScreen.IsFadeComplete() && !SceneFlag){
                SceneFlag = true;
				Application.LoadLevelAsync("Result");
			}
		}
	}
}
