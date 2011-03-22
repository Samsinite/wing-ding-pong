using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace wing_ding_pong
{
    interface IDrawable
    {
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
