using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JwFinalMonoGame
{
    public class Border
    {
        private Texture2D imgPixel { get; set; }  //pixel image used to draw border
        private SpriteBatch spriteBatch;

        public float Width { get; set; }
        public float Height { get; set; }

        // gameComponents = game1 -> GameComponents
        //use own components not drawable game
        public Border(float screenWidth, float screenHeight, SpriteBatch spriteBatch, GameComponents gameComponents)
        {
            //use pixel component for border
            Width = screenWidth;
            Height = screenHeight;
            imgPixel = gameComponents.imgPixel;
            this.spriteBatch = spriteBatch;
        }

        public void Draw()
        {
            //draw  single-pixel wide border line 
            //bottom empty for ball fall through- 
            //starttop left span across
            spriteBatch.Draw(imgPixel, new Rectangle(0, 0, (int)Width - 1, 1), Color.White); //top 
            spriteBatch.Draw(imgPixel, new Rectangle(0, 0, 1, (int)Height - 1), Color.White); //left 
            spriteBatch.Draw(imgPixel, new Rectangle((int)Width - 1, 0, 1, (int)Height - 1), Color.White);//right

        }
    }
}
