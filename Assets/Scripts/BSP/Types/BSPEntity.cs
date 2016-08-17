using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BSPEntity
{
    public Dictionary<string, string> KeyValues;

    public BSPEntity( StringReader entityText )
    {
        KeyValues = new Dictionary<string, string>();

        string line;
        do
        {
            line = entityText.ReadLine();
            if ( line != null )
            {
                if ( line == "}" )
                    return;

                // Read key/value pairs by splitting by quotes, eg: "origin" "958 526 12"
                string [] tokens = line.Split( '"' );
                KeyValues.Add( tokens[ 1 ], tokens[ 3 ] );                
            }
        } while ( line != null );
    }
}
