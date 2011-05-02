using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace wing_ding_pong
{
    public class Score : IDrawable
    {
        private IList<Player> _players;
        //private Texture2D _sprite;
        SpriteFont _font ;

        public Score(SpriteFont font, IList<Player> players)
        {
            _font = font;
            _players = players;
        }

        public IList<Player> Players
        {
            get { return _players; }
        }
        
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            StringBuilder scoreStrBuilder = new StringBuilder();
            foreach (Player player in this.Players)
            {
                scoreStrBuilder.Append(String.Format("Player {0}: {1} -- ",
                    (int)player.Player_Index + 1, player.Score));
            }
            scoreStrBuilder.Remove(scoreStrBuilder.Length - 4, 3);
            spriteBatch.DrawString( // draw our score string
                   _font, // Score font.
                   scoreStrBuilder.ToString(), // Build the string.
                   new Vector2( // Text position.
                    GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 5,
                   10.0f),
                   Color.Yellow); // Text color.

        }
    }
}
