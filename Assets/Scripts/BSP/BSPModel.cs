using UnityEngine;
using System.Collections;
using System.IO;

public class BSPModel
{
    public BSPBoundingBox boundingBox; 
    public Vector3 origin; 
    public long node_id0; 
    public long node_id1; 
    public long node_id2; 
    public long node_id3; 
    public long leafCount;
    public long faceIndex; 
    public long faceCount; 

	public BSPModel( BinaryReader bspFile )
    {
    	boundingBox = new BSPBoundingBox( new Vector3( bspFile.ReadSingle(), bspFile.ReadSingle(), bspFile.ReadSingle() ),
    									  new Vector3( bspFile.ReadSingle(), bspFile.ReadSingle(), bspFile.ReadSingle() ) );

    	origin = new Vector3( bspFile.ReadSingle(), bspFile.ReadSingle(), bspFile.ReadSingle() );
    	node_id0 = bspFile.ReadUInt32();
		node_id1 = bspFile.ReadUInt32();
		node_id2 = bspFile.ReadUInt32();
		node_id3 = bspFile.ReadUInt32();
    	leafCount = bspFile.ReadUInt32();
    	faceIndex = bspFile.ReadUInt32();
    	faceCount = bspFile.ReadUInt32();						  
    }
}
