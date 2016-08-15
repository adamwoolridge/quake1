using UnityEngine;
using System.Collections;
using System.IO;

public class MDLSkin
{
    public int group;
    public byte[] textureData;

    public MDLSkin( BinaryReader mdlFile, MDLHeader header )
    {
        group = mdlFile.ReadInt32();
        
        int byteCount = header.skinWidth  * header.skinHeight;

        textureData = new byte[ byteCount ];
        textureData = mdlFile.ReadBytes( byteCount );        
    }
}
