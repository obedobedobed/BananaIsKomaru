using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BananaIsKomaru;

public static class World
{
    // Bullets
    public static List<Bullet> Bullets { get; private set; } = new List<Bullet>();
    private static List<Bullet> bulletsToRemove = new List<Bullet>();
    
    public static void AddBullet(Bullet bullet)
    {
        Bullets.Add(bullet);
    }

    public static void RemoveBullet(Bullet bullet)
    {
        bulletsToRemove.Add(bullet);
    }

    // Enemies
    public static List<Enemy> Enemies { get; private set; } = new List<Enemy>();
    private static List<Enemy> enemiesToRemove = new List<Enemy>();
    
    public static void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
    }

    public static void RemoveEnemy(Enemy enemy)
    {
        enemiesToRemove.Add(enemy);
    }

    // Walls
    public static List<Wall> Walls { get; private set; } = new List<Wall>();
    private static List<Wall> wallsToRemove = new List<Wall>();
    
    public static void AddWall(Wall wall)
    {
        Walls.Add(wall);
    }

    public static void RemoveWall(Wall wall)
    {
        wallsToRemove.Add(wall);
    }

    // Update/Draw
    public static void Update(GameTime gameTime)
    {
        // Bullets
        foreach (var bullet in Bullets)
            bullet.Update(gameTime);

        foreach (var bullet in bulletsToRemove)
            Bullets.Remove(bullet);

        bulletsToRemove.Clear();

        // Enemies
        foreach (var enemy in Enemies)
            enemy.Update(gameTime, GameScene.Instance.Player);

        foreach (var enemy in enemiesToRemove)
            Enemies.Remove(enemy);

        enemiesToRemove.Clear();
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        foreach (var bullet in Bullets)
            bullet.Draw(spriteBatch);

        foreach (var enemy in Enemies)
            enemy.Draw(spriteBatch);
    }

    // QoL
    public static void Clear()
    {
        Bullets.Clear();
        Enemies.Clear();
    }
}