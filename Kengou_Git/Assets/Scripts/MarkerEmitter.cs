using UnityEngine;
using System.Collections;

public class MarkerEmitter : MonoBehaviour {

   public Vector2 Pos;

   public float SetTime;

    [SerializeField]
   private Canvas canvas;

    [SerializeField]
   private GameObject marker;

    void Emit()
    {
        GameObject tmp = Instantiate(marker, Vector3.zero, Quaternion.identity) as GameObject;
        Transform FirePoint = tmp.transform.GetChild(0);


            //Suc↓
        //tmp.transform.GetChild(0).GetComponent<MarkerFirePoint>().SetStaticFirePoint();
       // Debug.Log("TestSTR:" + );
        Marker markerCompornent = tmp.transform.GetComponent<Marker>();
        markerCompornent.MarkerCanvas = canvas.gameObject;
        tmp.transform.parent = canvas.transform;
        tmp.transform.localScale = marker.transform.localScale;
        //今だけランダム
       // tmp.transform.GetComponent<Marker>().PosOnScreen = GetComponent<MarkerEmitter>().Pos;
        markerCompornent.PosOnScreen.x = Random.Range(-0.3f, 0.3f);
        markerCompornent.PosOnScreen.y = Random.Range(-0.3f, 0.3f);
        markerCompornent.SetTime(SetTime);
        markerCompornent.SettingPos();
            //MarkerFirePoint markerFirePoint = emit.transform.GetChild(0).GetComponent<MarkerFirePoint>();

        //    //markerFirePoint = emit.GetComponentInChildren<MarkerFirePoint>();
        //Debug.Log(emit.transform.GetChild(0).ToString());
        //    //if(!markerFirePoint)
        //    //{
        //    //    Debug.Log("ポイントエラー");
        //    //}
        FirePoint.GetComponent<MarkerFirePoint>().SetStaticFirePoint();
        FirePoint.GetComponent<MarkerFirePoint>().SettingSpeed();
    }
	// Use this for initialization
	void Start () {

	}
	
    public void MarkerEmit(Object Data )
    {


    }
	// Update is called once per frame
	void Update () {
	
	}
}
