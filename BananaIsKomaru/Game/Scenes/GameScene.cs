using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static BananaIsKomaru.GameParameters;

namespace BananaIsKomaru;

public class GameScene(SpriteBatch s, GraphicsDeviceManager g) : Scene(s, g)
{
    public static GameScene Instance { get; private set; }
    public Player Player { get; private set; }
    private List<Bullet> bullets = new List<Bullet>();
    public bool GameOver = false;
    public static Texture2D Pixel;
    public static Texture2D Circle;

    public override void Load(ContentManager Content)
    {
        Instance = this;
        World.Clear();

        var playerAtlas = new Atlas(Content.Load<Texture2D>("Sprites/Player"), (PlayerSize / SIZE_MOD).ToPoint());
        Player = new Player(playerAtlas, new Vector2(200, 200), PlayerSize,
        Content.Load<Texture2D>("Sprites/Gun"), Content.Load<Texture2D>("Sprites/Bullet"));

        var enemyAtlas = new Atlas(Content.Load<Texture2D>("Sprites/Enemy"), (EnemySize / SIZE_MOD).ToPoint());
        World.AddEnemy(new Enemy(enemyAtlas, new Vector2(500, 200), EnemySize, 0));

        Pixel = Content.Load<Texture2D>("Sprites/Pixel");
        Circle = Content.Load<Texture2D>("Sprites/Circle");
    }

    public override void Update(GameTime gameTime)
    {
        var keyboard = Keyboard.GetState();

        Player.Update(gameTime);
        World.Update(gameTime);

        if (GameOver && keyboard.IsKeyDown(Keys.Enter))
            SceneManager.Load(new GameScene(SpriteBatch, GraphicsManager), Game1.Instance.Content);
    }

    public void AddBullet(Bullet bullet) => bullets.Add(bullet);

    public override void Draw()
    {
        SpriteBatch.Begin(samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack,
        blendState: BlendState.AlphaBlend);

        World.Draw(SpriteBatch);
        Player.Draw(SpriteBatch);

        if (GameOver)
        {
            SpriteBatch.Draw(Pixel, new Rectangle(0, 0, GraphicsManager.PreferredBackBufferWidth,
            GraphicsManager.PreferredBackBufferHeight), new Color(0, 0, 0, 150));
            Text.Draw("Game Over! Press ENTER to restart...", new Vector2(GraphicsManager.PreferredBackBufferWidth / 2,
            GraphicsManager.PreferredBackBufferHeight / 2), Color.White, SpriteBatch, TextDrawingMode.Center, 1f);
        }

        SpriteBatch.End();
    }
}