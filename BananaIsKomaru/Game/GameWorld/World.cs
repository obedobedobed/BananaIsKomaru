using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BananaIsKomaru;

public static class World
{
    // Bullets
    public static List<Bullet> bullets { get; private set; } = new List<Bullet>();
    private static List<Bullet> bulletsToRemove = new List<Bullet>();
    
    public static void AddBullet(Bullet bullet)
    {
        bullets.Add(bullet);
    }

    public static void RemoveBullet(Bullet bullet)
    {
        bulletsToRemove.Add(bullet);
    }

    // Update/Draw
    public static void Update(GameTime gameTime)
    {
        // Bullets
        foreach (var bullet in bullets)
            bullet.Update(gameTime);

        foreach (var bullet in bulletsToRemove)
            bullets.Remove(bullet);

        bulletsToRemove.Clear();
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (var bullet in bullets)
            bullet.Draw(spriteBatch);
    }
}