using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ThirdPartyNinjas.XnaUtility;

namespace BeatupXNA
{
    public class Camera2d
    {
        protected float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        protected float _rotation; // Camera Rotation

        public Camera2d()
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
        }

        // Sets and gets zoom
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }
        // Get set position
        public Vector2 Pos
        {
            get { return _pos; }
            set { _pos = value; }
        }
        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            Viewport viewport = graphicsDevice.Viewport;
            _transform =       // Thanks to o KB o for this solution
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
            return _transform;
        }
    }
    public class Clock
    {
        private float time_elapsed;
        private float threshold;

        public void reset() { time_elapsed = 0; }
        public void set_threshold(float threshold) { this.threshold = threshold; }
        public bool passed_threshold() { return this.time_elapsed >= threshold; }

        public float get_percentage() {
            if (this.time_elapsed <= 0.0f) { return 0.0f; }
            else { return this.time_elapsed / this.threshold; };
        }
        public bool is_started() { return this.time_elapsed > 0; }
        public bool is_active() { return this.is_started() && !this.passed_threshold(); }

        public void start() { time_elapsed = 0.001f; } //figure out a better way to do this
        public void start_for_thres(float val) { this.set_threshold(val); this.start(); }
    };

    public class Sprite
    {
        private Game1 game;
        TextureAtlas atlas;
        Texture2D sheet;

        public float rotation;

        string name;
        TextureRegion region;

        public int x;
        public int y;

        public bool flippedX;

        public float scale_x;
        public float scale_y;

        public Sprite (Game1 game, string atlas_name, string sheet_name, string sprite_name) 
        {

            atlas = game.Content.Load<TextureAtlas>(atlas_name);
            region = atlas.GetRegion(sprite_name);

            sheet = game.Content.Load<Texture2D>(sheet_name);

            x = 0;
            y = 0;

            flippedX = false;
            rotation = 0;

            scale_x = 4.0f;
            scale_y = 4.0f;
        }

        public Point _GetRawPosition()
        {
            return new Point(x, y);
        }

        /// <summary>
        /// size of sprite scaled up
        /// </summary>
        /// <returns></returns>
        public Point GetContentSize()
        {
            return new Point(
                region.Bounds.Width * (int)scale_x,
                region.Bounds.Height * (int)scale_y
                );
        }

        /// <summary>
        /// scaled up sprite rect offset to the center of the sprite
        /// </summary>
        /// <returns></returns>
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
            sb.Draw(sheet, rect, region.Bounds, Color.White, rotation, new Vector2(0.5f, 0.5f), flippedX ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
        }
    };

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera2d camera;

        private Sprite face;
        public Sprite left_fist;
        public Sprite right_fist;

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

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Q))
            {
                // left_fist.x = 5;
                left_fist.x = face.x - 100;
                left_fist.y = face.y + 50;
                left_fist.rotation = 100/360f;
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
