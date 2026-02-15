using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BananaIsKomaru;

public class Atlas
{
    public Texture2D Texture { get; private set; }
    public Rectangle[] Rectangles { get; private set; }

    public Atlas(Texture2D texture, Point spriteSize)
    {
        Texture = texture;

        var tmpRects = new List<Rectangle>();

        int yPos = 0;
        for (int y = 0; y < Texture.Height / spriteSize.Y; y++)
        {
            int xPos = 0;
            for (int x = 0; x < Texture.Width / spriteSize.X; x++)
            {
                tmpRects.Add(new Rectangle(xPos, yPos, spriteSize.X, spriteSize.Y));

                xPos += spriteSize.X;
            }

            yPos += spriteSize.Y;
        }

        Rectangles = tmpRects.ToArray();
    }
}