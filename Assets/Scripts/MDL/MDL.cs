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
}
