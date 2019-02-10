using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour {
    
    [SerializeField]
    MeshFilter mf;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R)){
            MangleMesh(mf);
        }
	}

    void MangleMesh(MeshFilter mf){
        Mesh mesh = new Mesh();
        mesh.vertices = GenerateVertices();
        mesh.triangles = GenerateTriangles();
        mesh.normals = GenerateNormals();
        mf.mesh = mesh;
    }

    Vector3[] GenerateVertices(){
        return new Vector3[]{
            new Vector3(-1, 0, 0),
            new Vector3(1, 2, 0),
            new Vector3(1, 0, 0)
        };
    }

    int[] GenerateTriangles(){
        return new int[]{
            0, 1, 2
        };
    }

    Vector3[] GenerateNormals(){
        return new Vector3[]{
            Vector3.forward,
            Vector3.forward,
            Vector3.forward,
        };
    }

}
