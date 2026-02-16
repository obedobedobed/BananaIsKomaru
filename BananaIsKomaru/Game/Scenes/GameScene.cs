using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using static BananaIsKomaru.GameParameters;

namespace BananaIsKomaru;

public class GameScene(SpriteBatch s, GraphicsDeviceManager g) : Scene(s, g)
{
    public static GameScene Instance { get; private set; }
    public Player Player { get; private set; }
    private Enemy testEnemy;
    private List<Bullet> bullets = new List<Bullet>();

    public override void Load(ContentManager Content)
    {
        Instance = this;

        var playerAtlas = new Atlas(Content.Load<Texture2D>("Sprites/Player"), (PlayerSize / SIZE_MOD).ToPoint());
        Player = new Player(playerAtlas, new Vector2(200, 200), PlayerSize,
        Content.Load<Texture2D>("Sprites/Gun"), Content.Load<Texture2D>("Sprites/Bullet"));

        var enemyAtlas = new Atlas(Content.Load<Texture2D>("Sprites/Enemy"), (EnemySize / SIZE_MOD).ToPoint());
        testEnemy = new Enemy(enemyAtlas, new Vector2(500, 200), EnemySize, 0);
    }

    public override void Update(GameTime gameTime)
    {
        Player.Update(gameTime);
        World.Update(gameTime);
        testEnemy.Update(gameTime, Player);
    }

    public void AddBullet(Bullet bullet) => bullets.Add(bullet);

    public override void Draw()
    {
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        Player.Draw(SpriteBatch);
        World.Draw(SpriteBatch);
        testEnemy.Draw(SpriteBatch);

        SpriteBatch.End();
    }
}