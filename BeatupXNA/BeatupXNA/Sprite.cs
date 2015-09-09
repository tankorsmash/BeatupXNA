using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ThirdPartyNinjas.XnaUtility;

namespace BeatupXNA
{
    public class Sprite : Node
    {
        private Beatup game;
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

        public Sprite (Beatup game, string atlas_name, string sheet_name, string sprite_name) 
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
}