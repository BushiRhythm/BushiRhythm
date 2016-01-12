using UnityEngine;
using System.Collections;

public class DeathSceneButton : MonoBehaviour {
    public GameObject Dodon;
    public GameObject _Effect;
	public GameOverPushEffect Effect{
		get{
			return _Effect.GetComponent<GameOverPushEffect>();
		}
	}
	public GameObject _Screen;
	public ScreenFadeout FScreen{
		get{
			return _Screen.GetComponent<ScreenFadeout>();
		}
	}
	public GameObject _gameOverMain;
	public GameOverMain gameOverMain{
		get{
			return _gameOverMain.GetComponent<GameOverMain>();
		}
	}
	public GameObject _Button;
	public void destroy(){
        Instantiate(Dodon,transform.position,transform.rotation);
		Effect.EffectEnable ();
		FScreen.FadeEnable ();
		Destroy(this.gameObject);
		Destroy(this._Button);
	} 
	public void ReTryPush(){
		gameOverMain.SelectRetry();
	} 
	public void TitlePush(){
		gameOverMain.SelectEnd();
	} 
}
