using UnityEngine;
using System.Collections;
using System.IO;

public class MDLTriangle
{
    public int facesFront;
    public int [] vertexIndexes;

    public MDLTriangle( BinaryReader mdlFile )
    {        
        facesFront = mdlFile.ReadInt32();

        vertexIndexes = new int[ 3 ];
        for ( int i = 0; i < 3; i++)
        {
            vertexIndexes[ i ] = mdlFile.ReadInt32();
        }
    }
}
