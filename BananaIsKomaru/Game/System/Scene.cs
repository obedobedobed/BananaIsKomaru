using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BananaIsKomaru;

public abstract class Scene(SpriteBatch spriteBatch, GraphicsDeviceManager graphicsManager)
{
    protected SpriteBatch SpriteBatch = spriteBatch;
    protected GraphicsDeviceManager GraphicsManager = graphicsManager;

    public abstract void Load(ContentManager Content);
    public abstract void Update(GameTime gameTime);
    public abstract void Draw();
}