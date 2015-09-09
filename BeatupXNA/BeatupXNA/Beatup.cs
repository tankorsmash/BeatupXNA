using System;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeatupXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Beatup : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera2d camera;

        private Sprite face;
        public Sprite left_fist;
        public Sprite right_fist;

        public Beatup()
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
            this.graphics.PreferredBackBufferHeight = 640;
            this.graphics.PreferredBackBufferWidth = 960;
            this.graphics.ApplyChanges();

            this.IsMouseVisible = true;

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

            left_fist = new Sprite(this, "face_xml", "facepng", "fist_neutral.png");
            left_fist.x = face.x - 200;
            left_fist.y = face.y + 200;
            left_fist.flippedX = true;

            right_fist = new Sprite(this, "face_xml", "facepng", "fist_neutral.png");
            right_fist.x = face.x + 200;
            right_fist.y = face.y + 200;

            camera = new Camera2d();
            camera.Pos = new Vector2(graphics.GraphicsDevice.Viewport.Width/2, graphics.GraphicsDevice.Viewport.Height/2);

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

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.S))
            {
            }
            else
            {

                Viewport viewport = graphics.GraphicsDevice.Viewport;
                camera.Pos = new Vector2(viewport.Width/2, viewport.Height/2);
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Q))
            {
                // left_fist.x = 5;
                left_fist.x = face.x - 100;
                left_fist.y = face.y + 50;
                left_fist.rotation = 100/360f;
                int intensity = 5;
                ShakeCamera(intensity);
            }
            else
            {
                left_fist.x = face.x - 200;
                left_fist.y = face.y + 200;
                left_fist.rotation = 0;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.E))
            {
                right_fist.x = face.x + 100;
                right_fist.y = face.y + 50;
                right_fist.rotation = -100/360f;
                int intensity = 5;
                ShakeCamera(intensity);
            }
            else
            {
                right_fist.x = face.x + 200;
                right_fist.y = face.y + 200;
                right_fist.rotation = 0.0f;
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public void ShakeCamera(int intensity)
        {
            Random rand = new Random();
            camera.Pos = new Vector2(camera.Pos.X + rand.Next(-10, 10), camera.Pos.Y + rand.Next(-10, 10));
            Viewport viewport = graphics.GraphicsDevice.Viewport;
            camera.Pos = new Vector2(viewport.Width/2 + rand.Next(-intensity, intensity),
                viewport.Height/2 + rand.Next(-intensity, intensity));
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Matrix camera = new Matrix(1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1);
            Viewport viewport = GraphicsDevice.Viewport;
            //Matrix camera =  Matrix.CreateOrthographicOffCenter(0, viewport.Width, viewport.Height, 0, 0, 1);
            //Matrix camera = Matrix.CreateLookAt()

            // cam.Zoom = 2.0f // Example of Zoom in
            // cam.Zoom = 0.5f // Example of Zoom out

            //// if using XNA 4.0
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                                    BlendState.AlphaBlend,
                                    SamplerState.PointClamp, 
                                    null,
                                    null,
                                    null,
                                    camera.get_transformation(graphics.GraphicsDevice));
            GraphicsDevice.Clear(Color.CornflowerBlue);
 //           spriteBatch.Begin(SpriteSortMode.BackToFront,
 //               BlendState.NonPremultiplied,
 //SamplerState.PointClamp,
 //DepthStencilState.Default,
 //RasterizerState.CullNone,
 //null, camera);

            left_fist.Draw(spriteBatch);
            right_fist.Draw(spriteBatch);
            face.Draw(spriteBatch);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
