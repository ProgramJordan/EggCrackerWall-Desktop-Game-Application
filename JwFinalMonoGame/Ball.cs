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
    public class Ball
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float XVelocity { get; set; }
        public float YVelocity { get; set; }
        public float Rotation { get; set; }
        public bool UseRotation { get; set; }
        //
        public bool Visible { get; set; }  //is ball visible on screen
        public int Score { get; set; }
        public int bricksCleared { get; set; } //number of bricks cleared current level
        //
        public float Height { get; set; }
        public float Width { get; set; }
        public float ScreenWidth { get; set; } //width of game screen
        public float ScreenHeight { get; set; } //height of game screen
        //
        private Texture2D imgBall { get; set; }
        private SpriteBatch spriteBatch;  
        private GameComponents gameComponents;

        public Ball(float screenWidth, float screenHeight, SpriteBatch spriteBatch, GameComponents gameComponents)
        {
            XVelocity = 0;
            YVelocity = 0;
            Rotation = 0;
            X = 0;
            Y = 0;
            imgBall = gameComponents.imgBall;
            Width = imgBall.Width;
            Height = imgBall.Height;
            this.spriteBatch = spriteBatch;
            this.gameComponents = gameComponents;
            ScreenHeight = screenHeight;
            ScreenWidth = screenWidth;
            Visible = false;
            Score = 0;
            bricksCleared = 0;
            UseRotation = true;
        }

        public void Draw()
            //making ball spin
        {
            if (Visible == false)
            {
                return;
            }
            if (UseRotation)
            {
                //spin direction/speed
                Rotation += .13f;
                if (Rotation > 4 * Math.PI)
                {
                    Rotation = 0;
                }
            }
            spriteBatch.Draw(imgBall, new Vector2(X, Y), null, Color.White, Rotation, 
                new Vector2(Width / 2, Height / 2), 1.0f, SpriteEffects.None, 0);
        }

        public bool Move(Wall wall, Platform platform)
        {
            if (Visible == false)
            {
                return false;
            }
            X = X + XVelocity;
            Y = Y + YVelocity;

            //check for wall hits
            if (X < 1)
            {
                X = 1;
                XVelocity = XVelocity * -1;
                PlaySound(gameComponents.wallBounceSound);
            }
            if (X > ScreenWidth - Width + 5)
            {
                X = ScreenWidth - Width + 5;
                XVelocity = XVelocity * -1;
                PlaySound(gameComponents.wallBounceSound);
            }
            if (Y < 1)
            {
                Y = 1;
                YVelocity = YVelocity * -1;
                PlaySound(gameComponents.wallBounceSound);
            }
            if (Y + Height > ScreenHeight)
            {
                Visible = false;
                Y = 0;
                PlaySound(gameComponents.missSound);
                return false;
            }
            //check for platform collision
            //platform = 70 pixels. divide to determine the angle of the bounce

            Rectangle platformRect = new Rectangle((int)platform.X, (int)platform.Y, (int)platform.Width, (int)platform.Height);
            Rectangle ballRect = new Rectangle((int)X, (int)Y, (int)Width, (int)Height);

            //X position determines direction and angle of deflection of the egg 
            //if ball lands on left or right side it will return in the direction on the same side 
            // closer hits to the  edge will make a sharper deflection. 
            if (HitTest(platformRect, ballRect))
            {
                PlaySound(gameComponents.platformSound);
                int offset = Convert.ToInt32((platform.Width - (platform.X + platform.Width - X + Width / 2)));
                offset = offset / 5;
                if (offset < 0)
                {
                    offset = 0;
                }
                switch (offset)
                {
                    // left edge
                    case 0:
                        XVelocity = -6;
                        break;
                    case 1:
                        XVelocity = -5;
                        break;
                    case 2:
                        XVelocity = -4;
                        break;
                    case 3:
                        XVelocity = -3;
                        break;
                    case 4:
                        XVelocity = -2;
                        break;
                    case 5:
                        XVelocity = -1;
                        break;
                        //center
                    case 8:
                        XVelocity = 3;
                        break;
                    case 9:
                        XVelocity = 4;
                        break;
                    case 10:
                        XVelocity = 5;
                        break;
                    default:
                        XVelocity = 6;
                        break;
                       //right edge
                }
                YVelocity = YVelocity * -1;
                Y = platform.Y - Height + 1;
                return true;
            }

            bool hitBrick = false;
            //collisions
            for (int i = 0; i < 5; i++)
            {
                if (hitBrick == false)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        BreakableWall brick = wall.BreakableWall[i, j];
                        if (brick.Visible)
                        {
                            Rectangle brickRect = new Rectangle((int)brick.X, (int)brick.Y, (int)brick.Width, (int)brick.Height);
                            if (HitTest(ballRect, brickRect))
                            {
                                PlaySound(gameComponents.breakSound);
                                brick.Visible = false;
                                Score = Score + 5 - i;
                                YVelocity = YVelocity * -1;
                                bricksCleared++;
                                hitBrick = true;
                                break;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void Launch(float x, float y, float xVelocity, float yVelocity)
        {
            if (Visible == true)
            {
                return;  //ball already exists, ignore 
            }
            Visible = true;
            //add ball sound here + to interactions
            PlaySound(gameComponents.startSound);
            X = x;
            Y = y;
            XVelocity = xVelocity;
            YVelocity = yVelocity;
        }

        public static void PlaySound(SoundEffect sound)
        {
            //right/left speakerr same sounds
            float pan = 0.0f;
            //0 (silent) to 1 (full volume)
            float volume = 0.1f;
            // pitch of the sound effect add or - 1 to raise/lower
            float pitch = 0.0f;
            sound.Play(volume, pitch, pan);
        }

        public static bool HitTest(Rectangle r1, Rectangle r2)
        {
            if (Rectangle.Intersect(r1, r2) != Rectangle.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
