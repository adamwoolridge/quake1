using UnityEngine;
using System.Collections;

public class MapLoader : MonoBehaviour {

    public string MapFileName = "";
    public Material GreyboxMaterial;
    public bool IgnoreTriggers = true;

    private BSPMap map;

    MDL soldierMdl;
    private int frameIndex = 0;

    // Use this for initialization
    void Start ()
    {
        soldierMdl = new MDL("soldier.mdl");

        map = new BSPMap( MapFileName );

        int curModelCount = 0;

        foreach ( BSPModel model in map.models )
        {
            GameObject modelObj = new GameObject( "model_" + curModelCount );
            modelObj.transform.parent = transform;

            int findex = 0;

            for ( int f = 0; f < model.faceCount; f++ )
            {
                findex = (int)model.faceIndex + f;

                BSPFace face = map.faces[ findex ];

                if (IgnoreTriggers && map.textures[(int)map.textureSurfaces[face.textureInfoIndex].textureIndex].name.Contains("trigger"))
                    continue;

                // Vertices
                Vector3[] vertices = new Vector3[ face.edgeCount ];

                int index = 0;

                for ( int i = (int)face.edgeListIndex; i < (int)face.edgeListIndex + face.edgeCount; i++ )
                {
                    if ( map.edgeList[ (int)face.edgeListIndex + index ] < 0 )
                        vertices[ index ] = map.vertices[ map.edges[ Mathf.Abs( map.edgeList[ i ] ) ].startIndex ];
                    else
                        vertices[ index ] = map.vertices[ map.edges[ map.edgeList[ i ] ].endIndex ];

                    index++;
                }

                // Triangles
                int[] triangles = new int[ ( face.edgeCount - 2 ) * 3 ];

                int step = 1;

                for ( int i = 1; i < vertices.Length - 1; i++ )
                {
                    triangles[ step - 1 ] = 0;
                    triangles[ step ] = i;
                    triangles[ step + 1 ] = i + 1;
                    step += 3;
                }

                // UVs
                BSPTextureSurface textureSurface = map.textureSurfaces[ face.textureInfoIndex ];
                Vector2[] uvs = new Vector2[ face.edgeCount ];

                for ( int i = 0; i < face.edgeCount; i++ )
                {
                    uvs[ i ].x = ( ( Vector3.Dot( vertices[ i ], textureSurface.u ) + textureSurface.uOffset ) / (float)map.textures[ (int)map.textureSurfaces[ face.textureInfoIndex ].textureIndex ].width );
                    uvs[ i ].y = ( ( Vector3.Dot( vertices[ i ], textureSurface.v ) + textureSurface.vOffset ) / (float)map.textures[ (int)map.textureSurfaces[ face.textureInfoIndex ].textureIndex ].height );
                }

                // Create the mesh for the face
                GameObject faceObj = new GameObject();
                Mesh mesh = new Mesh();
                mesh.vertices = vertices;
                mesh.uv = uvs;
                mesh.triangles = triangles;
                mesh.RecalculateNormals();
                faceObj.AddComponent<MeshFilter>().mesh = mesh;
                MeshRenderer rend = faceObj.AddComponent<MeshRenderer>();

                rend.material.shader = Shader.Find("Legacy Shaders/Diffuse");
                rend.material.mainTexture = map.textures[ (int)map.textureSurfaces[ face.textureInfoIndex ].textureIndex ].texture;
                rend.material.mainTexture.filterMode = FilterMode.Point;
                faceObj.transform.parent = modelObj.transform;
            }

            curModelCount++;
        }      
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            frameIndex++;
            soldierMdl.SetFrame(frameIndex);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            frameIndex--;
            soldierMdl.SetFrame(frameIndex);
        }
    }
}
