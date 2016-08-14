using UnityEngine;
using System.Collections;
using System.IO;

public class BSPFace
{
    public ushort planeIndex;
    public ushort sideOfPlane;
    public long edgeListIndex;
    public ushort edgeCount;
    public ushort textureInfoIndex;
    public char lightingType;
    public char baseLight;
    public char lightm0;
    public char lightm1;
    public long lightmap;

	public BSPFace( BinaryReader bspFile )
	{
        planeIndex = bspFile.ReadUInt16();
        sideOfPlane = bspFile.ReadUInt16();
        edgeListIndex = bspFile.ReadInt32();
        edgeCount = bspFile.ReadUInt16();
        textureInfoIndex = bspFile.ReadUInt16();
        lightingType = bspFile.ReadChar();
        baseLight = bspFile.ReadChar();
        lightm0 = bspFile.ReadChar();
        lightm1 = bspFile.ReadChar();
        lightmap = bspFile.ReadInt32();
    }
}
