using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
           
    }

    private void OnEnable()
    {

    }

    float lastTime = 0;
    float interval = 0.1f;
    // Update is called once per frame
    void Update()
    {
        if (Time.time-lastTime > interval)
        {
            lastTime = Time.time;
        }

        GCTestNew();
        GCTest1();
    }



    private void GCTestNew()
    {
        var touches = Input.touches;
        var vertices = GetComponent<MeshFilter>().mesh.vertices;
        var normals = GetComponent<MeshFilter>().mesh.normals;
        var tangents = GetComponent<MeshFilter>().mesh.tangents;
        var uv1 = GetComponent<MeshFilter>().mesh.uv;
        var uv2 = GetComponent<MeshFilter>().mesh.uv2;
        var uv3 = GetComponent<MeshFilter>().mesh.uv3;
        var uv4 = GetComponent<MeshFilter>().mesh.uv4;
        var uv5 = GetComponent<MeshFilter>().mesh.uv5;
        var uv6 = GetComponent<MeshFilter>().mesh.uv6;
        var uv7 = GetComponent<MeshFilter>().mesh.uv7;
        var uv8 = GetComponent<MeshFilter>().mesh.uv8;
        var colors32 = GetComponent<MeshFilter>().mesh.colors32;
        var boneWeights = GetComponent<MeshFilter>().mesh.boneWeights;
        var colors = GetComponent<MeshFilter>().mesh.colors;
        var triangles = GetComponent<MeshFilter>().mesh.triangles;
    }
    int[] arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
    int[] arr2 = new int[1000];
    private void GCTest1()
    {
        foreach (var item in arr)
        {

        }
    }


    
}
