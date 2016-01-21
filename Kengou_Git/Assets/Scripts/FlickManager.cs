using UnityEngine;
using System.Collections;

//　参考
//http://stickpan.hatenablog.com/entry/2014/01/23/193922
//  ゲッターは一番下

public class FlickManager : MonoBehaviour
{

    private GameObject lig;    //  確認用ライト
    Color col = Color.white;



    private bool isFlick = false;           //  フリックかどうか
    private bool isEndFlick = false;        //  フリック終わり
    private bool isTouch = false;           //  タッチかどうか
    private Vector3 touchStartPos;          //  フリック開始位置
    private Vector3 touchBeforePos;         //  1f前の位置
    private Vector3 touchEndPos;            //  フリック途中orフリック終了位置
    private Vector2 direction;              //  方向 + 1fで進んだ距離
    private Vector2 distance;               //  一回のフリックで進んだ距離
    private int flickTime = 0;                  //  フリックした時間

    private bool isMove     = false;
    private bool markerF    = false;

    // Use this for initialization
    void Start()
    {
        //gameObject取得 
        lig = GameObject.Find("Directional Light");
        direction = new Vector3(0, 0, 0);
        distance =  new Vector3(0, 0, 0);
    }

    [SerializeField, TooltipAttribute("カーソル位置ログ &&　青:フリック成功 赤:フリック失敗"), HeaderAttribute("0:デバックなし 1:デバック"), Range(0, 1)]
    private int DebugFlag = 1;

    [SerializeField, TooltipAttribute("フリック有効時間(スライドかフリックか)"), HeaderAttribute ("フリック有効時間")]
    private float flickSpeed = 0.4f;
    [SerializeField, TooltipAttribute("どれだけ指を動かしたらタップでないと判断するか"), HeaderAttribute("フリック判断距離")]
    private float validDistance = 0.2f;

    [SerializeField, TooltipAttribute("画面に触れてからの時間"), HeaderAttribute("タップ判断時間")]
    private float tapTime = 0.15f;

    //  有効範囲X
    //[SerializeField, TooltipAttribute("画面右側の有効範囲"), HeaderAttribute("画面:フリック有効範囲X")]
    private float MAX_ScopeX = Screen.width;
    //[SerializeField, TooltipAttribute("画面左側の有効範囲")]   
    private float MIN_ScopeX = 0;
    

    //  有効範囲Y
    //[SerializeField, TooltipAttribute("画面上側の有効範囲"), HeaderAttribute("画面:フリック有効範囲Y")]
    private float MAX_ScopeY = Screen.height;
    //[SerializeField, TooltipAttribute("画面下側の有効範囲")]
    private float MIN_ScopeY = 0;


    StaticScript _staticScript;
    StaticScript staticScript
    {
        get
        {
            if (!_staticScript)
                _staticScript = Object.FindObjectOfType<StaticScript>();
            return _staticScript;
        }

    }


    public void Update()
    {
        markerF = false;

        //  フラグ初期化
        isEndFlick = false;
        //  フリック中なら
        if (IsFlick())
        {
            //  経過時間
            flickTime++;
            //  1f前の位置
            touchBeforePos = touchEndPos;

        }
		MAX_ScopeX = Screen .width;
		MAX_ScopeY = Screen .height;
        //  押し始め
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
           
            //  タップした位置の取得
            touchStartPos = new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y,
                        Input.mousePosition.z);


            //  有効範囲内なら
            if (touchStartPos.x >= MIN_ScopeX && touchStartPos.x <= MAX_ScopeX &&
                touchStartPos.y >= MIN_ScopeY && touchStartPos.y <= MAX_ScopeY)
            {
                //  タイム初期化
                flickTime = 0;
                //  タッチフラグオン
                TouchOn();
                FlickOn();
                //  位置の初期化
                touchBeforePos = touchStartPos;
                touchEndPos = touchStartPos;
                //  設定した時間後,フリック受付をオフにする
                Invoke("FlickOff", flickSpeed);
            }
        }

        //  デバック
        if (DebugFlag == 1)
            FlickDebug();


        //  押している間
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //  位置を取り続ける
            touchEndPos = new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y,
                        Input.mousePosition.z);
           
        }
        
        
        //  指を離した
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //  最終位置
            touchEndPos = new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y,
                        Input.mousePosition.z);

            Vector3 vec = touchEndPos - touchStartPos;
            vec.z = 0;

            //  タッチかフリックか
            if (vec.sqrMagnitude >= validDistance * validDistance)
            {
                //  タッチではない        
                TouchOff();
            }
           
            //  フリックした！
            if (IsFlick() && vec.sqrMagnitude >= validDistance * validDistance)
            {
                //  位置差分
                //  1f
                direction.x = touchEndPos.x - touchBeforePos.x;
                direction.y = touchEndPos.y - touchBeforePos.y;
                //  フリック始めから終わりまで
                distance.x = touchEndPos.x - touchStartPos.x;
                distance.y = touchEndPos.y - touchStartPos.y;


                //  フリック終わり
                isEndFlick = true;
                FlickOff();
                
            }
            else
            {
                //  フリックではなかった
                direction.x = 0;
                direction.y = 0;
                FlickOff();
            }

            
        }
        //  フリックをしている途中なら
        if (IsFlick())
        {
            //  方向 + 1fで進んだ距離更新
            direction.x = touchEndPos.x - touchBeforePos.x;
            direction.y = touchEndPos.y - touchBeforePos.y;
            //  フリック始めからいままで
            distance.x = touchEndPos.x - touchStartPos.x;
            distance.y = touchEndPos.y - touchStartPos.y;
        }
      
      

        
    }
    private void FlickDebug()
    {
        //  マウス位置表示
        Vector3 Dpos = new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y,
                        Input.mousePosition.z);

        Debug.Log(Dpos);

        //  指を離した
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            bool check = true;
            //  最終位置
            Vector3 DebugPos = new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y,
                        Input.mousePosition.z);

            Vector3 vec = DebugPos - touchStartPos;

            //  タッチかフリックか
            if (Vector3.Distance(Vector3.zero, vec) >= validDistance)
            {
                //  タッチではない        
                check = false;
            }

            //  フリックした！
            if (IsFlick() && !check)
            {
                col = Color.blue;
                Invoke("ColorWhite", 1.0f);

            }
            else
            {
                //  フリックではなかった
                col = Color.red;
                Invoke("ColorWhite", 1.0f);
            }
        }
        //色変更
        lig.GetComponent<Light>().color = col;
    }


    //  色を戻す（デバック用
    private void ColorWhite()
    {
        col = Color.white;

    }

    
    //  タッチした
    private void TouchOn()
    {
        isTouch = true;
        Invoke("TouchOff", tapTime);
    }
    //  タッチしたかどうか
    private bool IsTouch()
    {
        return isTouch;
    }
    //  タッチではない
    private void TouchOff()
    {
        isTouch = false;
    }

    //  フリックする
    private void FlickOn()
    {
        isFlick = true;
    }
    //  フリックではない
    private void FlickOff()
    {
        direction.x = 0;
        direction.y = 0;
        isFlick = false;
    }
    /////////////////////////////////////////
    /*              ゲッター               */
    /////////////////////////////////////////
    //  フリックしているかどうか
    public bool IsFlick()
    {
        return isFlick;
    }
    //  フリック終わり
    public bool IsEndFlick()
    {
        return isEndFlick;
    }

    //  フリック開始終わり位置
    public Vector2 FlickStartPos
    {
        get
        {
            Vector2 flickPos = new Vector2(touchStartPos.x, touchStartPos.y);
            return flickPos;
        }

    }
    //  フリック終わり位置(現在のフリック位置含む)
    public Vector2 FlickEndPos
    {
        get{
            Vector2 flickPos = new Vector2(touchEndPos.x,touchEndPos.y);
            return flickPos;
        }

    }

    public Vector3 FlickStartPos3
    {
        get
        {
            return touchStartPos;
        }

    }
    //  フリック終わり位置(現在のフリック位置含む)
    public Vector3 FlickEndPos3
    {
        get
        {
            return touchEndPos;
        }

    }

    //  フリック速度(一回のフリックの場合(平均速度
    public float FlickSpeed
    {
        get { if (flickTime == 0)flickTime = 1; return Mathf.Sqrt(Mathf.Pow(distance.x, 2) + Mathf.Pow(distance.y, 2)) / flickTime; }
    }
    //  フリック速度(１フレーム
    public float OneFlickSpeed
    {
        get { return direction.magnitude; }
    }

    //  方向取得(一回のフリックの場合
    //public Vector2 FlickDirection
    //{
    //    get {
    //        Vector3 hoge = new Vector3(distance.x,
    //                                distance.y, 0);
    //        hoge = hoge.normalized;
    //        Vector2 normalize = new Vector2(hoge.x, hoge.y);
    //        return normalize;
    //    }
    //}

    //  方向取得(1フレーム
    public Vector2 FlickDirection
    {
        get
        {
            Vector3 hoge = new Vector3(direction.x,
                                    direction.y, 0);
            hoge = hoge.normalized;
            Vector2 normalize = new Vector2(hoge.x, hoge.y);
            return normalize;
        }
    }


    public bool IsTouch2()
    {        
        return isTouch;
        
    }

    public bool MarkeMoveTymingFlag()
    {
        if (!markerF)
        {
            foreach (Marker m in staticScript.markerManager.MarkerList)
            {
                if (m == null)
                    continue;

                if (m.IsSwordTyming)
                {
                    markerF = true;
                    break;
                }
            }
        }
        return markerF;
    }

    public bool IsMove()
    {
        isMove = false;

        MarkeMoveTymingFlag();
        if (isTouch && !isFlick && !markerF)
        {
            isMove = true;

        }
        return isMove;

    }

}

