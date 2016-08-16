using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;


public class MDL
{
    public MDLHeader header;    
    public List<MDLSkin> skins;
    public List<MDLTexCoord> texCoords;
    public List<MDLTriangle> triangles;
    public List<MDLFrame> frames;

    private BinaryReader mdlFile;

    public MDL( string fileName )
    {                        
        mdlFile = new BinaryReader( File.Open( "Assets/Resources/Models/" + fileName, FileMode.Open ) );

        header = new MDLHeader( mdlFile );

        LoadSkins( mdlFile );
        LoadTextureCoords( mdlFile );
        LoadTriangles( mdlFile );
        LoadFrames( mdlFile );

        DrawFrame(0);
    }

    private void LoadSkins( BinaryReader mdlFile )
    {
        skins = new List<MDLSkin>();

        for ( int i = 0; i < header.skinCount; i++ )        
            skins.Add( new MDLSkin( mdlFile, header ) );        
    }

    private void LoadTextureCoords( BinaryReader mdlFile )
    {
        texCoords = new List<MDLTexCoord>();

        for ( int i = 0; i < header.vertCount; i++ )        
            texCoords.Add( new MDLTexCoord( mdlFile ) );        
    }

    private void LoadTriangles( BinaryReader mdlFile )
    {
        triangles = new List<MDLTriangle>();

        for ( int i = 0; i < header.triCount; i++ )
            triangles.Add( new MDLTriangle( mdlFile) );
    }

    private void LoadFrames( BinaryReader mdlFile )
    {
        frames = new List<MDLFrame>();

        for (int i = 0; i < header.frameCount; i++)
            frames.Add( new MDLFrame( mdlFile, header ) );
    }

    // Just getting it working, will clean up when it works
    public void DrawFrame( int frame )
    {
        Vector3 [] allVerts = new Vector3[ header.vertCount ];
        int[] tris = new int[ header.triCount * 3];
        Vector2[] uvs = new Vector2[ header.vertCount ];

        for ( int i = 0; i < header.triCount; i++ )
        {
            Vector3[] vertices = new Vector3[ 3 ];

            for ( int v = 0; v < 3; v ++ )
            {
                MDLVert thisVert = frames[ frame ].verts[ triangles[ i ].vertexIndexes[ v ] ];
                int index = triangles[ i ].vertexIndexes[ v ];

                allVerts[ index ] = new Vector3();
                allVerts[ index ].x = ( header.scale.x * thisVert.v[ 0 ] ) + header.translate[ 0 ];
                allVerts[ index ].y = ( header.scale.y * thisVert.v[ 2 ] ) + header.translate[ 2 ];
                allVerts[ index ].z = ( header.scale.z * thisVert.v[ 1 ] ) + header.translate[ 1 ];

                float s = texCoords[triangles[i].vertexIndexes[v]].s;
                float t = texCoords[triangles[i].vertexIndexes[v]].t;

                if (triangles[i].facesFront != 1 && texCoords[triangles[i].vertexIndexes[v]].onSeam != 0)
                {
                    s += (float)header.skinWidth * 0.5f;
                }

                s = (s + 0.5f) / header.skinWidth;
                t = (t + 0.5f) / header.skinHeight;

                uvs[index] = new Vector2(s, t);
            }

            tris[ i * 3 ] = triangles[ i ].vertexIndexes[ 0 ];
            tris[ i * 3 + 1 ] = triangles[ i ].vertexIndexes[ 1 ];
            tris[ i * 3 + 2 ] = triangles[ i ].vertexIndexes[ 2 ];            
        }

        GameObject faceObj = new GameObject();
        Mesh mesh = new Mesh();
        mesh.vertices = allVerts;        
        mesh.triangles = tris;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        faceObj.AddComponent<MeshFilter>().mesh = mesh;
        MeshRenderer rend = faceObj.AddComponent<MeshRenderer>();
    }
}
