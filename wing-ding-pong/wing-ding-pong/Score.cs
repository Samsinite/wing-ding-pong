using System;
using System.Collections.Generic;
using System.Linq;
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
        private Player _owner;
        private Texture2D _sprite;
        GraphicsDevice _graphicsDevice;
        SpriteFont _font ;
        int _rightScore;
        int _leftScore;

        public Score(SpriteFont font)
        {
            _font = font;
        }

        public Player Owner
        {
            set { _owner = value; }
            get { return _owner; }
        }
        
        public int RightScore
        {
            set { _rightScore = value; }
            get { return _rightScore; }
        }

        public int LeftScore
        {
            set { _leftScore = value; }
            get { return _leftScore; }
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
           //spriteBatch.DrawString( // draw our score string
           //       _font, // Score font.
           //       _leftScore.ToString() + " - " + _rightScore.ToString(), // Build the string.
           //       new Vector2( // Text position.
           //       _graphicsDevice.Viewport.Bounds.Width / 2 - 25, // Half the screen and a little to the left.
           //       10.0f),
           //       Color.Yellow); // Text color.

        }
    }
}
