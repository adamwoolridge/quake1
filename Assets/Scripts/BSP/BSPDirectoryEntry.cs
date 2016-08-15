using UnityEngine;
using System.Collections;
using System.IO;

public class BSPDirectoryEntry
{
    public int fileOffset;
    public int size;

    public BSPDirectoryEntry( BinaryReader bspFile )
    {
        fileOffset = bspFile.ReadInt32();
        size = bspFile.ReadInt32();
    }
}

public enum DIRECTORY_ENTRY
{
    ENTITIES,
    PLANES,            
    WALL_TEXTURES,
    MAP_VERTICES,
    LEAVES_VIS, 
    NODES,
    FACE_TEXTURE_INFO,  
    FACES,             
    LIGHTMAPS,
    CLIPNODES,
    LEAVES,
    FACE_LIST,
    EDGES,
    EDGE_LIST,
    MODELS,             
    COUNT = 15
}