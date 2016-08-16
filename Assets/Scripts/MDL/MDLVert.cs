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

        v[0] = mdlFile.ReadByte();
        v[2] = mdlFile.ReadByte();
        v[1] = mdlFile.ReadByte();
       
        normalIndex = mdlFile.ReadByte();
    }
}
