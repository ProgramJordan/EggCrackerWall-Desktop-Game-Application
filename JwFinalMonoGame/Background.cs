using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace JwFinalMonoGame
{
    public class Background : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex2d;
        private Vector2 position;

        public Background(Game game,SpriteBatch spriteBatch,
            Texture2D tex2d) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.tex2d = tex2d;
            this.position = new Vector2(0, 0);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex2d, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
