using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BananaIsKomaru;

public class GameObject
{
    public Atlas Atlas { get; protected set; }
    public int Frame { get; protected set; }
    public Texture2D Texture { get; protected set; }
    public Vector2 Position { get; protected set; }
    public Vector2 Size { get; protected set; }

    public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    public GameObject(Atlas atlas, Vector2 position, Vector2 size, int defaultFrame = 0)
    {
        Atlas = atlas;
        Position = position;
        Size = size;
        Frame = defaultFrame;
    }

    public GameObject(Texture2D texture, Vector2 position, Vector2 size)
    {
        Texture = texture;
        Position = position;
        Size = size;
    }

    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        if (Texture != null)
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        else
            spriteBatch.Draw(Atlas.Texture, Rectangle, Atlas.Rectangles[Frame], Color.White);
    }

    public bool IsObjectInRadius(GameObject obj, float radius)
    {
        var distance = (obj.Position + obj.Size / 2) - (Position + Size / 2);

        return distance.LengthSquared() <= radius;
    }
}