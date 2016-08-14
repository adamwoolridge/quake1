using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BSPMap
{
    private BinaryReader bspFile;

    private BSPHeader header;
    private List<Vector3> vertices;
    private List<BSPFace> faces;   
    private List<BSPEdge> edges;
    private List<BSPModel> models;

    public BSPMap( string mapFileName )
    {
        bspFile = new BinaryReader( File.Open("Assets/Resources/Maps/" + mapFileName, FileMode.Open) );
        header = new BSPHeader( bspFile );

        LoadVertices( bspFile );
        LoadFaces( bspFile );
        LoadEdges( bspFile );
        LoadModels( bspFile );

        bspFile.Close();
    }

    private void LoadFaces( BinaryReader bspFile )
    {
        faces = new List<BSPFace>();

        BSPDirectoryEntry facesEntry = header.GetDirectoryEntry(DIRECTORY_ENTRY.FACES);

        long faceCount = facesEntry.size / 20;

        bspFile.BaseStream.Seek( facesEntry.fileOffset, SeekOrigin.Begin );

        for (int i = 0; i < faceCount; i++)
        {
            faces.Add( new BSPFace( bspFile ) );
        }
    }

    private void LoadVertices( BinaryReader bspFile )
    {
    	vertices = new List<Vector3>();

		BSPDirectoryEntry verticesEntry = header.GetDirectoryEntry( DIRECTORY_ENTRY.MAP_VERTICES );

		long vertCount = verticesEntry.size / 12;

		bspFile.BaseStream.Seek( verticesEntry.fileOffset , SeekOrigin.Begin );

		for ( int i = 0; i < vertCount; i++ )
		{
			vertices.Add( new Vector3( bspFile.ReadSingle(), bspFile.ReadSingle(), bspFile.ReadSingle() ) );
		}
    }

	private void LoadEdges( BinaryReader bspFile )
    {
    	edges = new List<BSPEdge>();

		BSPDirectoryEntry edgesEntry = header.GetDirectoryEntry( DIRECTORY_ENTRY.EDGES );

		long edgeCount = edgesEntry.size / 4;

		bspFile.BaseStream.Seek( edgesEntry.fileOffset , SeekOrigin.Begin );

		for ( int i = 0; i < edgeCount; i++ )
		{
			edges.Add( new BSPEdge( bspFile.ReadUInt16(), bspFile.ReadUInt16() ) );
		}
    }


    private void LoadModels( BinaryReader bspFile )
    {
    	models = new List<BSPModel>();

		BSPDirectoryEntry modelEntry = header.GetDirectoryEntry( DIRECTORY_ENTRY.MODELS );

		long modelCount = modelEntry.size / 64;

    	bspFile.BaseStream.Seek( modelEntry.fileOffset , SeekOrigin.Begin );

    	for ( int i = 0; i < modelCount; i++ )
    	{
    		BSPModel model = new BSPModel( bspFile );	
    		models.Add( model );
    	}
    }
}
