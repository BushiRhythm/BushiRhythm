using UnityEngine;
using System.Collections;

public class StageSelectStarter : MonoBehaviour {

	[SerializeField]
	Behaviour[] components;

	public Canvas NextCanvas;
	
	public Camera NextCamera;

	public void AllEnable()
	{
		AllChange( true );
	}
	public void AllDisable()
	{
		AllChange( false );
	}

	void AllChange(bool value)
	{
		foreach (Behaviour com in components)
			com .enabled = value;
	}
	// Use this for initialization
	void Start () {
		if(FindObjectOfType<TitleSystem>() != null)
		{
			AllDisable();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
