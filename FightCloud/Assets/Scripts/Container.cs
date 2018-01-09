using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Container : MonoBehaviour {
    public float safeZone;
    public float resolution;
    public float threshold;
    public ComputeShader computeShader;
    public bool calculateNormals;

    private CubeGrid grid;
    
    public int numBalls = 100;
    public float ballSpeed = 0.1f;
    public float ballRadius = 0.075f;

    public MetaBall ballPrefab;
    private MetaBall[] ballArray;

    public void Start() {
        List<MetaBall> ballList = new List<MetaBall>();

        for (int i = 0; i < numBalls; i++)
        {
            MetaBall b = GameObject.Instantiate(ballPrefab);
            b.transform.parent = this.gameObject.transform;
            b.transform.localPosition = Vector3.zero;
            ballList.Add(b);
        }

        ballArray = ballList.ToArray();

        this.grid = new CubeGrid(this, this.computeShader);
    }

    public void Update() {
        if (ballArray != null)
        {
            foreach(BouncingBall b in ballArray)
            {
                b.radius = ballRadius;
                b.speed = ballSpeed;
            }
            this.grid.evaluateAll(ballArray);
        }

        Mesh mesh = this.GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = this.grid.vertices.ToArray();
        mesh.triangles = this.grid.getTriangles();

        if(this.calculateNormals) {
            mesh.RecalculateNormals(60);
        }
    }
}