using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MDLFrame
{
    public int type;
    public MDLVert boundingBoxMin;
    public MDLVert boundingBoxMax;
    public string name;
    public List<MDLVert> verts;

    public MDLFrame( BinaryReader mdlFile, MDLHeader header )
    {        
        verts = new List<MDLVert>();

        type = mdlFile.ReadInt32();

        Debug.Log("Type: " + type);

        boundingBoxMin = new MDLVert( mdlFile );
        boundingBoxMax = new MDLVert( mdlFile );
        name = new string( mdlFile.ReadChars( 16 ) );

        //Debug.Log(name);

        for ( int i = 0; i < header.vertCount; i++ )
        {
            verts.Add( new MDLVert( mdlFile ) );
        }
    }
}
