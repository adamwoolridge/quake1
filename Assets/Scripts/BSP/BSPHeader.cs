using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BSPHeader
{    
    List<BSPDirectoryEntry> DirectoryEntries;

    public BSPHeader( BinaryReader bspFile )
    {
        DirectoryEntries = new List<BSPDirectoryEntry>();

        bspFile.BaseStream.Seek( 0, SeekOrigin.Begin );

        bspFile.ReadInt32(); // skip version

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
