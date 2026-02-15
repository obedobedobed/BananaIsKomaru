using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BananaIsKomaru;

public static class SceneManager
{
    public static Scene Scene { get; private set; }
    
    public static void Load(Scene scene, ContentManager contentManager)
    {
        Scene = scene;
        Scene.Load(contentManager);
    }

    public static void Update(GameTime gameTime) => Scene.Update(gameTime);
    public static void Draw() => Scene.Draw();
}