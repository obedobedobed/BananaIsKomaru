using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static BananaIsKomaru.GameParameters;

namespace BananaIsKomaru;

public class Bullet : GameObject
{
    private const float SPEED = 95f * SIZE_MOD;
    private Vector2 dir;
    private float rot;
    private float lifeTime = 1.5f;

    public Bullet(Texture2D texture, Vector2 position, Vector2 size, Vector2 dir, float rot) : base(texture, position, size)
    {
        this.dir = dir;
        this.rot = rot;
    }

    public override void Update(GameTime gameTime)
    {
        Position += dir * SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if ((lifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds) <= 0)
            World.RemoveBullet(this);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Rectangle, new Rectangle(0, 0, (int)Size.X / SIZE_MOD, (int)Size.Y / SIZE_MOD),
        Color.White, rot, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 0f);
    }
}