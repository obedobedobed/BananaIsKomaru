using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static BananaIsKomaru.GameParameters;

namespace BananaIsKomaru;

public class Player : GameObject
{
    // Movement
    private Vector2 velocity = new Vector2();
    private const float SPEED = 75f * SIZE_MOD;

    // Animation
    private float timeToFrame = FRAME_TIME;
    private SpriteEffects flip = SpriteEffects.None;
    private int lastFrame = 0;

    // Combat
    private Gun gun;
    private const int MAX_HEALTH = 10;
    private int health = MAX_HEALTH;
    private const float IMMORTAL_TIME = 0.4f;
    private float immortalTime = 0f;

    // Frames
    private const int IDLE_0 = 0;
    private const int IDLE_1 = 1;
    private const int IDLE_2 = 2;
    private const int RUN_0 = 3;
    private const int RUN_1 = 4;
    private const int RUN_2 = 5;
    private const int RUN_3 = 6;
    private const int RUN_4 = 7;
    private const int RUN_5 = 8;

    // Game
    private float deltaTime = 0f;

    // Input
    private MouseState lastMouse;

    public Player(Atlas atlas, Vector2 position, Vector2 size, Texture2D gunTexture, Texture2D bulletTexture,
    int defaultFrame = 0) : base(atlas, position, size, defaultFrame)
    {
        gun = new Gun(gunTexture, bulletTexture);
    }

    public override void Update(GameTime gameTime)
    {
        deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        immortalTime -= deltaTime;

        GetInput();
        Move();
        Animate();

        gun.Update(gameTime);
    }

    private void GetInput()
    {
        var keyboard = Keyboard.GetState();
        var mouse = Mouse.GetState();

        if (keyboard.IsKeyDown(Keys.D) || keyboard.IsKeyDown(Keys.Right))
            velocity.X = SPEED * deltaTime;
        else if (keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.Left))
            velocity.X = -SPEED * deltaTime;
        else
            velocity.X = 0;

        if (keyboard.IsKeyDown(Keys.W) || keyboard.IsKeyDown(Keys.Up))
            velocity.Y = -SPEED * deltaTime;
        else if (keyboard.IsKeyDown(Keys.S) || keyboard.IsKeyDown(Keys.Down))
            velocity.Y = SPEED * deltaTime;
        else
            velocity.Y = 0;

        if (mouse.LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released)
            gun.Shoot();

        lastMouse = mouse;
    }

    private void Move()
    {
        Position += velocity;
    }

    private void Animate()
    {
        flip = velocity.X != 0 ? velocity.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally : flip;

        if ((timeToFrame -= deltaTime) <= 0)
        {
            int current = Frame;

            if (velocity == Vector2.Zero)
            {
                Frame = Frame switch
                {
                    IDLE_0 => IDLE_1,
                    IDLE_1 => lastFrame == IDLE_0 ? IDLE_2 : IDLE_0,
                    _ => IDLE_1
                };
            }
            else
            {
                Frame = Frame switch
                {
                    RUN_0 => RUN_1,
                    RUN_1 => RUN_2,
                    RUN_2 => RUN_3,
                    RUN_3 => RUN_4,
                    RUN_4 => RUN_5,
                    _ => RUN_0
                };
            }

            lastFrame = current;

            timeToFrame = FRAME_TIME;
        }
    }

    public void TakeDamage(int damage)
    {
        if (immortalTime > 0)
            return;

        health -= damage;
        immortalTime = IMMORTAL_TIME;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Atlas.Texture, Rectangle, Atlas.Rectangles[Frame], immortalTime <= 0 ? Color.White :
        new Color(255, 50, 50), 0f, Vector2.Zero, flip, 0f);
        gun.Draw(spriteBatch);
        Text.Draw($"{health}/{MAX_HEALTH} HP", new Vector2(10, 10), health > MAX_HEALTH / 2 ? Color.LightGreen : 
        Color.Red, spriteBatch, TextDrawingMode.Right, true, Color.Black);
    }
}