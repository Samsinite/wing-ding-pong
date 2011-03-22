using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace wing_ding_pong
{
	class ControllerDetectScreen : Screen
	{
		// Background texture for the screen.
		Texture2D mControllerDetectScreenBackground;

		public ControllerDetectScreen(ContentManager theContent, EventHandler theScreenEvent) : base(theScreenEvent)
		{
			//Load the background texture for the screen.
			mControllerDetectScreenBackground = theContent.Load<Texture2D>(@"Screens/ControllerDetectScreen");
		}

		// Update all of the elements that need updating in the Controller Detect Screen.
		public override void Update(GameTime theTime)
		{
			// Poll all the gamepads (and the keyboard) to check to see
			// which controller will be the player one controller. When the controlling
			// controller is detected, call the screen event associated with this screen.
			for (int aPlayer = 0; aPlayer < 4; aPlayer++)
			{
				if (GamePad.GetState((PlayerIndex)aPlayer).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.A) == true)
				{
					PlayerOne = (PlayerIndex)aPlayer;
					ScreenEvent.Invoke(this, new EventArgs());

					return;
				}
			}

			base.Update(theTime);
		}

		// Draw all of the elements that make up the Controller Detect Screen.
		public override void Draw(SpriteBatch theBatch)
		{
			theBatch.Draw(mControllerDetectScreenBackground, Vector2.Zero, Color.White);
			base.Draw(theBatch);
		}



	}
}
