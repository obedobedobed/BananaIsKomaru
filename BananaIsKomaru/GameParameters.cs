using Microsoft.Xna.Framework;

namespace BananaIsKomaru;

public static class GameParameters
{
    public const int SIZE_MOD = 4;
    public const float FRAME_TIME = 0.15f;
    public static readonly Vector2 PlayerSize = new Vector2(16, 16) * SIZE_MOD;
    public static readonly Vector2 GunSize = new Vector2(8, 8) * SIZE_MOD;
    public static readonly Vector2 BulletSize = new Vector2(8, 8) * SIZE_MOD;
}