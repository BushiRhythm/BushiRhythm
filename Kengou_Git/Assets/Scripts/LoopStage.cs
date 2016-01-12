using UnityEngine;
using System.Collections;

public class LoopStage : MonoBehaviour {

    public GameObject EmitMesh;
    [SerializeField]
    float EmitFar = 20;

    private GameObject FinalEmit;   //最後に生成した物体のトランスフォーム

    Camera mainCamera;

	Camera MainCamera
	{
		get
		{
			if(!mainCamera)
			{
				mainCamera = Camera.main;
			}
			return mainCamera;

		}
	}

	// Use this for initialization
	void Start () {

        FinalEmit = transform.gameObject;
        //とりあえずひとつ生成
        Emit();
        
	}
	
	// Update is called once per frame
    //カメラが射影内にあるとき永遠にステージを生成する
	void Update () {
        Vector3 CameraFront = MainCamera.transform.forward;
        CameraFront.Normalize();
        Vector3 CameraLen;
        float Dot;
        float Near =  MainCamera.nearClipPlane;

        while(true)
        {
			CameraLen = FinalEmit .transform .position - MainCamera .transform .position;
            Dot = Vector3.Dot(CameraLen, CameraFront);
            if (Dot < EmitFar && Dot > Near)
                Emit();
            else
                break;
        }

    
	}

    void Emit()
    {
        FinalEmit = (GameObject)Instantiate(EmitMesh, FinalEmit.transform.position, FinalEmit.transform.rotation);
        FinalEmit = FinalEmit.GetComponent<EmitMesh>().EmitterPoint;
    }
}
