using UnityEngine;
using System.Collections;

public class SelectBackMove : MonoBehaviour {

	[SerializeField]
	Vector3 Vel;

	[SerializeField]
	bool OnMove = false;
	StageSelectMain _SelectMain;
	StageSelectMain SelectMain
	{
		get
		{
			if (!_SelectMain)
				_SelectMain = Object.FindObjectOfType<StageSelectMain>();
			return _SelectMain;
		}
	}
	// Use this for initialization
	void Start () {
        OnMove = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!SelectMain.OnStageSelect)return;
		Vector3 Pos = this.transform.position;
		Pos += Vel * Time.deltaTime;
        Vel += Vel;
		this.transform.position = Pos;

        if (Mathf.Abs(Pos.x) > 3000 || Mathf.Abs(Pos.y) > 3000) Destroy(gameObject);
	}
}
