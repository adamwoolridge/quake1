using UnityEngine;
using System.Collections;
using System.IO;

public class MDLVert
{
    public byte [] v;
    public byte normalIndex;

    public MDLVert( BinaryReader mdlFile)
    {        
        v = new byte[ 3 ];                   
        for ( int i = 0; i < 3; i++ )
        {
            v[ i ] = mdlFile.ReadByte();
        }
       
        normalIndex = mdlFile.ReadByte();
    }
}
