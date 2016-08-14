using UnityEngine;
using System.Collections;
using System.IO;

public class BSPMap
{
    private BinaryReader bspFile;

    private BSPHeader header;

    public BSPMap(string mapFileName)
    {
        bspFile = new BinaryReader(File.Open("Assets/Resources/Maps/" + mapFileName, FileMode.Open));
        header = new BSPHeader(bspFile);

        bspFile.Close();
    }
}
