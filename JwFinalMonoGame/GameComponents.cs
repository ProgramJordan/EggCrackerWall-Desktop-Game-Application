using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace JwFinalMonoGame
{
    public class GameComponents
    {
        public SpriteFont labelFont { get; set; }

        public Song sClip { get; set; }
        public Song sClipg { get; set; }

        public Texture2D imgBreakableWall { get; set; }
        public Texture2D imgPlatform { get; set; }
        public Texture2D imgBall { get; set; }
        public Texture2D imgPixel { get; set; }

        public SoundEffect startSound { get; set; }
        public SoundEffect missSound { get; set; }
        public SoundEffect breakSound { get; set; } 
        public SoundEffect platformSound { get; set; }
        //public Song platformSound { get; set; }
        public SoundEffect wallBounceSound { get; set; }

        public GameComponents(ContentManager Content)
        {
            //Content fonts
            labelFont = Content.Load<SpriteFont>("Arial22");
            //Music Sounds
            sClip = Content.Load<Song>("Images/ImgSounds/sClips");
            sClipg = Content.Load<Song>("Images/ImgSounds/menuSound");
            //Content images
            imgBall = Content.Load<Texture2D>("Images/Easter");
            imgPixel = Content.Load<Texture2D>("Images/pixel");
            imgPlatform = Content.Load<Texture2D>("Images/platforms");
            imgBreakableWall = Content.Load<Texture2D>("Images/breakableWall");
            //Game sounds
            startSound = Content.Load<SoundEffect>("Images/ImgSounds/PlatformBounceSounds");
            breakSound = Content.Load<SoundEffect>("Images/ImgSounds/BreakableWallSounds");
            platformSound = Content.Load<SoundEffect>("Images/ImgSounds/PlatformBounceSoundz");
            wallBounceSound = Content.Load<SoundEffect>("Images/ImgSounds/WallBounceSound");
            missSound = Content.Load<SoundEffect>("Images/ImgSounds/MissSound");
        }
    }
}
