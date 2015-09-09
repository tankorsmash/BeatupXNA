using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeatupXNA
{
    public class Node
    {
        public Point position;

        public Vector2 rotation;
        public Vector2 scale;

        public bool is_visible;

        public List<Node> children;

        public void addChild(Node child)
        {
            children.Add(child);
        }
    }
    public class FaceStage
    {
        public Sprite NeutralSprite;
        public Sprite LeftSprite;
        public Sprite RightSprite;
        public Sprite HitSprite;

        public enum FaceStageType
        {
            Healthy,
            Hurt,
            Wounded,
            Dead
        }

        private FaceStageType type;

        public FaceStage(Beatup beatup, //FaceStageType type,
                string neutral_path, string left_path,
                string right_path, string hit_path)
        {
            this.NeutralSprite = new Sprite(beatup, "face_xml", "facepng", neutral_path);
            this.LeftSprite = new Sprite(beatup, "face_xml", "facepng", left_path);
            this.RightSprite = new Sprite(beatup, "face_xml", "facepng", right_path);

            this.HitSprite = new Sprite(beatup, "face_xml", "facepng", hit_path);
        }
    }

    public class Face : Node
    {
        private FaceStage healthy_stage;
        private FaceStage hurt_stage;
        private FaceStage wounded_stage;
        private FaceStage dead_stage;

        public Face (Beatup beatup)
        {
            this.healthy_stage = new FaceStage(beatup, "f_face_neutral.png", "f_look_left.png", "f_look_right.png", "f_gritted_teeth.png");
            this.hurt_stage = new FaceStage(beatup, "f_teeth.png", "f_look_left.png", "f_look_right.png", "f_teeth_side.png");
            this.wounded_stage = new FaceStage(beatup, "f_teeth_one.png", "f_look_left_teeth.png", "f_look_right_teeth.png", "f_teeth_side_one.png");
            this.dead_stage = new FaceStage(beatup, "f_face_neutral_eye.png", "f_face_neutral_eye.png", "f_look_left_eye.png", "f_look_right_eye.png");
        }

        //public void Draw
        public void Draw(SpriteBatch sb)
        {
            this.healthy_stage.NeutralSprite.position = this.position;
            this.healthy_stage.NeutralSprite.Draw(sb);
        }
    }

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Beatup : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera2d camera;

        private Face face;
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

            face = new Face(this);

            face.position.X = this.GraphicsDevice.Viewport.Width/2;
            face.position.Y = this.GraphicsDevice.Viewport.Height/2;

            left_fist = new Sprite(this, "face_xml", "facepng", "fist_neutral.png");
            left_fist.x = face.position.X - 200;
            left_fist.y = face.position.Y + 200;
            left_fist.flippedX = true;

            right_fist = new Sprite(this, "face_xml", "facepng", "fist_neutral.png");
            right_fist.x = face.position.X + 200;
            right_fist.y = face.position.Y + 200;

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
                left_fist.x = face.position.X - 100;
                left_fist.y = face.position.Y + 50;
                left_fist.rotation = 100/360f;
                int intensity = 5;
                ShakeCamera(intensity);
            }
            else
            {
                left_fist.x = face.position.X - 200;
                left_fist.y = face.position.Y + 200;
                left_fist.rotation = 0;
            }

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.E))
            {
                right_fist.x = face.position.X + 100;
                right_fist.y = face.position.Y + 50;
                right_fist.rotation = -100/360f;
                int intensity = 5;
                ShakeCamera(intensity);
            }
            else
            {
                right_fist.x = face.position.X + 200;
                right_fist.y = face.position.Y + 200;
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
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, 
                                    null, null, null, camera.get_transformation(graphics.GraphicsDevice));

            GraphicsDevice.Clear(Color.CornflowerBlue);

            left_fist.Draw(spriteBatch);
            right_fist.Draw(spriteBatch);
            face.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
