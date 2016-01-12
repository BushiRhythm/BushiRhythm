using UnityEngine;
using System.Collections;

public class OptionLoader : MonoBehaviour {

    [SerializeField]
    GameObject OptionManagerObject;
    [SerializeField]
    GameObject FadeObject;
    OptionManager _optionManager;
    OptionManager optionManager {
        get {
            if (!_optionManager) _optionManager = FindObjectOfType<OptionManager>();
            return _optionManager;
        }
   }

	// Use this for initialization

    bool Flag = false;

    void Start()
    {
        if (Flag) return;
        if (!optionManager)
        {
            Instantiate(OptionManagerObject, transform.position, transform.rotation);
        }
        else
        {
            GameObject Fade = Instantiate(FadeObject, transform.position, transform.rotation) as GameObject;
            Fade.transform.parent = this.transform;
        }
        Flag = true;
    }
}
