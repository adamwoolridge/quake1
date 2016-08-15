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

    public void DrawFrame( int frame )
    {
        Vector3 [] allVerts = new Vector3[ header.vertCount ];
        //int[] triangles = new int[ header.triCount];


        for ( int i = 0; i < header.triCount; i++ )
        {
            Vector3[] vertices = new Vector3[ 3];

            for ( int v = 0; v < 3; v ++ )
            {
                MDLVert thisVert = frames[frame].verts[triangles[i].vertexIndexes[v]];
                vertices[v] = new Vector3();
                vertices[v].x = (header.scale.x * thisVert.v[0]) + header.translate[0];
                vertices[v].y = (header.scale.y * thisVert.v[2]) + header.translate[2];
                vertices[v].z = (header.scale.z * thisVert.v[1]) + header.translate[1];

                
            }

            Debug.DrawLine(vertices[0], vertices[1], Color.white, 10000f);
            Debug.DrawLine(vertices[1], vertices[2], Color.white, 10000f);
            Debug.DrawLine(vertices[2], vertices[0], Color.white, 10000f);
        }
    }
}
