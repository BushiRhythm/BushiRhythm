using UnityEngine;
using System.Collections;



//  とりあえずの実装　あとで変える

public class Sword : MonoBehaviour {

    [SerializeField]
    bool hitStop2 = false;

    bool hitStop = false;

    bool IsLood = false;
    

    ActionTymingManager _actionTymingManager;
    ActionTymingManager actionTymingManager
    {
        get
        {
            if (!_actionTymingManager)
                _actionTymingManager = Object.FindObjectOfType<ActionTymingManager>();
            return _actionTymingManager;
        }

    }

    RhythmManager _rhythmManager;
    RhythmManager rhythmManager
    {
        get
        {
            if (!_rhythmManager)
                _rhythmManager = Object.FindObjectOfType<RhythmManager>();
            return _rhythmManager;
        }

    }

    SwordCollision _swordcollision;
    SwordCollision swordcollision
    {
        get
        {
            if (!_swordcollision)
                _swordcollision = Object.FindObjectOfType<SwordCollision>();
            return _swordcollision;
        }

    }

    FlickManager _flickManager;
    FlickManager flickManager
    {
        get
        {
            if (!_flickManager)
                _flickManager = Object.FindObjectOfType<FlickManager>();
            return _flickManager;
        }

    }

    SwordActionSetter _swordActionSetter;
    SwordActionSetter swordActionSetter
    {
        get
        {
            if (!_swordActionSetter)
                _swordActionSetter = Object.FindObjectOfType<SwordActionSetter>();
            return _swordActionSetter;
        }

    }

   // SwordEffectAnimation _swordEffectAnimation;
    SwordEffectAnimation swordEffectAnimation;
    //{
    //    get
    //    {
    //        if (!_swordEffectAnimation)
    //            _swordEffectAnimation = Object.FindObjectOfType<SwordEffectAnimation>();
    //        return _swordEffectAnimation;
    //    }
    //
    //}
    Transform swordEffectAnimationTransform;

    private GameObject MarkerCanvas;

    private GameObject tuba;
    private GameObject cube4;
    private GameObject tuka;
    private GameObject ha;
    private Color swordColor;
    private float alpha = 255;
    [SerializeField]
    private float alphaSpeed = 100;


    private GameObject Collision2D;
    RectTransform CollisionTransform;

    Vector3 FlickStartPosVec3;
    Vector3 FlickEndPosVec3;
    
   
    //  フリック
    GameObject Flick;
    Vector2 FlickDirection;

    //  当たり判定用Obj
    GameObject CollisionCube = null;

    [SerializeField, HeaderAttribute("攻撃時最低速度")]
    private float swordSpeed = 1200;

    //[SerializeField]
    float timar = 0;

    int step = 0;
    Vector2 interpolationVec;
    [SerializeField, HeaderAttribute("補間速度")]
    float interpolationTime = 10;

    float rad;

    //  カメラ情報
    private Vector3 CameraPos;
    private Vector3 CameraFront;
    private Vector3 CameraUp;
    private Vector3 CameraRight;

    Vector2 FlickEndPos;
    Vector2 FlickStartPos;


    [SerializeField, HeaderAttribute("刀位置"), TooltipAttribute("前:z")]
    private float swordFront = 0.33f;
    [SerializeField, TooltipAttribute("上:y")]
    private float swordUp = -0.17f;
    [SerializeField, TooltipAttribute("右:x")]
    private float swordRight = 0.28f;

    //  Collision2Dの    
    BoxCollider Collision;

    private GameObject Collision2DFlick;

    RectTransform CollisionFlickTransform;
    BoxCollider CollisionFlick;
    [SerializeField, TooltipAttribute("軌跡サイズ")]
    private float locusPositionX;

    [SerializeField, TooltipAttribute("軌跡サイズ")]
    private float locusPositionY;
    enum SwordType
    {
        MOVE,
        ATTACK,
        INTERPOLATION,
        STOP,
        ALPHA,

        TypeNum
    };

    float stopTimer=0;
    
    SwordType swordType = SwordType.MOVE;
    //  1つ前の状態
    SwordType swordTypePrevious = SwordType.MOVE;

    RectTransform MarkerCanvasRectTransform;


    //  仮
    SwordType swordTypeStop = SwordType.MOVE;


    bool posFlag = false;
    Vector3 startPos = Vector3.zero;

    Quaternion LocusQ;



	void Start () {
        Flick = GameObject.Find("Flick");
        Collision2D = GameObject.Find("swordCollision");
        
        Collision = Collision2D.GetComponent<BoxCollider>();
        
       // Collision.enabled = false;
        CollisionTransform = Collision2D.GetComponent<RectTransform>();

        Collision2DFlick = GameObject.Find("swordCollisionFlick");
        CollisionFlick = Collision2DFlick.GetComponent<BoxCollider>();
        

        tuba = GameObject.Find("tuba");
        cube4 = GameObject.Find("cube4");
        tuka = GameObject.Find("tuka");
        ha = GameObject.Find("ha");

        swordColor = tuba.GetComponent<Renderer>().material.color;
        swordColor.a = 0;

        
        MarkerCanvas = GameObject.Find("MarkerCanvas");
        MarkerCanvasRectTransform = MarkerCanvas.GetComponent<RectTransform>();
        
	}
	
	void Update () {

        if (!IsLood)
        {
            
            GameObject SEA = GameObject.Find("SwordEffectAnimation");
            
            if (SEA == null)
                return;
            swordEffectAnimation = SEA.GetComponent<SwordEffectAnimation>();
            swordEffectAnimationTransform = GameObject.Find("SwordEffectAnimation").transform;
            //swordEffectAnimationTransform = SEA.transform;
            //swordEffectAnimationTransform.position = Vector3.zero;
            IsLood = true;
        }
        
        
        
        
        //if (Collision2D.GetComponent<BoxCollider2D>().enabled)
        //    Debug.Log("当たる！");
        
        //actionTymingManager.IsBladeActiond
        //  カメラ情報取得(Pos,Front,Up,Right)
        GetCameraInfo();

        if (actionTymingManager.IsBladeActiond())
            ChangeType(SwordType.MOVE);
       // Collision.enabled = false;
        if (swordType == SwordType.MOVE)
            SwordMove();
       
        if (swordType == SwordType.ATTACK)
            SwordAttack();

        if (swordType == SwordType.INTERPOLATION)
            SwordInterpolation();

        if (swordType == SwordType.STOP)
            SwordStop();

        if (swordType == SwordType.ALPHA)
            SwordAlpha();

        if(hitStop2)
        {
            
            HitStop();
            hitStop2 = false;
        }
        posFlag = false;
        tuba.GetComponent<Renderer>().material.color = swordColor;
        cube4.GetComponent<Renderer>().material.color = swordColor;
        tuka.GetComponent<Renderer>().material.color = swordColor;
        ha.GetComponent<Renderer>().material.color = swordColor;


	}

    private void GetCameraInfo()
    {
        Matrix4x4 m = Matrix4x4.TRS(Camera.main.transform.position, Camera.main.transform.rotation, Camera.main.transform.localScale);

        CameraPos.x = m.m03;
        CameraPos.y = m.m13;
        CameraPos.z = m.m23;

        CameraFront.x = m.m02;
        CameraFront.y = m.m12;
        CameraFront.z = m.m22;

        CameraUp.x = m.m01;
        CameraUp.y = m.m11;
        CameraUp.z = m.m21;

        CameraRight.x = m.m00;
        CameraRight.y = m.m10;
        CameraRight.z = m.m20;

    }

    private void SwordMove()
    {
        this.transform.position = CameraPos + CameraFront * swordFront + CameraUp * swordUp + CameraRight * swordRight;

        Quaternion CameraQ = Camera.main.transform.rotation;
       
        this.transform.rotation = CameraQ;



        
        if (actionTymingManager.IsBladeActiond())
        {
            swordcollision.fastPos = Vector3.zero;
            swordcollision.secondPos = Vector3.zero;
            Vector3 pos = Vector3.zero;
            Vector3 scale = Vector3.zero;
            Quaternion q = Quaternion.identity;

            FlickSword(ref pos, ref q, ref scale);

            Vector3 p = Input.mousePosition;
            p.x -= Screen.width / 2;
            p.y -= Screen.height / 2;
            p.z = 0;
            
            //transform.localPosition = p;

            CollisionTransform.localPosition = pos;
            CollisionTransform.rotation = q;
            CollisionTransform.localScale = scale;
            //Vector3 ST = CollisionTransform.localPosition;


            //Collision2D.transform.rotation = ColiQ;
            //Collision2D.transform.localScale = CubeScale;

            
            ChangeType(SwordType.ATTACK);
        
        }
       

    }

    private void SwordAttack()
    {
        swordColor.a = 0;
        Vector2 AP = AttackPos((timar / 180) / 2 + 0.25f);
        //this.transform.position = CameraPos + CameraFront * swordFront + CameraUp * AP.y * swordUp + CameraRight * AP.x * swordRight;

        Quaternion CameraQ = Quaternion.AngleAxis(rad, CameraFront);

        Quaternion hoge = Quaternion.AngleAxis(timar, CameraRight);

        //Vector3 v = flickManager.FlickEndPos3 * interpolationTime + flickManager.FlickStartPos3 * (1.0f - interpolationTime);
        //
        //v = Camera.main.ScreenToWorldPoint(v);
        //
        //this.transform.position = CameraPos + CameraFront / 2 + CameraUp * AP.y + CameraRight * AP.x;

        //(CameraQ, hoge);
        this.transform.rotation = CameraQ * hoge;// Quaternion.RotateTowards(CameraQ, hoge, z);//Quaternion.Lerp(CameraQ, hoge, z);

        //  一瞬だけ当たり判定
       // if (timar == 0)
       // {
       //     CollisionCube.transform.position = this.transform.position;
       //     CollisionCube.transform.rotation = CameraQ;
       //
       // }
       // else
       // {
       //     Destroy(CollisionCube);
       //     CollisionCube = null;
       // }
        if (timar == 0)
        {
            Vector3 scale = new Vector3(60, 60, 60);
            //Vector3 scale = new Vector3(0.06f, 0.06f, 0.06f);
            LocusQ = Quaternion.AngleAxis(rad - 90, CameraFront);
            hoge = Quaternion.AngleAxis(timar, CameraRight);
            FlickStartPosVec3 = flickManager.FlickStartPos3;
            FlickEndPosVec3 = flickManager.FlickEndPos3;
            Marker m = swordActionSetter.HitMarker;
            if(m != null)
                m.DestroyRequest();
            

            //pos += swordEffectAnimationTransform.right * 0.1f;

            
            swordEffectAnimation.AnimationStart();
        }
        //Vector2 EfPos = AttackPos(0.5f);
        //Vector3 pos = CameraPos + CameraFront * swordFront + CameraUp * EfPos.y * swordUp + CameraRight * EfPos.x * swordRight;
       //Vector2 EfPos = (FlickStartPos + FlickEndPos) * 0.5f;
       //EfPos.x /= Screen.width;
       //EfPos.y /= Screen.height;
       //EfPos.x *= 500;
       //EfPos.y *= 300;

       //Vector3 pos = new Vector3(EfPos.x, EfPos.y, 0);

        //Vector3 CP = Camera.main.transform.position;
        //Vector3 CP = MarkerCanvas.transform.position;
        //float vz = Camera.main.WorldToViewportPoint(CP).z;
        //
        //
        //Vector2 EfPos = FlickStartPos;
        //EfPos.x /= Screen.width;
        //EfPos.y /= Screen.height;
        //EfPos.y += 0.3f;
        //
        //
        //Vector3 VPos = new Vector3(EfPos.x, EfPos.y, vz);
        //Vector3 pos = Camera.main.ViewportToWorldPoint(VPos);
        //
        //pos.z += 0.5f;
        ////Vector3 pos = new Vector3(EfPos.x, EfPos.y);
        //swordEffectAnimationTransform.localPosition = pos;
        //

       
       

        if(swordActionSetter.FlickNotCheck)
        {
            Vector3 scale = new Vector3(60, 60, 60);

            Vector3 FSP = flickManager.FlickStartPos3;
            Vector3 FEP = flickManager.FlickEndPos3;

            FlickDirection = FEP - FSP;
            if (FlickDirection == Vector2.zero)
                return;

            float leng = Mathf.Sqrt(Mathf.Pow(FlickDirection.x, 2) + Mathf.Pow(FlickDirection.y, 2));
            leng /= Screen.width;
            scale.x = Screen.width *0.1f ;//locusPositionX * leng * Screen.width;
            scale.y *= locusPositionY * Screen.height;
            

            swordEffectAnimationTransform.rotation = Quaternion.identity;
            swordEffectAnimationTransform.localScale = scale;
            swordEffectAnimationTransform.rotation = LocusQ;
        }

       // Vector3 ppp = (FlickStartPosVec3 + FlickEndPosVec3) * 0.5f;
        Vector3 ppp = (FlickEndPosVec3);

        ppp.x -= Screen.width /  2;
        ppp.y -= Screen.height / 2;
        

        swordEffectAnimationTransform.localPosition = ppp;

        if(timar > 180)
        {
            //timar = 0;
            //swordType = SwordType.STOP;
            //ChangeType(SwordType.STOP);
            ChangeType(SwordType.ALPHA);
        }

        timar += Time.deltaTime * swordSpeed;

    }

    private void SwordInterpolation()
    {
        Vector2 vec = new Vector2(swordRight, swordUp);
       if(step == 0)   
       {
           if (swordTypePrevious == SwordType.MOVE)
           {

               interpolationVec = AttackPos(0.25f) - vec;
           }
            if (swordTypePrevious == SwordType.ATTACK)
            {

                interpolationVec = vec - AttackPos(0.75f);
            
            }
           
            
            step++;
       }
            
       if (timar >= interpolationTime)
       {
           if (swordTypePrevious == SwordType.MOVE)
           {
               
               ChangeType(SwordType.ATTACK);

           }
           if (swordTypePrevious == SwordType.ATTACK)
           {
               interpolationVec = new Vector2(swordUp, swordRight);
               ChangeType(SwordType.MOVE);
           }
          
       }
           
        timar++;
        //if (interpolationTime == timar)
        //    return;

        Vector2 NestPos = (interpolationVec / interpolationTime) * timar;

        if (swordTypePrevious == SwordType.MOVE)
        {

            NestPos += vec;
            this.transform.position = CameraPos + CameraFront / 2 + CameraUp * NestPos.y + CameraRight * NestPos.x;


            Quaternion CameraQ = Quaternion.AngleAxis(rad, CameraFront);

            Quaternion hoge = Quaternion.AngleAxis(0, CameraRight);

            Quaternion hogehoge = CameraQ * hoge;

            this.transform.rotation = Quaternion.Lerp(hogehoge, Camera.main.transform.rotation, 1.0f - (timar / interpolationTime));

        }
        if (swordTypePrevious == SwordType.ATTACK)
        {
            NestPos += AttackPos(0.75f);
            this.transform.position = CameraPos + CameraFront / 2 + CameraUp * NestPos.y + CameraRight * NestPos.x;


            Quaternion CameraQ = Quaternion.AngleAxis(rad, CameraFront);

            Quaternion hoge = Quaternion.AngleAxis(179, CameraRight);

            Quaternion hogehoge = CameraQ * hoge;

            this.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, hogehoge, 1.0f - (timar / interpolationTime));
        }
        


        

        
    }

   //private void MoveToAttack()
   //{
   //    Vector2 vec = new Vector2(swordRight, swordUp);
   //    if(step == 0)
   //}
   //
   //private void AttackToMove()
   //{
   //    Vector2 vec = new Vector2(swordRight, swordUp);
   //    if(step == 0)
   //}
   //
    private void SwordStop()
    {
        
        stopTimer -= Time.deltaTime;

        if (stopTimer < 0)
        {
            hitStop = false;
            Collision.enabled = true;
            CollisionFlick.enabled = true;
            swordType = swordTypeStop;
            return;
        }
    }

    private void SwordAlpha()
    {
        if(step == 0)
        {
            alpha = 1.0f;

            step++;
            
        }

        alpha -= Time.deltaTime * alphaSpeed ;// Time.deltaTime* alphaSpeed;

        if(alpha < 0)
        {
            alpha = 0;
            ChangeType(SwordType.MOVE);
        }
        Vector2 AP = AttackPos(0.75f);
        //this.transform.position = CameraPos + CameraFront / 2 + CameraUp * AP.y + CameraRight * AP.x;
        this.transform.position = CameraPos + CameraFront * swordFront + CameraUp * AP.y * swordUp + CameraRight * AP.x * swordRight;

        Quaternion CameraQ = Quaternion.AngleAxis(rad, CameraFront);

        Quaternion hoge = Quaternion.AngleAxis(180, CameraRight);


        //(CameraQ, hoge);
        this.transform.rotation = CameraQ * hoge;


        //swordColor.a = alpha;


    }

    private void FlickSword(ref Vector3 pos,ref Quaternion q,ref Vector3 scale)
    {
        Vector2 size = MarkerCanvasRectTransform.sizeDelta;

        Collision.enabled = true;
        CollisionFlick.enabled = true;
        FlickEndPos = flickManager.FlickEndPos;
        FlickStartPos = flickManager.FlickStartPos;

        FlickDirection = FlickEndPos - FlickStartPos;
        if (FlickDirection == Vector2.zero)
            return;

        float leng = Mathf.Sqrt(Mathf.Pow(FlickDirection.x, 2) + Mathf.Pow(FlickDirection.y, 2));

        rad = Mathf.Atan2(FlickDirection.y, FlickDirection.x);

        FlickDirection = (FlickEndPos + FlickStartPos) / 2;
        rad += 3.141592f / 2;
        rad *= Mathf.Rad2Deg;


        //CollisionCube = (GameObject)Instantiate(CollisionSword, FlickEndPos, CameraQ);//GameObject.Find("CollisionSword");//GameObject.CreatePrimitive(PrimitiveType.Cube);


        //  当たり判定！！



        scale = Collision2D.transform.localScale;


        scale.x = 0.40f;
        //scale.x = 600 / Screen.width;
        scale.y = leng * 0.01f;
        



        q = Quaternion.AngleAxis(rad, CameraFront);
        //Collision2D.transform.position = FlickStartPos;



        Vector2 PosOnScreen = ScreenPos(FlickStartPos);


        //AdjustScreenを使って移動させる

        pos.x = (size.x / 2) * PosOnScreen.x;
        pos.y = (size.y / 2) * PosOnScreen.y;
        pos.z = .0f;

    }

    private void ChangeType(SwordType type)
    {
        swordTypePrevious = swordType;

        swordType = type;
        
        
        step = 0;
        timar = 0;
        
    }
    private void ChangeType(SwordType type,bool reset)
    {

        swordTypeStop = swordType;
        swordType = type;
        if (reset)
        {
            step = 0;
            timar = 0;
            swordTypePrevious = swordType;
        }
    }


    private Vector2 AttackPos(float interpolationTime)
    {
        Vector2 size = MarkerCanvasRectTransform.sizeDelta;
        Vector2 sp,ep;

        if (swordcollision.fastPos == Vector3.zero)
            sp = ScreenPos(FlickStartPos);
        else
        {
            sp = swordcollision.fastPos;
            sp.x += 0.25f;
            sp.y -= 0.25f;

        }
        if (swordcollision.secondPos == Vector3.zero)
            ep = ScreenPos(FlickEndPos);
        else
        {
            ep = swordcollision.secondPos;
            ep.x += 0.25f;
            ep.y -= 0.25f;
        }
        Vector2 FPos = ep * interpolationTime + sp * (1.0f - interpolationTime);
        //Vector2 FPos = FlickEndPos * interpolationTime + FlickStartPos * (1.0f - interpolationTime);
        //Vector2 WH;
        //WH.x = Camera.main.pixelWidth  / 2;
        //WH.y = Camera.main.pixelHeight / 2;

        //FPos.x = FPos.x * 0.5f + 0.5f;
        //FPos.y = FPos.y * 0.5f + 0.5f;


    
        //FPos.x /=size.x * 4;
        //FPos.y /=size.y * 4;

        //FPos.x = FPos.x - Camera.main.pixelWidth / (size.x * 2);
        //FPos.y = FPos.y - Camera.main.pixelHeight/ (size.y * 2);
        //
        //FPos.x *= 2;
        //FPos.y *= 2;
        
        //FPos -= WH;
        //
        //
        //FPos.x = (FPos.x / WH.x) * 0.5f;
        //if (swordcollision.secondPos == Vector3.zero)
        //    FPos.y = (FPos.y / WH.y) * 0.3f;
        //else
        //    FPos.y = (FPos.y / WH.y) * 0.1f;
        //

        return FPos;

    }


    public Vector2 ScreenPos(Vector2 pos)
    {
        Vector2 PosOnScreen = pos;

        PosOnScreen.x /= Camera.main.pixelWidth;
        PosOnScreen.y /= Camera.main.pixelHeight;

        PosOnScreen.x = (PosOnScreen.x - 0.5f) * 2;
        PosOnScreen.y = (PosOnScreen.y - 0.5f) * 2;

        return PosOnScreen;
    }


    public bool GetAttackCheck
    {
        get { return swordType == SwordType.ATTACK ? true : false; }
    }

    public bool GetInterpolationCheck
    {
        get { return swordType == SwordType.INTERPOLATION ? true : false; }
    }

    public void HitStop()
    {
        hitStop = true;
        stopTimer = rhythmManager.OnTempoTime / 2;
        ChangeType(SwordType.STOP, false);

    }

    public GameObject GetSwordCollisionCube
    {
        get { return CollisionCube; }
    }

    public bool HitStopFlag
    {
        get { return hitStop; }
    }

    public Vector2 SwordDirection
    {
        get 
        {
            Vector2 vec = FlickEndPos - FlickStartPos;
            vec.Normalize();
            return vec;
        }
    }

    public Vector3 SwordDirection3D
    {
        get
        {
            Vector2 vec = FlickEndPos - FlickStartPos;
            vec.Normalize();
            Vector3 vec3 = new Vector3(vec.x, vec.y, 0);

            return vec3;
        }
    }


    public Vector3 StartFlickPos
    {
        get
        {
            Vector3 p = new Vector3(FlickStartPos.x, FlickStartPos.y, 0);
            return p;
        }
    }

    public Vector3 StartPos
    {
        get
        {
            if (!posFlag)
            {
                Vector3 pos = Vector3.zero;
                Vector3 scale = Vector3.zero;
                Quaternion q = Quaternion.identity;

                FlickSword(ref pos, ref q, ref scale);


                Vector2 AP = AttackPos(0.25f);
                startPos = CameraPos + CameraFront * swordFront + CameraUp * AP.y * swordUp + CameraRight * AP.x * swordRight;
                posFlag = true;
            }
            return startPos;
        }
    }


}
