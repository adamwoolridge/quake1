using UnityEngine;
using System.Collections;
using System.IO;

public class BSPDirectoryEntry
{
    public long fileOffset;
    public long size;

    public BSPDirectoryEntry( BinaryReader bspFile )
    {
        fileOffset = bspFile.ReadUInt32();
        size = bspFile.ReadUInt32();
    }
}

public enum DIRECTORY_ENTRY
{
    ENTITIES,
    PLANES,             // numplanes = size / sizeof(plane)
    WALL_TEXTURES,
    MAP_VERTICES,
    LEAVES_VIS, 
    NODES,
    FACE_TEXTURE_INFO,  
    FACES,              // numfaces = size/sizeof(face)
    LIGHTMAPS,
    CLIPNODES,
    LEAVES,
    FACE_LIST,
    EDGES,
    EDGE_LIST,
    MODELS,              // nummodels = Size/sizeof(model)
    COUNT = 15
}