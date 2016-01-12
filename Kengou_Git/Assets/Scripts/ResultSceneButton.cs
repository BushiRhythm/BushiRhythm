using UnityEngine;
using System.Collections;

public class ResultSceneButton : MonoBehaviour
{
    public GameObject Dodon;
    public GameObject _Effect;
    public GameOverPushEffect Effect
    {
        get
        {
            return _Effect.GetComponent<GameOverPushEffect>();
        }
    }

    public GameObject _Screen;
    public ScreenFadeout FScreen
    {
        get
        {
            return _Screen.GetComponent<ScreenFadeout>();
        }
    }

    public GameObject _resultMain;
    public ResultManager resultMain
    {
        get
        {
            return _resultMain.GetComponent<ResultManager>();
        }
    }
    public GameObject _Button;

    public ResultDataShow _RDShow;
    public ResultDataShow RDShow
    {
        get
        {
            if (!_RDShow) _RDShow = this.GetComponent<ResultDataShow>();
                return _RDShow;
        }
    }
    public void destroy()
    {
        if (!RDShow.IsMoveComplete()) return;
        Instantiate(Dodon, transform.position, transform.rotation);
        Effect.EffectEnable();
        FScreen.FadeEnable();
        Destroy(this.gameObject);
        Destroy(this._Button);
    }
    public void ReTryPush()
    {
        if (RDShow.IsMoveComplete())
        resultMain.SelectRetry();
    }
    public void TitlePush()
    {
        if (RDShow.IsMoveComplete())
        resultMain.SelectEnd();
    } 
}
