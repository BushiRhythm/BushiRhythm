using UnityEngine;
using System.Collections;

//現在地でのフルスクリーン用メッシュを生成します
public class FulScreenMesh : MonoBehaviour {

	public MeshFilter meshFilter;
	[SerializeField]
	MeshRenderer renderer;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetTexture(Texture texture)
	{
		renderer .material .SetTexture( "_MainTex" , texture );
	
	}

	public void CreateMesh()
	{

		Mesh mesh = new Mesh();
		Camera camera = Camera .main;

		//現在位置でのZ座標を取得
		float ViewPortZ = camera .WorldToViewportPoint( transform .position ) .z;


		Vector3[] vertices = new Vector3[4];

		Vector2[] ViewPorts =
		{
			new Vector2 (1,1),
			new Vector2 (0,1),
			new Vector2 (0,0),
			new Vector2 (1,0),

	};
		Vector3 ViewPort = new Vector3(0,0,ViewPortZ);
		Vector3 RequestPos = new Vector3( );
		Matrix4x4 mat = new Matrix4x4();
		for (int i = 0;i < 4;i++)
		{
			vertices[i] = new Vector3();
			ViewPort.x = ViewPorts[i].x;
			ViewPort.y= ViewPorts[i].y;
			RequestPos = camera .ViewportToWorldPoint( ViewPort );

			 vertices[i] = transform .InverseTransformPoint( RequestPos );
		}

		int[] triangles = new int[] {
			0, 2, 1,
			0, 3, 2
		};
		Vector2[] uv = new Vector2[] {
			new Vector2(1.0f, 1.0f),
			new Vector2(0.0f, 1.0f),
			new Vector2(0.0f, 0.0f),
			new Vector2(1.0f, 0.0f),
		};

		mesh .vertices = vertices;
		mesh .triangles = triangles;
		mesh .uv = uv;
		mesh .RecalculateNormals();
		mesh .RecalculateBounds();


		meshFilter .mesh = mesh;


	
	}
}
