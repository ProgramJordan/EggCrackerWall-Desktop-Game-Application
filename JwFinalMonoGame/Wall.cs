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
    public class Wall
    {
        //5 rows, each own color
        //10 breakable walls per row
        //3 extra blank  at top
        //breakblewall = 50px x 16px
        public BreakableWall[,] BreakableWall { get; set; }
        // gameComponents = game1 -> GameComponents
        //use Wall constructor to incrment the layout of walls in 5x10 grid
        public Wall(float x, float y, SpriteBatch spriteBatch, GameComponents gameComponents)
        {
            //multi array 5 rows down 10  row across
            BreakableWall = new BreakableWall[5, 10];
            float breakableX = x;
            float breakableY = y;
            Color color = new Color();
            //5 rows of bricks. set colors
            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 0:
                        color = Color.MidnightBlue;
                        break;
                    case 1:
                        color = Color.DarkBlue;
                        break;
                    case 2:
                        color = Color.MediumBlue;
                        break;
                    case 3:
                        color = Color.Blue;
                        break;
                    case 4:
                        color = Color.DodgerBlue;
                        break;
                    case 5:
                        color = Color.SteelBlue;
                        break;
                }
                breakableY = y + i * (gameComponents.imgBreakableWall.Height + 1);
                //for loop to spawn bricks using array created + colors set.
                for (int j = 0; j < 10; j++)
                {
                    breakableX = x + j * (gameComponents.imgBreakableWall.Width);
                    BreakableWall Breakable = new BreakableWall(breakableX, breakableY, color, spriteBatch, gameComponents);
                    BreakableWall[i, j] = Breakable;
                }
            }
        }
        public void Draw()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    BreakableWall[i, j].Draw();
                }
            }
        }
    }
}
