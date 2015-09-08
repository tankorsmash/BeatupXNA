using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using ThirdPartyNinjas.XnaUtility;

namespace BeatupXNA
{
    public class Sprite
    {
        private Game1 game;
        TextureAtlas atlas;
        Texture2D sheet;

        string name;
        TextureRegion region;

        public int x;
        public int y;

        public float scale_x;
        public float scale_y;

        public Sprite (Game1 game, string atlas_name, string sheet_name, string sprite_name) 
        {

            atlas = game.Content.Load<TextureAtlas>(atlas_name);
            region = atlas.GetRegion(sprite_name);

            sheet = game.Content.Load<Texture2D>(sheet_name);

            x = 0;
            y = 0;

            scale_x = 4.0f;
            scale_y = 4.0f;
        }

        public Point _GetRawPosition()
        {
            return new Point(x, y);
        }

        public Point GetContentSize()
        {
            return new Point(
                region.Bounds.Width * (int)scale_x,
                region.Bounds.Height * (int)scale_y
                );
        }

        public Rectangle GetDrawableRect()
        {
            Point content_size = GetContentSize();
            int scaled_x = content_size.X;
            int scaled_y = content_size.Y;

            Point raw_pos = _GetRawPosition();
            int draw_x = raw_pos.X - scaled_x / 2;
            int draw_y = raw_pos.Y - scaled_y / 2;

            return new Rectangle(draw_x, draw_y, scaled_x, scaled_y);
        }

        public void Draw (SpriteBatch sb)
        {


            Rectangle rect = GetDrawableRect();
            sb.Draw(sheet, rect, region.Bounds, Color.White);
        }
    };

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Sprite face;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.graphics.PreferMultiSampling = false;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            face = new Sprite(this, "face_xml", "facepng", "s_baby.png");

            face.x = this.GraphicsDevice.Viewport.Width/2;
            face.y = this.GraphicsDevice.Viewport.Height/2;


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }
            else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            face.Draw(spriteBatch);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
