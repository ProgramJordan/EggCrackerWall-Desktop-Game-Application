/*
 * Jordan G. WEADICK
 * Section 3 Game Programming with DATA scructures 
 * Nov 30th 2018
 * JweadickFinalAssignment - JwFinalMono
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace JwFinalMonoGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //components class object reference
        private GameComponents gameComponents;
        // scenes
        private StartScene startScene;
        private ActionScene actionScene;
        private HelpScene helpScene;
        //private LeaderBoards leaderboards;
        private Background background;
        private AboutScene aboutScene;
       
        // objects
        private Platform platform;
        private Wall wall;
        private Border border;
        private Ball ball;
        private Ball staticBall;
        //sounds
        //private Song sClips;
        //private Song sClipg;
        //
        //declared components end
        private int screenWidth = 0;
        private int screenHeight = 0;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            //// TODO: Add your initialization logic here
            
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
            System.Console.WriteLine(Shared.stage.X + " " + Shared.stage.Y);

            base.Initialize();
        }
        protected override void LoadContent()
        {
            // SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
           
            Texture2D tex2d = this.Content.Load<Texture2D>("Images/night-background_PNG");
            background = new Background(this, spriteBatch, tex2d);
            this.Components.Add(background);

            startScene = new StartScene(this, spriteBatch);
            this.Components.Add(startScene);

            helpScene = new HelpScene(this, spriteBatch);
            this.Components.Add(helpScene);
            //
            aboutScene = new AboutScene(this, spriteBatch);
            this.Components.Add(aboutScene);

            gameComponents = new GameComponents(Content);
            
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //set game to 502x700 or screen max if smaller
            //500 + 2 for 1px borders
            //bricks 50px ea
            if (screenWidth >= 502)
            {
                screenWidth = 502;
            }
            if (screenHeight >= 700)
            {
                screenHeight = 700;
            }
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();

            //// game objects platform position
            int platformX = (screenWidth - gameComponents.imgPlatform.Width) / 2; //center paddle on the screen to start
            int platformY = screenHeight - 75;  //paddle will be 75 pixels from the bottom of the screen

            ////instantiated objects
            platform = new Platform(platformX, platformY, screenWidth, spriteBatch, gameComponents);  // create the game platform
            wall = new Wall(1, 50, spriteBatch, gameComponents);
            border = new Border(screenWidth, screenHeight, spriteBatch, gameComponents);
            ball = new Ball(screenWidth, screenHeight, spriteBatch, gameComponents);
               
            // egg image location (502x700y)- to show balls left
            staticBall = new Ball(screenWidth, screenHeight, spriteBatch, gameComponents);
            staticBall.X = 465;
            staticBall.Y = 675;
            staticBall.Visible = true;
            staticBall.UseRotation = false;

            // added all item components to action scene
            actionScene = new ActionScene(this,spriteBatch, graphics, gameComponents, border, platform, wall, ball, staticBall);
            this.Components.Add(actionScene);
            //
            // instantiation ends
            startScene.show();
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            // TODO: Add your update logic here
            int selectedIndex = 0;

            KeyboardState ks = Keyboard.GetState();

            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;

                switch (selectedIndex)
                {
                    case 0:
                        if (ks.IsKeyDown(Keys.Enter))
                        {
                            MediaPlayer.IsRepeating = true;
                            //MediaPlayer.Play(sClips);
                            MediaPlayer.Volume = 0.07f;
                            MediaPlayer.Play(gameComponents.sClip);

                            actionScene.show();
                            startScene.hide();
                        }
                        break;
                    case 1:
                        if (ks.IsKeyDown(Keys.Enter))
                        {
                            helpScene.show();
                            startScene.hide();
                        }
                        break;
                    case 2:
                        if (ks.IsKeyDown(Keys.Enter))
                        {
                            aboutScene.show();
                            startScene.hide();
                        }
                        break;
                    case 3:
                        if (ks.IsKeyDown(Keys.Enter))
                        {
                            Exit();
                        }
                        break;

                    default:
                        break;
                }
            }
            if (actionScene.Enabled)
            {
                //change from action music to menu music (sClipg)
                if (ks.IsKeyDown(Keys.Escape))
                {
                    MediaPlayer.Play(gameComponents.sClipg);
                    MediaPlayer.Volume = 0.055f;

                    actionScene.hide();
                    startScene.show();
                }
            }
            //dont restart menu music after leave these scenes.
            if (helpScene.Enabled || aboutScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    helpScene.hide();
                    aboutScene.hide();
                    startScene.show();
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //repeat song if not playing. PLAY IT!!(sClipg)
            if (MediaPlayer.State != MediaState.Playing)
            {
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(gameComponents.sClipg);

                //MediaPlayer.Play(sClipg);
                MediaPlayer.Volume = 0.055f;
            }
            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
