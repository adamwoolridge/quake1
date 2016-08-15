using UnityEngine;
using System.Collections;
using System.IO;

public class BSPTexture
{
    public string name;
    public int width;
    public int height;
    public int offset;
    public byte[] textureData;
    public Texture2D texture;

    public BSPTexture( BinaryReader bspFile, BSPPalette palette )
    {        
        name = new string( bspFile.ReadChars( 16 ) );
        width = bspFile.ReadInt32();
        height = bspFile.ReadInt32();
        offset = bspFile.ReadInt32();

        // Skip past the next 3 offsets, straight to the start of fullsize mip texture data
        bspFile.BaseStream.Seek( 12, SeekOrigin.Current );
        int byteCount = width * height;

        textureData = new byte[ byteCount ];
        textureData = bspFile.ReadBytes( byteCount );

        texture = new Texture2D( width, height );

        int index = 0;
        
        for ( int y = 0; y < height; y++ )
        {
            for ( int x = 0; x < width; x++ )
            {
                texture.SetPixel( x, y, palette.Colours[ textureData[ index ] ] );
                index++;
            }
        }
        
        texture.Apply();       
    }
}
