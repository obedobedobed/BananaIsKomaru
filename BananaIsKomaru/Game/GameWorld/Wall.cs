using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static BananaIsKomaru.GameParameters;

namespace BananaIsKomaru;

public class Wall : GameObject
{
    public Wall(Texture2D texture, Vector2 position)
    : base(texture, position, WallSize) { }
}