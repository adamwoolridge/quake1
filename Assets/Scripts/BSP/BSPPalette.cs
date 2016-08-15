using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class BSPPalette
{
    public Color[] Colours = new Color[256];

    public BSPPalette( string paletteFileName )
    {
        BinaryReader paletteFile = new BinaryReader(File.Open("Assets/Resources/Palettes/" + paletteFileName, FileMode.Open));

        paletteFile.BaseStream.Seek(0, SeekOrigin.Begin);

        for ( int i = 0; i < 256; i++ )
        {
            Colours[i] = new Color( paletteFile.ReadByte()/255f, paletteFile.ReadByte()/255f, paletteFile.ReadByte()/255f );
        }

        paletteFile.Close();
    }    
}
