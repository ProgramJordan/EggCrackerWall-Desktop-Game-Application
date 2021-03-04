using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JwFinalMonoGame
{
    public class BreakableWall
    {
        public float X { get; set; } //x position of brick on screen
        public float Y { get; set; } //y position of brick on screen

        public bool Visible { get; set; } //brick still visable?
        public float Width { get; set; } //brick WIDTH
        public float Height { get; set; } //brick height

        private Color color;
        private Texture2D imgBreakableWall { get; set; }  //grab image of imgBreakableWall from components
        private SpriteBatch spriteBatch;  //allows write to backbuffer to draw self
                                          // gameComponents = game1 -> GameComponents
        public BreakableWall(float x, float y, Color color, SpriteBatch spriteBatch, GameComponents gameComponents)
        {
            X = x;
            Y = y;
            imgBreakableWall = gameComponents.imgBreakableWall;
            Height = imgBreakableWall.Height;
            Width = imgBreakableWall.Width;
            this.spriteBatch = spriteBatch;
            Visible = true;
            this.color = color;
        }

        public void Draw()
        {
            if (Visible)
            {
                //null srcrec
                spriteBatch.Draw(imgBreakableWall, new Vector2(X, Y), null, color, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
            }
        }

    }
}
