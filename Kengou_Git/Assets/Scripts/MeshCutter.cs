using UnityEngine;
using System.Collections;
using System .Collections.Generic;

public class Vertex
{
	public Vector3 Pos;
	public Vector2 Uv;

	public Vertex()
	{
		Pos = new Vector3();
		Uv = new Vector3();
	}
	public float angle;

	public static int CompareByAngle( Vertex a , Vertex b )
	{
		if (a .angle > b .angle)
		{
			return 1;
		}
		else
		{
			return -1;
		}
	}
}

public class MeshCutter : MonoBehaviour {

	public MeshCutterAttacher meshCutterAttach;
	
	public MeshFilter meshFilter;

	public MeshRenderer renderer;

	public int MaxCut;

	MeshCutter[] meshcutters;

	bool Cuted;

	int CreateIndex;

	// Use this for initialization
	void Start () {
		meshcutters = new MeshCutter[2];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Cut( Vector3 p0 , Vector3 p1 , Vector3 forward ,Vector3 CutedForward)
	{
		if (MaxCut <= 0)
			return;
		if (Cuted)
		{
			for (int i = 0; i < 2; i++)
			{
				meshcutters[i] .Cut( p0 , p1 , forward , CutedForward );
			}
			return;
		}
		p0 = transform .InverseTransformPoint( p0 );
		p1 = transform .InverseTransformPoint( p1 );
		forward = transform .InverseTransformVector( forward );

		Vector3 up = p1 - p0;
		Vector3 right = Vector3 .Cross( up , forward );
		forward .Normalize();
		up .Normalize();
		right .Normalize();

		Mesh mesh = meshFilter .mesh;


		int CrossCnt = 0;

		float cp = Vector3.Dot(p0,right);
		
		Vector3[] CrossPoint = new Vector3[2];
		Vector2[] CrossUV = new Vector2[2];



		for (int i = 0;i < mesh.vertexCount;i++)
		{		
			float Min = float.MaxValue;
			float Max = -float.MaxValue;
			float work;
			int MinIndex = 0;
			int MaxIndex = 0;
			for (int j = 0; j < 2; j++)
			{
				 work =Vector3.Dot(mesh.vertices[(i  + j)% mesh.vertexCount],right);
				if(work < Min)
				{
					Min = work;
					MinIndex = ( i + j ) % mesh .vertexCount;
				}
				if (work > Max)
				{
					Max = work;
					MaxIndex = ( i + j ) % mesh .vertexCount;
				}
			}

			if (!( Min < cp && cp < Max ))
				continue;

			float Length = (cp - Min)  / (Max - Min); //0～1


			Vector3 MinVec = mesh .vertices[MinIndex];
			Vector3 MaxVec = mesh .vertices[MaxIndex];

			Vector2 MinUV = mesh .uv[MinIndex];
			Vector2 MaxUV = mesh .uv[MaxIndex];

			CrossPoint[CrossCnt] = MinVec + ( MaxVec - MinVec ) * Length;
			CrossUV[CrossCnt] = MinUV + ( MaxUV - MinUV ) * Length;
			CrossCnt++;
		}

		if (CrossCnt != 2) //貫通していないので処理を止める
			return;

		Vector3 Cp0,Cp1;
		Vector2 uv0 , uv1;

		float work0 = Vector3.Dot(up,CrossPoint[0] );
		float work1 = Vector3.Dot(up,CrossPoint[1] );

		if(work0 < work1)
		{
			Cp0 = CrossPoint[0];
			Cp1 = CrossPoint[1];
			uv0 = CrossUV[0];
			uv1 = CrossUV[1];
		}
		else
		{
			Cp0 = CrossPoint[1];
			Cp1 = CrossPoint[0];
			uv0 = CrossUV[1];
			uv1 = CrossUV[0];
		}

		//左方の頂点と右方の頂点を整理
		List<int> LeftVerticesID = new List<int>();
		List<int> RightVerticesID = new List<int>();

		for (int i = 0;i < mesh .vertexCount;i++)
		{
			if(Vector3.Dot(mesh.vertices[i],right) < cp)
			{
				LeftVerticesID .Add( i );
			}
			else
			{
				RightVerticesID .Add( i );
			}
		}
		//左方の頂点と右方の頂点クラスを作成
		List<Vertex> LeftVertices = new List<Vertex>();
		List<Vertex> RightVertices = new List<Vertex>();

		for(int i = 0 ; i<LeftVerticesID.Count; i++)
		{
			Vertex vtx = new Vertex();
			vtx .Pos = mesh .vertices[LeftVerticesID[i]];
			vtx .Uv = mesh .uv[LeftVerticesID[i]]; 
			LeftVertices.Add(vtx);
		}

		foreach (int idx  in RightVerticesID)
		{
			Vertex vtx = new Vertex();
			vtx.Pos = mesh .vertices[idx];
			vtx.Uv = mesh .uv[idx]; 
			RightVertices.Add(vtx);
		}

		for (int i = 0; i < 2; i++)
		{
			Vertex vtx = new Vertex();
			vtx.Pos = CrossPoint[i];
			vtx.Uv = CrossUV[i]; 
			RightVertices.Add(vtx);
			LeftVertices.Add(vtx);
		}
		
		//中心点を設定
		Vector3 LeftCenter =  new Vector3();
		Vector3 RightCenter =  new Vector3();

		foreach( Vertex vtx  in RightVertices)
		{
			RightCenter += vtx.Pos;
		}

		foreach( Vertex vtx  in LeftVertices)
		{
			LeftCenter += vtx.Pos;
		}
	

		RightCenter /=RightVertices.Count;
		LeftCenter /=LeftVertices.Count;

		Vector3 normal = new Vector3();
		normal = Vector3 .Cross( mesh .vertices[meshFilter .mesh .triangles[0]] - mesh .vertices[meshFilter .mesh .triangles[1]] , mesh .vertices[meshFilter .mesh .triangles[0]] - mesh .vertices[meshFilter .mesh .triangles[2]] );
		normal .Normalize();
		Vector3 vUp = Vector3 .up;
		Vector3 vRight = Vector3 .Cross( normal , vUp );
		vUp = Vector3 .Cross( vRight , normal );
		vUp.Normalize();
		vRight .Normalize();

		int[] RightSortIDs = new int[RightVertices.Count];

		SortID( RightVertices , RightSortIDs , vUp , vRight );
		ListSort( ref RightVertices , RightSortIDs);

		int[] LeftSortIDs = new int[LeftVertices .Count];

		SortID( LeftVertices , LeftSortIDs , vUp , vRight );
		ListSort( ref LeftVertices , LeftSortIDs );



		////角度を計算



		
		//foreach( Vertex vtx  in RightVertices)
		//{
		//	Vector3 len =vtx.Pos - RightCenter;
		//	len .Normalize();

		//	float dot = Vector3 .Dot( up , len );
		//	if( Vector3 .Dot( Vector3 .Cross(up , len) , forward ) < .0f)
		//	{
		//		dot = 2 - dot;
		//	}
		//	vtx .angle = dot;
		//}



		////角度を計算
		//foreach( Vertex vtx  in LeftVertices)
		//{
		//	Vector3 len =vtx.Pos - LeftCenter;
		//	len .Normalize();

		//	float dot = Vector3 .Dot( up , len );
		//	if (Vector3 .Dot( Vector3 .Cross( up , len ) , forward ) < .0f)
		//	{
		//		dot = 2 - dot;
		//	}
		//	vtx .angle = dot;
		//}
		//LeftVertices.Sort(Vertex.CompareByAngle);

		//RightVertices.Sort(Vertex.CompareByAngle);



		int[] LeftIndex = new int[(LeftVertices.Count -2) * 3];

		for (int i = 0;i < LeftVertices.Count-2;i++)
		{
			LeftIndex[i * 3] = 0;
			LeftIndex[i * 3 + 1] = i +2;
			LeftIndex[i * 3 + 2] = i +1;
		}
		int[] RightIndex = new int[(RightVertices.Count -2) * 3];

		for (int i = 0;i < RightVertices .Count - 2;i++)
		{
			RightIndex[i * 3] = 0;
			RightIndex[i * 3 + 1] = i + 2;
			RightIndex[i * 3 + 2] = i + 1;
		}

		Mesh LeftMesh = new Mesh();
		Mesh RightMesh = new Mesh();

		MeshCreate( LeftMesh , LeftVertices , LeftIndex );
		MeshCreate( RightMesh , RightVertices , RightIndex );

		Vector3 CutRight = Vector3 .Cross( up , CutedForward );
		CutRight .Normalize();

		CreateObject( LeftMesh , -CutRight );
		CreateObject( RightMesh , CutRight );

		Cuted = true;
		renderer .enabled = false;
	}

	public void CreateObject(Mesh mesh, Vector3 CutDir)
	{
		MeshCutterAttacher Empty = ( Instantiate( meshCutterAttach.gameObject , transform .position , transform .rotation ) as GameObject ) .GetComponent < MeshCutterAttacher>();
		Empty.transform .parent = transform;
		Empty .transform .localScale = new Vector3( 1 , 1 , 1 );
		Empty.transform .parent = null;
		MeshCutter EmptyMeshCutter = Empty .gameObject.AddComponent<MeshCutter>();
		EmptyMeshCutter .renderer = Empty .gameObject .AddComponent<MeshRenderer>();
		EmptyMeshCutter .meshFilter = Empty .gameObject .AddComponent<MeshFilter>();

		EmptyMeshCutter .renderer .material = renderer .sharedMaterial;
		EmptyMeshCutter .meshFilter .mesh = mesh;

		EmptyMeshCutter .MaxCut = MaxCut - 1;

		EmptyMeshCutter .meshCutterAttach = meshCutterAttach;
		Empty .CutDir = CutDir;

		meshcutters[CreateIndex] = EmptyMeshCutter;
		CreateIndex++;

	
	}
	void MeshCreate(Mesh Copymesh , List<Vertex> CopyVertices,int[] Index)
	{
		Vector2[] UVs = new Vector2[CopyVertices .Count];
		Vector3[] Vertices = new Vector3[CopyVertices .Count];

		for (int i = 0; i < CopyVertices.Count; i++)
		{
			UVs[i] = CopyVertices[i] .Uv;
			Vertices[i] = CopyVertices[i] .Pos;
		}
		Copymesh .vertices = Vertices;
		Copymesh .uv = UVs;
		Copymesh .triangles = Index;

	}

	void SortID(List<Vertex> Vertices ,int[] SortID, Vector3 Up,Vector3 Right)
	{
		bool[] Switch = new bool[Vertices .Count];
		//まず右端のIDを探す
		int cnt = 0;
		int CurID = 0;
		Vector3 CurAxis = Up;

		float Max = -float .MaxValue;
		for (int i = 0;i < Vertices.Count;i++)
		{
			float dot = Vector3 .Dot( Right , Vertices[i] .Pos );
			if(dot > Max)
			{
				Max = dot;
				CurID = i;
			}
		}
		Vector3 MaxAxis = new Vector3();
		Vector3 WorkAxis = new Vector3();
		int MaxId = 0;
		for (cnt = 0;cnt < Vertices .Count;cnt++)
		{
			//角度の浅い点を探す
			Max = -float .MaxValue;
			for (int i = 0;i < Vertices .Count;i++)
			{
				if (Switch[i])
					continue;
				if (CurID == i)
					continue;

				WorkAxis = ( Vertices[i] .Pos - Vertices[CurID] .Pos );
				WorkAxis .Normalize();
				float dot = Vector3 .Dot( CurAxis , WorkAxis );
				if (dot > Max)
				{
					Max = dot;
					MaxAxis = WorkAxis;
					MaxId = i;
				}

			}

			Switch[MaxId] = true;
			CurID = MaxId;
			SortID[cnt] = CurID;
			CurAxis = MaxAxis;

		}

	}

	void ListSort(ref List<Vertex> Vertices , int[] SortID)
	{
		List<Vertex> NewVertices = new List<Vertex>();

		for (int i = 0;i < Vertices.Count;i++)
		{
			NewVertices .Add( Vertices[SortID[i]] );
		}

		Vertices = NewVertices;
	}
}
