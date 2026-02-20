using Microsoft.Xna.Framework;

namespace BananaIsKomaru;

public static class GameParameters
{
    public const int SIZE_MOD = 4;
    public const int TEXT_MOD = 2;
    public const float FRAME_TIME = 0.15f;
    public const int UI_SPACING = 2 * SIZE_MOD;
    public const int HEALTH_BAR_Y_SIZE = 4 * SIZE_MOD;
    public static readonly Vector2 PlayerSize = new Vector2(16, 16) * SIZE_MOD;
    public static readonly Vector2 EnemySize = new Vector2(16, 16) * SIZE_MOD;
    public static readonly Vector2 WallSize = new Vector2(20, 16) * SIZE_MOD;
    public static readonly Vector2 GroundSize = new Vector2(16, 16) * SIZE_MOD;
    public static readonly Vector2 GunSize = new Vector2(8, 8) * SIZE_MOD;
    public static readonly Vector2 BulletSize = new Vector2(8, 8) * SIZE_MOD;
    public static readonly Vector2 GlyphSize = new Vector2(8, 8) * TEXT_MOD;
}