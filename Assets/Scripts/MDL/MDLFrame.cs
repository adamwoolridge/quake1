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
        
        boundingBoxMin = new MDLVert( mdlFile );
        boundingBoxMax = new MDLVert( mdlFile );
        
        byte [] result = mdlFile.ReadBytes( 16 );
        name = System.Text.Encoding.Default.GetString( result );                

        for ( int i = 0; i < header.vertCount; i++ )
        {
            verts.Add( new MDLVert( mdlFile ) );
        }
    }
}
