using UnityEngine;
using System.Collections;

public class Face
{
    public Vector3[] Vertices;
    public string TextureName = "";

    public Face()
    {
        Vertices = new Vector3[3];
    }
}
