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
    private List<Bullet> bullets = new List<Bullet>();
    public static Texture2D pixel;

    public override void Load(ContentManager Content)
    {
        Instance = this;

        var playerAtlas = new Atlas(Content.Load<Texture2D>("Sprites/Player"), (PlayerSize / SIZE_MOD).ToPoint());
        Player = new Player(playerAtlas, new Vector2(200, 200), PlayerSize,
        Content.Load<Texture2D>("Sprites/Gun"), Content.Load<Texture2D>("Sprites/Bullet"));

        var enemyAtlas = new Atlas(Content.Load<Texture2D>("Sprites/Enemy"), (EnemySize / SIZE_MOD).ToPoint());
        World.AddEnemy(new Enemy(enemyAtlas, new Vector2(500, 200), EnemySize, 0));

        pixel = Content.Load<Texture2D>("Sprites/Pixel");
    }

    public override void Update(GameTime gameTime)
    {
        Player.Update(gameTime);
        World.Update(gameTime);
    }

    public void AddBullet(Bullet bullet) => bullets.Add(bullet);

    public override void Draw()
    {
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        Player.Draw(SpriteBatch);
        World.Draw(SpriteBatch);

        SpriteBatch.End();
    }
}