/*
 *Jordan G. WEADICK
 * Section 3 Game Programming with DATA scructures 
 * Nov 30th 2018
 * JweadickFinalAssignment - JwFinalMono
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JwFinalMonoGame
{
    public class ActionScene : GameScene
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
       
        private GameComponents gameComponents;
        private Platform platform;
        private Wall wall;
        private Border border;
        private Ball ball;
        //
        private MouseState oldMouseState;
        private KeyboardState oldKeyboardState;
        //
        private Ball staticBall; // Remaining Visualizer
        private int ballsRemaining = 3;
        //
        private int screenWidth = 0;
        private int screenHeight = 0;
        //
        private bool readyToServeBall = true;
        private bool IsActive = true;
        
        public ActionScene(Game game,SpriteBatch spriteBatch, GraphicsDeviceManager graphics, GameComponents gameComponents, 
            Border border, Platform platform, Wall wall, Ball ball, Ball staticBall) : base(game)
        {
            this.gameComponents = gameComponents;
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;
            this.wall = wall;
            this.border = border;
            this.platform = platform;     
            this.staticBall = staticBall;
            this.ball = ball;
        }

        public override void Draw(GameTime gameTime)
        {
            //declare points message set screen postion + size
            string scoreMsg = "Score: " + ball.Score.ToString("0000");
            Vector2 space = gameComponents.labelFont.MeasureString(scoreMsg);

            spriteBatch.Begin();
            platform.Draw();
            border.Draw();
            wall.Draw();
            staticBall.Draw();
            spriteBatch.DrawString(gameComponents.labelFont, scoreMsg,
                new Vector2((screenWidth + space.X) / 3, screenHeight + 670), Color.White);
            //draw ball if ball visable/in play if not remove life/reset.
            if (ball.Visible)
            {
                bool inPlay = ball.Move(wall, platform);
                if (inPlay)
                {
                    ball.Draw();
                }
                else
                {
                    ballsRemaining--;
                    readyToServeBall = true;
                }
            }
           
            //check for game over 
            //hide ball,reset cleared/walls ready to play again
            if (ball.bricksCleared >= 50)
            {
                ball.Visible = false;
                ball.bricksCleared = 0;
                wall = new Wall(1, 50, spriteBatch, gameComponents);
                ball.Score = 0;
                readyToServeBall = true;
            }
            //--> to ready serve if 
            if (readyToServeBall)
            {
                if (ballsRemaining > 0)
                {
                    string startMsg = "Press [Space] or [Left Click] to Launch";
                    Vector2 startSpace = gameComponents.labelFont.MeasureString(startMsg);
                    spriteBatch.DrawString(gameComponents.labelFont, startMsg, new Vector2((screenWidth + startSpace.X) / 2, screenHeight / 2), Color.White);
                }
                else
                {
                    string endMsg = "          Out of Lives - Game-Over";
                    Vector2 endSpace = gameComponents.labelFont.MeasureString(endMsg);
                    spriteBatch.DrawString(gameComponents.labelFont, endMsg, new Vector2((screenWidth + endSpace.X) / 2, screenHeight / 2 ), Color.Red);
                }
            }
            //postion + show remaining balls in text. 502x700= pos screen
            spriteBatch.DrawString(gameComponents.labelFont, ballsRemaining.ToString(), new Vector2(445, 668), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (IsActive == false)
            {
                return;  //if our window is not active don't update; freeze game
               
            }
            KeyboardState newKeyboardState = Keyboard.GetState();
            MouseState newMouseState = Mouse.GetState();

            //remember previous click to know current click
            if (newMouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed && 
                oldMouseState.X == newMouseState.X && oldMouseState.Y == newMouseState.Y && readyToServeBall)
            {
                ServeBall();
            }

            // 1) process mouse move                                
            if (oldMouseState.X != newMouseState.X)
            {
                if (newMouseState.X >= 0 || newMouseState.X < screenWidth)
                {
                    platform.MoveTo(newMouseState.X);
                }
            }
          
            // 2) process keyboard events                           
            if (newKeyboardState.IsKeyDown(Keys.Left))
            {
                platform.MoveLeft();
            }
            if (newKeyboardState.IsKeyDown(Keys.Right))
            {
                platform.MoveRight();
            }
            if (oldKeyboardState.IsKeyUp(Keys.Space) && newKeyboardState.IsKeyDown(Keys.Space) && readyToServeBall)
            {
                ServeBall();
            }

            //this saves  old states
            oldMouseState = newMouseState;
            oldKeyboardState = newKeyboardState;
            //****need to have at least two data points from MonoGame, before we can detect a click event. 
            //Because of that; every time we get an Update event from MonoGame, 
            //we will need to remember the previous mouse and keyboard state.

            base.Update(gameTime);
        }

        private void ServeBall()
        {
            if (ballsRemaining < 1)
            {
                ballsRemaining = 3;
                ball.Score = 0;
                //X start at 1px. Y start 50px from top
                wall = new Wall(1, 50, spriteBatch, gameComponents);
            }
            readyToServeBall = false;
            //center ball
            float ballX = platform.X + (platform.Width) / 2;
            //shootball vertically from platform.
            float ballY = platform.Y - ball.Height;
            ball.Launch(ballX, ballY, 4, 4);
        }
    }
}
