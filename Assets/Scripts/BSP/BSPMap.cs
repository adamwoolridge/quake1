using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BSPMap
{
    private BinaryReader bspFile;

    public BSPHeader header;
    public List<Vector3> vertices;
    public List<BSPFace> faces;
    public List<int> faceList;
    public List<BSPEdge> edges;
    public List<int> edgeList;
    public List<BSPModel> models;
    

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
        // Faces
        faces = new List<BSPFace>();

        BSPDirectoryEntry facesEntry = header.GetDirectoryEntry(DIRECTORY_ENTRY.FACES);
        int faceCount = facesEntry.size / 20;

        bspFile.BaseStream.Seek( facesEntry.fileOffset, SeekOrigin.Begin );

        for (int i = 0; i < faceCount; i++)
        {
            faces.Add( new BSPFace( bspFile ) );
        }

        // Face list
        faceList = new List<int>();

        BSPDirectoryEntry faceListEntry = header.GetDirectoryEntry(DIRECTORY_ENTRY.FACE_LIST);
        int faceListCount = faceListEntry.size / 4;

        bspFile.BaseStream.Seek(faceListEntry.fileOffset, SeekOrigin.Begin);

        for (int i = 0; i < faceListCount; i++)
        {
            faceList.Add( bspFile.ReadInt32() );
        }
    }

    private void LoadVertices( BinaryReader bspFile )
    {        
    	vertices = new List<Vector3>();

		BSPDirectoryEntry verticesEntry = header.GetDirectoryEntry( DIRECTORY_ENTRY.MAP_VERTICES );
		int vertCount = verticesEntry.size / 12;

		bspFile.BaseStream.Seek( verticesEntry.fileOffset , SeekOrigin.Begin );

		for ( int i = 0; i < vertCount; i++ )
		{
            // Read vertice and flip Y/Z to match Quake 1
            float x = bspFile.ReadSingle();
            float y = bspFile.ReadSingle();
            float z = bspFile.ReadSingle();

            vertices.Add( new Vector3( x, z, y ) );
		}
    }

	private void LoadEdges( BinaryReader bspFile )
    {
        // Edges
    	edges = new List<BSPEdge>();

		BSPDirectoryEntry edgesEntry = header.GetDirectoryEntry( DIRECTORY_ENTRY.EDGES );
		int edgeCount = edgesEntry.size / 4;

		bspFile.BaseStream.Seek( edgesEntry.fileOffset , SeekOrigin.Begin );

		for ( int i = 0; i < edgeCount; i++ )
		{
			edges.Add( new BSPEdge( bspFile.ReadUInt16(), bspFile.ReadUInt16() ) );
		}

        // Edge list
        edgeList = new List<int>();

        BSPDirectoryEntry edgeListEntry = header.GetDirectoryEntry(DIRECTORY_ENTRY.EDGE_LIST);
        long edgeListCount = edgeListEntry.size / 4;

        bspFile.BaseStream.Seek(edgeListEntry.fileOffset, SeekOrigin.Begin);

        for (int i = 0; i < edgeListCount; i++)
        {
            edgeList.Add(bspFile.ReadInt32());
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
