using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Map
{
    public List<Entity> Entities;

    public Map(TextAsset mapAsset)
    {
        Entities = new List<Entity>();

        LoadMap(mapAsset);
    }

    private void LoadMap(TextAsset mapAsset)
    {
        string mapString = mapAsset.text;

        string[] lines = Regex.Split(mapString, "\n|\r|\r\n");
        
        foreach (string str in lines)
        {
            Debug.Log(str);
        }

    }
}
