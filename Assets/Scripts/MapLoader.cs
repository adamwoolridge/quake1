using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapLoader : MonoBehaviour {

    public string MapFileName = "";
    public Material GreyboxMaterial;
    public bool IgnoreTriggers = true;
    public Transform Camera;
    
    public bool DebugDrawBSP = true;

    private BSPMap map;       
        
    // Use this for initialization
    void Start ()
    {
        LoadMap();

        BSPEntity spawnEnt = map.entities.FirstOrDefault( ent => ent.KeyValues[ "classname" ] == "info_player_start" );
        Camera.position = spawnEnt.GetVector3( "origin" );

        SpawnMonsters();        
    }

    private void LoadMap()
    {
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

                if ( IgnoreTriggers && map.textures[ (int)map.textureSurfaces[ face.textureInfoIndex ].textureIndex ].name.Contains( "trigger" ) )
                    continue;

                // Vertices
                Vector3[] vertices = new Vector3[ face.edgeCount ];

                int index = 0;

                for (int i = (int)face.edgeListIndex; i < (int)face.edgeListIndex + face.edgeCount; i++)
                {
                    if (map.edgeList[(int)face.edgeListIndex + index] < 0)
                        vertices[index] = map.vertices[map.edges[Mathf.Abs(map.edgeList[i])].startIndex];
                    else
                        vertices[index] = map.vertices[map.edges[map.edgeList[i]].endIndex];

                    index++;
                }

                // Triangles
                int[] triangles = new int[ ( face.edgeCount - 2 ) * 3 ];

                int step = 1;

                for (int i = 1; i < vertices.Length - 1; i++)
                {
                    triangles[step - 1] = 0;
                    triangles[step] = i;
                    triangles[step + 1] = i + 1;
                    step += 3;
                }

                // UVs
                BSPTextureSurface textureSurface = map.textureSurfaces[ face.textureInfoIndex ];
                Vector2[] uvs = new Vector2[ face.edgeCount ];

                for (int i = 0; i < face.edgeCount; i++)
                {
                    uvs[i].x = ((Vector3.Dot(vertices[i], textureSurface.u) + textureSurface.uOffset) / (float)map.textures[(int)map.textureSurfaces[face.textureInfoIndex].textureIndex].width);
                    uvs[i].y = ((Vector3.Dot(vertices[i], textureSurface.v) + textureSurface.vOffset) / (float)map.textures[(int)map.textureSurfaces[face.textureInfoIndex].textureIndex].height);
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

                rend.material.shader = Shader.Find("Unlit/Texture");
                rend.material.mainTexture = map.textures[(int)map.textureSurfaces[face.textureInfoIndex].textureIndex].texture;
                rend.material.mainTexture.filterMode = FilterMode.Point;
                faceObj.transform.parent = modelObj.transform;
            }

            curModelCount++;
        }
    }
        	
    private void SpawnMonsters()
    {
        List<BSPEntity> monsterEnts = map.entities.Where(ent=>ent.KeyValues["classname"].Contains("monster")).ToList();

        foreach (BSPEntity ent in monsterEnts)
        {
            GameObject monsterGO = new GameObject(ent.KeyValues["classname"]);

            Monster monster = monsterGO.AddComponent<Monster>();
            monster.Init(ent);            
        }
    }

    void OnDrawGizmos()
    {
        if (!DebugDrawBSP) return;

        if (map == null) return;

        Gizmos.color = Color.blue;

        foreach (BSPNode node in map.nodes)
        {
            Gizmos.DrawWireCube(node.boundingBox.min + ((node.boundingBox.max- node.boundingBox.min) / 2), node.boundingBox.max - node.boundingBox.min);
        }

        Gizmos.color = Color.grey;

        foreach (BSPLeaf leaf in map.leaves)
        {
            Gizmos.DrawWireCube(leaf.boundingBox.min + ((leaf.boundingBox.max - leaf.boundingBox.min) / 2), leaf.boundingBox.max - leaf.boundingBox.min);
        }
    }
}
