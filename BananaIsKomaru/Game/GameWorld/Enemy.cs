using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static BananaIsKomaru.GameParameters;

namespace BananaIsKomaru;

public class Enemy : GameObject
{
    // Movement
    private Vector2 velocity = new Vector2();
    private const float SPEED = 35f * SIZE_MOD;

    // Animation
    private float timeToFrame = FRAME_TIME;
    private SpriteEffects flip = SpriteEffects.None;
    private int lastFrame = 0;

    // Combat
    private const int MAX_HEALTH = 5;
    private int health = MAX_HEALTH;

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

    public Enemy(Atlas atlas, Vector2 position, Vector2 size, int defaultFrame = 0)
    : base(atlas, position, size, defaultFrame) { }

    public void Update(GameTime gameTime, Player player)
    {
        deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        Move(player);
        Animate();
    }

    private void Move(Player player)
    {
        Vector2 dir = player.Position - Position;
        if (dir != Vector2.Zero)
            dir.Normalize();

        velocity = dir * SPEED * deltaTime;
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
        health -= damage;
        if (health <= 0)
            World.RemoveEnemy(this);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Atlas.Texture, Rectangle, Atlas.Rectangles[Frame], Color.White,
        0f, Vector2.Zero, flip, 0f);

        float healthBarWidth = Size.X - 4 * SIZE_MOD;
        var healthBarPos = new Vector2(Rectangle.Left + 2 * SIZE_MOD, Rectangle.Bottom + UI_SPACING);
        spriteBatch.Draw(GameScene.pixel, new Rectangle((int)healthBarPos.X, (int)healthBarPos.Y,
        (int)healthBarWidth, HEALTH_BAR_Y_SIZE), Color.Black);

        int healthScaleWidth = (int)(healthBarWidth * (health / (float)MAX_HEALTH));
        spriteBatch.Draw(GameScene.pixel, new Rectangle((int)healthBarPos.X, (int)healthBarPos.Y,
        healthScaleWidth, HEALTH_BAR_Y_SIZE), Color.LightGreen);
    }
}