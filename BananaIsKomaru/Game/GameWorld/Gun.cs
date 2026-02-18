using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static BananaIsKomaru.GameParameters;

namespace BananaIsKomaru;

public class Gun(Texture2D texture, Texture2D bulletTexture)
: GameObject(texture, Vector2.Zero, GunSize)
{
    private Texture2D bulletTexture = bulletTexture;
    private float rotation = 0f;
    private SpriteEffects flip = SpriteEffects.None;

    public override void Update(GameTime gameTime)
    {
        Position = GameScene.Instance.Player.Position + PlayerSize / 2;
        Position += new Vector2(0, 4 * SIZE_MOD);

        var mouse = Mouse.GetState();
        rotation = MathF.Atan2
        (
            mouse.Y - Position.Y,
            mouse.X - Position.X
        );

        if (rotation > MathF.PI / 2 || rotation < -MathF.PI / 2)
            flip = SpriteEffects.FlipVertically;
        else
            flip = SpriteEffects.None;
    }

    public void Shoot()
    {
        World.AddBullet(new Bullet(bulletTexture, Position, BulletSize, dir: new Vector2
        ((float)Math.Cos(rotation), (float)Math.Sin(rotation)), rot: rotation));
    }

    public void Draw(SpriteBatch spriteBatch, float layer)
    {
        spriteBatch.Draw(Texture, Rectangle, new Rectangle(0, 0, (int)Size.X / SIZE_MOD, (int)Size.Y / SIZE_MOD),
        Color.White, rotation, new Vector2(Texture.Width / 2, Texture.Height / 2), flip, layer);
    }
}