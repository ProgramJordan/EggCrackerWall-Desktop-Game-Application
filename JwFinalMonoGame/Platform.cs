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
    public class Platform
    {
        private Texture2D imgPlatform { get; set; }  //allow paddle to be shown //image taken from components
        private SpriteBatch spriteBatch;  

        //needed for Ball/actionScene.cs
        public float X { get; set; } //platform position
        public float Y { get; set; } //platform position

        //Both in Pixels to keep paddle in bounds
        public float Width { get; set; } //platform
        public float Height { get; set; } //platform
        public float ScreenWidth { get; set; } //game screen


        public Platform(float x, float y, float screenWidth, SpriteBatch spriteBatch, GameComponents gameComponents)
        {
            X = x;
            Y = y;
            imgPlatform = gameComponents.imgPlatform;
            Height = imgPlatform.Height;
            Width = imgPlatform.Width;
            ScreenWidth = screenWidth;
            this.spriteBatch = spriteBatch;
        }

        public void Draw()
        {
            //draw img - IMG/POSITION/NULL=FULL IMAGE/Transparent/no rotation/orgin/scale/effects/singleimage
            spriteBatch.Draw(imgPlatform, new Vector2(X, Y), null, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
        }

        public void MoveTo(float x)
        {
            if (x >= 0)
            {
                if (x < ScreenWidth - Width)
                {
                    X = x;
                }
                else
                {
                    X = ScreenWidth - Width;
                }
            }
            else
            {
                if (x < 0)
                {
                    X = 0;
                }
            }
        }
        //  -5 & +5 to keep inbounds
        public void MoveRight()
        {
            X = X + 5;
            if ((X + Width) > ScreenWidth)
            {
                X = ScreenWidth - Width;
            }
        }

        public void MoveLeft()
        {
            X = X - 5;

            if (X < 1)
            {
                X = 1;
            }
        }

    }
}
