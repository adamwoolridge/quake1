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

    public int GetInt(string name)
    {
        string val = "";

        if (KeyValues.TryGetValue(name, out val))
        {
            return int.Parse(val);
        }
        else
        {
            return 0;
        }

        
    }

    public Vector3 GetVector3(string name)
    {
        string [] tokens = KeyValues[name].Split(null);        
        return new Vector3(float.Parse(tokens[0]), float.Parse(tokens[2]), float.Parse(tokens[1]));
    }
}
