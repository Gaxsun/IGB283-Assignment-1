using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour {

    public Material material;

    public float angle = 1;

    public int speed = 10;

    Mesh mesh;

    public int squareHeight = 1;
    public int squareWidth = 1;

    Vector3[] squareVertecies = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0), new Vector3(1, 0, 0) };

    // Use this for initialization
    void Start() {
        // Add a MeshFilter and MeshRenderer to the Empty GameObject
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        // Get the Mesh from the MeshFilter
        mesh = GetComponent<MeshFilter>().mesh;

        // Set the material to the material we have selected
        GetComponent<MeshRenderer>().material = material;

        // Clear all vertex and index data from the mesh
        mesh.Clear();

        // Create a triangle with points at (0, 0, 0), (0, 1, 0) and(1, 1, 0)
        mesh.vertices = squareVertecies;

        // Set the colour of the triangle
        mesh.colors = new Color[] { new Color(0.8f, 0.3f, 0.3f, 1.0f), new Color(0.8f, 0.3f, 0.3f, 1.0f), new Color(0.8f, 0.3f, 0.3f, 1.0f), new Color(0.8f, 0.3f, 0.3f, 1.0f) };

        // Set vertex indicies
        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };
    }

    void Update() {
        // Get the vertices from the matrix
        Vector3[] vertices = mesh.vertices;

        // Get the rotation matrix
        Matrix3x3 R = rotate(angle * Time.deltaTime);

        //Get movement matrix
        Matrix3x3 M = move(0.0f);
        

        for (int i = 0; i < vertices.Length; i++) {
            vertices[i] = R.MultiplyPoint(vertices[i]);
        }
        
        for (int i = 0; i < vertices.Length; i++) {
            vertices[i] = M.MultiplyPoint(vertices[i]);
        }
        
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }

    Matrix3x3 rotate(float angle) {

        Matrix3x3 matrix = new Matrix3x3();

        // Set the rows of the matrix
        matrix.SetRow(0, new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0.0f));
        matrix.SetRow(1, new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f));
        matrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        return matrix;
    }

    Matrix3x3 move(float increment) {

        Matrix3x3 matrix = new Matrix3x3();

        matrix.SetRow(0, new Vector3(mesh.vertices[0].x+increment, 1.0f, 0.0f));
        matrix.SetRow(1, new Vector3(mesh.vertices[1].x+increment, 1.0f, 0.0f));
        matrix.SetRow(2, new Vector3(1.0f, 1.0f, 1.0f));
        
        print(mesh.vertices[0]);
        print(mesh.vertices[1]);
        print(mesh.vertices[2]);
        return matrix;
    }
}
