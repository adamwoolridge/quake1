using UnityEngine;
using System.Collections;

public class MapLoader : MonoBehaviour {

    public string MapFileName = "";

    private BSPMap map;

    public Material GreyboxMaterial;

    // Use this for initialization
    void Start () {
        map = new BSPMap( MapFileName );
      
        foreach (BSPFace face in map.faces)
        {
            Vector3 [] vertices = new Vector3[ face.edgeCount ];

            int index = 0;

            for ( int i = (int)face.edgeListIndex; i < (int)face.edgeListIndex + face.edgeCount; i++ )
            {
                if ( map.edgeList[ (int)face.edgeListIndex + index ] < 0 )                
                    vertices[index] = map.vertices[ map.edges[ Mathf.Abs( map.edgeList[ i ] ) ].startIndex ];                
                else                
                    vertices[index] = map.vertices[ map.edges[ map.edgeList[ i ] ].endIndex ];

                index++;              
            }
            
            int[] tris = new int[(face.edgeCount - 2) * 3];
            int tristep = 1;
            for (int i = 1; i < vertices.Length - 1; i++)
            {
                tris[tristep - 1] = 0;
                tris[tristep] = i;
                tris[tristep + 1] = i + 1;
                tristep += 3;
            }

            GameObject faceObj = new GameObject();
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = tris;
            mesh.RecalculateNormals();
            faceObj.AddComponent<MeshFilter>().mesh = mesh;
            faceObj.AddComponent<MeshRenderer>().material = GreyboxMaterial;
        }        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
