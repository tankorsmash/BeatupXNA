using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ThirdPartyNinjas.XnaUtility;

namespace Test
{
	public class TestGame : Microsoft.Xna.Framework.Game
	{
		public TestGame()
		{
			new GraphicsDeviceManager(this)
			{
				PreferredBackBufferWidth = 1280,
				PreferredBackBufferHeight = 720,
			};

			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			texture = Content.Load<Texture2D>("texture");
			textureAtlas = Content.Load<TextureAtlas>("atlasJSON");
			textureRegion = textureAtlas.GetRegion("Enemy Bug");
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			spriteBatch.Draw(texture, new Vector2(640, 360), textureRegion.Bounds, Color.White);
			spriteBatch.Draw(texture, new Vector2(640, 360), textureRegion.Bounds, Color.White, 0, textureRegion.OriginTopLeft, 1, SpriteEffects.None, 0);
			spriteBatch.Draw(texture, new Vector2(640, 360), textureRegion.Bounds, Color.White, 0, textureRegion.OriginCenter, 1, SpriteEffects.None, 0);
			spriteBatch.Draw(texture, new Vector2(640, 360), textureRegion.Bounds, Color.White, 0, textureRegion.OriginBottomRight, 1, SpriteEffects.None, 0);
			spriteBatch.End();

			base.Draw(gameTime);
		}

		static void Main(string[] args)
		{
			using (TestGame game = new TestGame())
			{
				game.Run();
			}
		}

		SpriteBatch spriteBatch;
		TextureAtlas textureAtlas;
		TextureRegion textureRegion;
		Texture2D texture;
	}

	public class Foobar : SpriteBatch
	{
		Foobar(GraphicsDevice graphicsDevice)
			: base(graphicsDevice)
		{
		}
	}
}
