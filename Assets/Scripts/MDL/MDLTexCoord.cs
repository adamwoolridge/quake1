using UnityEngine;
using System.Collections;
using System.IO;

public class MDLTexCoord
{
    public int onSeam;
    public int s;
    public int t;

    public MDLTexCoord( BinaryReader mdlFile )
    {
        onSeam = mdlFile.ReadInt32();
        s = mdlFile.ReadInt32();
        t = mdlFile.ReadInt32();
    }
}
