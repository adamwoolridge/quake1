using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class MDLHeader
{
    public int ident;
    public int version;

    public Vector3 scale;
    public Vector3 translate;
    public float boundingRadius;
    public Vector3 eyePos;

    public int skinCount;
    public int skinWidth;
    public int skinHeight;

    public int vertCount;
    public int triCount;
    public int frameCount;

    public int syncType;
    public int flags;

    public float size;    

    public MDLHeader( BinaryReader mdlFile )
    {
        ident = mdlFile.ReadInt32();
        version = mdlFile.ReadInt32();

        scale = new Vector3( mdlFile.ReadSingle() , mdlFile.ReadSingle() , mdlFile.ReadSingle() );
        float y = scale.y;
        scale.y = scale.z;
        scale.z = y;

        translate = new Vector3( mdlFile.ReadSingle(), mdlFile.ReadSingle(), mdlFile.ReadSingle() );
        y = translate.y;
        translate.y = translate.z;
        translate.z = y;

        boundingRadius = mdlFile.ReadSingle();
        eyePos = new Vector3( mdlFile.ReadSingle(), mdlFile.ReadSingle(), mdlFile.ReadSingle() );

        skinCount = mdlFile.ReadInt32();        
        skinWidth = mdlFile.ReadInt32();
        skinHeight = mdlFile.ReadInt32();

        vertCount = mdlFile.ReadInt32();
        triCount = mdlFile.ReadInt32();
        frameCount = mdlFile.ReadInt32();

        syncType = mdlFile.ReadInt32();
        flags = mdlFile.ReadInt32();

        size = mdlFile.ReadSingle();
    }
}
