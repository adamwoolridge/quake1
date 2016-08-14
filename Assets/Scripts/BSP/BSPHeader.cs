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

        for ( int i = 0; i < (int)DIRECTORY_ENTRY.COUNT; i++ )
        {
            BSPDirectoryEntry entry = new BSPDirectoryEntry( bspFile );
            DirectoryEntries.Add( entry );
        }
    }

    public BSPDirectoryEntry GetDirectoryEntry( DIRECTORY_ENTRY type )
    {
    	return DirectoryEntries[ (int)type ];
    }
}
