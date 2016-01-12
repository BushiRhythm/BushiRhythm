using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollImage : MonoBehaviour {
	[SerializeField]
	Image mat;

	[SerializeField]
	Renderer renderer;

	[SerializeField]
	Vector2 OffSetMove;

	Vector2 OffSetPos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		OffSetPos +=OffSetMove * Time.unscaledDeltaTime;
		if(mat)
			mat .material .SetTextureOffset("_MainTex", OffSetPos );
		if (renderer)
			renderer .material .SetTextureOffset( "_MainTex" , OffSetPos );
	}
}
