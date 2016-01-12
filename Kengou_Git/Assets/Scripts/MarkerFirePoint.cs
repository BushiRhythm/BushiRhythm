using UnityEngine;
using System.Collections;

public class MarkerFirePoint : MonoBehaviour {

    
    [SerializeField]
    MarkerBullet _markerbullet;

	// Bulletの速度設定します
	void Start () {


	}


    public void SetStaticFirePoint()
    {
        //弾の出現ポイントを設定
       transform.position = GameObject.Find("StaticFirePoint").transform.position;
       Debug.Log("座標" + transform.parent.localPosition);
    }

	public void SetStaticFirePoint(Vector3 pos)
	{
		//弾の出現ポイントを設定
		transform .position = pos;
		//Debug .Log( "座標" + transform .parent .localPosition );
	}


    public void SettingSpeed()
    {
        // 発射位置から着弾位置までの距離を計算
        Vector3 Len = transform.parent.position - transform.position;

        _markerbullet.Length = Len;

        // Markerから着弾時間を取得
        float RestTime = GetComponentInParent<Marker>().RestTime;

        // 着弾時間丁度に着弾するように速度を設定
        float Speed = Len.magnitude / RestTime;

        //距離を正規化
        Len.Normalize();

        //Rigidbodyに設定
        Transform Child = transform.GetChild(0);
        GetComponentInChildren<Rigidbody>().velocity = Len * Speed;
        Child.forward = Len;
        Debug.Log("移動適用"  + GetComponentInChildren<Rigidbody>().velocity);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("移動量" + GetComponentInChildren<Rigidbody>().velocity);

	}
}
