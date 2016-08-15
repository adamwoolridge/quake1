using UnityEngine;
using System.Collections;

public class MapLoader : MonoBehaviour {

    public string MapFileName = "";
    public Material GreyboxMaterial;

    private BSPMap map;
                
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
            
            int[] triangles = new int[ ( face.edgeCount - 2 ) * 3 ];

            int step = 1;

            for ( int i = 1; i < vertices.Length - 1; i++ )
            {
                triangles[ step - 1 ] = 0;
                triangles[ step ] = i;
                triangles[ step + 1 ] = i + 1;
                step += 3;
            }

            BSPTextureSurface textureSurface = map.textureSurfaces[ face.textureInfoIndex ];
            Vector2[] uvs = new Vector2 [vertices.Length ];
            for ( int i = 0; i < vertices.Length; i ++ )
            {
                uvs[i].x = Vector2.Dot(vertices[i], textureSurface.u) + textureSurface.uOffset;
                uvs[i].y = Vector2.Dot(vertices[i], textureSurface.v) + textureSurface.vOffset;
            }

            GameObject faceObj = new GameObject();
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            faceObj.AddComponent<MeshFilter>().mesh = mesh;
            faceObj.AddComponent<MeshRenderer>().material.mainTexture = map.textures[ (int)map.textureSurfaces[ face.textureInfoIndex ].textureIndex ].texture;
            faceObj.transform.parent = transform;
        }        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
