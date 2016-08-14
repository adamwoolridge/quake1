using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BSPHeader
{
    long version;

    List<BSPDirectoryEntry> DirectoryEntries;

    public BSPHeader( BinaryReader bspFile )
    {
        DirectoryEntries = new List<BSPDirectoryEntry>();

        bspFile.BaseStream.Seek( 0, SeekOrigin.Begin );
        version = bspFile.ReadUInt32();
        Debug.Log( "BSP version: " + version );
        
        for ( int i = 0; i < (int)DIRECTORY_ENTRY.COUNT; i++ )
        {
            BSPDirectoryEntry entry = new BSPDirectoryEntry(bspFile);
            DirectoryEntries.Add(entry);
        }

        long modelCount = DirectoryEntries[(int)DIRECTORY_ENTRY.MODELS].size / 92;
        Debug.Log("Model count: " + modelCount);
    }
}
