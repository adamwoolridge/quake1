using UnityEngine;
using System.Collections;
using System.IO;

public class MDLSkin
{
    public int group;
    public byte[] textureData;

    public Texture2D texture;

    public MDLSkin( BinaryReader mdlFile, MDLHeader header, BSPPalette palette )
    {
        group = mdlFile.ReadInt32();
        
        int byteCount = header.skinWidth  * header.skinHeight;

        textureData = new byte[ byteCount ];
        textureData = mdlFile.ReadBytes( byteCount );

        texture = new Texture2D(header.skinWidth, header.skinHeight);

        int index = 0;

        for (int y = 0; y < header.skinHeight; y++)
        {
            for (int x = 0; x < header.skinWidth; x++)
            {
                texture.SetPixel(x, y, palette.Colours[textureData[index]]);
                index++;
            }
        }

        texture.Apply();
    }
}
