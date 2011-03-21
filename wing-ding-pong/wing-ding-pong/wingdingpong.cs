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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class wingdingpong : Microsoft.Xna.Framework.Game
    {
       #region ClassMemberData

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont font;

		// Scores.
		int blueScore = 0;
		int redScore = 0;

		// Pong entities.
		Rectangle blueBar;
		Rectangle redBar;
		Rectangle ball; // Since there’s no "circle" class in XNA, simulate it with a bounding rectangle box.
		//Rectangle ball2;

		// Clone textures.
		Texture2D grass;
		Texture2D spriteSheet;

		// Sound effects.
		SoundEffect ballBounce;
		SoundEffect playerScored;

		// Ball speed.
		Vector2 ballVelocity = Vector2.Zero;
		//Vector2 ballVelocity2 = Vector2.Zero;

		// Source rectangles of our graphics.
		Rectangle blueSrcRect = new Rectangle( // Blue bar source rectangle.
			  0,	// Upper-left corner x-coordinate of the blue bar inside the spriteSheet
			  0,	// Upper-left corner y-coordinate
			  42,	// Width (original: 32)
			  115); // Height (original: 128)
		Rectangle redSrcRect = new Rectangle( // Red bar source rectangle.
			  42,	// Upper-left corner x-coordinate of the red bar inside the spriteSheet (original: 32)
			  0,
			  42,
			  115);
		Rectangle ballSrcRect = new Rectangle( // Ball source rectangle.
			  84,	// Upper-left corner x-coordinate of the ball inside the spriteSheet (original: 64)
			  0,
			  32,
			  32);

		#endregion

		#region wingdingpong

		public wingdingpong()
        {
#if WINDOWS
            GraphicsAdapter.UseReferenceDevice = true;  //Requires DirectX SDK and enables software based DirectX (Slow)
#endif
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

	    // Ideal resolution for the XBox 360.
	    this.graphics.PreferredBackBufferWidth = 1280;
	    this.graphics.PreferredBackBufferHeight = 720;
	}

		#endregion

		#region Initialize
		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			// initializing our entities
			blueBar = new Rectangle(
				  32, // "X" coordinate of the upper left corner of our rectangle
				  GraphicsDevice.Viewport.Bounds.Height / 2 - 64, // "Y" coordinate of the upper left corner
				  32, // Width
				  128); // Height
			redBar = new Rectangle(
				  GraphicsDevice.Viewport.Bounds.Width - 64, // "X" coordinate of the upper left corner of our rectangle
				  GraphicsDevice.Viewport.Bounds.Height / 2 - 64, // "Y" coordinate of the upper left corner
				  32, // Width
				  128); // Height
			ball = new Rectangle(
				  GraphicsDevice.Viewport.Bounds.Width / 2 - 16, // "X" coordinate of the upper left corner of our rectangle
				  GraphicsDevice.Viewport.Bounds.Height / 2 - 16, // "Y" coordinate of the upper left corner
				  32, // Width
				  32); // Height


			base.Initialize();
		}

		#endregion

		#region LoadContent

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load textures from the Content Pipeline
			grass = Content.Load<Texture2D>(@"Textures/Funky");
			spriteSheet = Content.Load<Texture2D>(@"Textures/Objects");

			// Load sound effects from the Content Pipeline
			ballBounce = Content.Load<SoundEffect>(@"Sounds/Bounce");
			playerScored = Content.Load<SoundEffect>(@"Sounds/Supporters");

			// Load our score font.
			font = Content.Load<SpriteFont>(@"ScoreFont");

		}

		#endregion

		#region UnloadContent

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		#endregion

		#region Update

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// 
		/// "Update" polls the system every clock tick.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Check that both controllers are connected to the game.
			// If one or more is not, pause the game.
			if ((GamePad.GetState(PlayerIndex.One).IsConnected == false) 
				|| (GamePad.GetState(PlayerIndex.Two).IsConnected == false))
			{
				// Pause the game.
			}

			// Allows the game to exit by pressing the "back" button on the Xbox controller.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
			{
				this.Exit();
			}
			
			// NOTE:
			// Consider adding half-values (blueBar.Y += 5) for slower paddle speed.
			// This can be done by checking that the stick is +/- .5f
			//
			// Also note:
			// Thumbstick movement is vector2 with range of +/- 1.0f on either
			// the X or Y axis of the stick being polled. 
			//
			// Update:
			// Triggers (left and right) use a range of 0.0f to 1.0f to determine
			// the pressure exerted on them; whether this is useful or not will
			// remain to be seen, but I'm including it here just in case.
			//
			// Player one controls (blue).
			if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed
				|| GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == 0.5f ||
                Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up))
			{
				blueBar.Y -= 10;
			}
			else if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed
				|| GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y == -0.5f ||
                Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
			{
				blueBar.Y += 10;
			}

			// Player two controls (red).
			if (GamePad.GetState(PlayerIndex.Two).DPad.Up == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y == 0.5f ||
                Keyboard.GetState(PlayerIndex.Two).IsKeyDown(Keys.Up))
			{
				redBar.Y -= 10;
			}
			else if (GamePad.GetState(PlayerIndex.Two).DPad.Down == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y == -0.5f ||
                Keyboard.GetState(PlayerIndex.Two).IsKeyDown(Keys.Down))
			{
				redBar.Y += 10;
			}
			
			// Limit the bars' movement to the screen bounds.
			if (redBar.Y < 0) // Upper bound.
			{
				redBar.Y = 0; // Limit.
			}

			if (blueBar.Y < 0)
			{
				blueBar.Y = 0;
			}

			if (redBar.Y + redBar.Height > GraphicsDevice.Viewport.Bounds.Height)
			{
				redBar.Y = GraphicsDevice.Viewport.Bounds.Height - redBar.Height;
			}

			if (blueBar.Y + blueBar.Height > GraphicsDevice.Viewport.Bounds.Height)
			{
				blueBar.Y = GraphicsDevice.Viewport.Bounds.Height - blueBar.Height;
			}

			// Move the ball.
			ball.X += (int)ballVelocity.X;
			ball.Y += (int)ballVelocity.Y;
			//ball2.X += (int)ballVelocity2.X;
			//ball2.Y += (int)ballVelocity2.Y;

			// Handling ball initialization; use Navigation Button to reset.
			if (GamePad.GetState(PlayerIndex.One).Buttons.BigButton == ButtonState.Pressed ||
                Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))
			{
				InitBall();
			}

			// Collision handling. //
			// Wall collisions.
			if (ball.Y < 0 || // if the ball reach the upper bound of the screen
				  ball.Y + ball.Height > GraphicsDevice.Viewport.Bounds.Height) // or the lower one
			{
				ballVelocity.Y = -ballVelocity.Y; // make if bounce by inverting the Y velocity
				ballBounce.Play(); // Bounce sound.
			}

			// Bar collisions.
			if (ball.Intersects(redBar) || ball.Intersects(blueBar))
			{
				ballVelocity.X = -ballVelocity.X; // Make it bounce by inverting the X velocity.
				ballBounce.Play(); // Bounce sound.

			}

			// Scoring.
			if (ball.X < 0) // Red scores a point.
			{
				redScore++;
				playerScored.Play(); // Play a sound when a point is scored.
				InitBall(); // Re-init the ball.
			}
			else if (ball.X + ball.Width > GraphicsDevice.Viewport.Bounds.Width) // Blue scores a point.
			{
				blueScore++;
				playerScored.Play();
				InitBall(); // Re-init the ball.
			}
			
			base.Update(gameTime);
		}	// End "update".

		#endregion

		#region Draw

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			// Uncomment this section to render the background image.
			// Grass background.
			spriteBatch.Begin();
			spriteBatch.Draw(
			      grass, // Grass texture.
			      GraphicsDevice.Viewport.Bounds, // Stretch the texture to the whole screen.
			    // GraphicsDevice.Viewport.Bounds is Rectangle corresponding to the actual viewport (meaning the entire screen no matter the resolution), only available as of XNA 4.0
			      Color.White);
			spriteBatch.End();

			// Draw the score.
			// The position of this code is important; if it were done
			// before the background is drawn, the background would
			// cover up the score. Due to this, we draw the score on top
			// of the background by putting this code after the background
			// code.
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

			spriteBatch.DrawString( // draw our score string
				  font, // Score font.
				  blueScore.ToString() + " - " + redScore.ToString(), // Build the string.
				  new Vector2( // Text position.
				  GraphicsDevice.Viewport.Bounds.Width / 2 - 25, // Half the screen and a little to the left.
				  10.0f),
				  Color.Yellow); // Text color.

			spriteBatch.End();
			
			// Draw the entities (bars and ball).
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend); // Setup alpha-blend to support transparency.

			// Draw the red bar.
			spriteBatch.Draw(
				  spriteSheet,	// Use the sprites texture.
				  redBar,		// The rectangle where to draw the bar on the screen.
				  redSrcRect,	// The source rectangle of the bar inside the sprite sheet.
				  Color.White);

			// Draw the blue bar.
			spriteBatch.Draw(spriteSheet, blueBar, blueSrcRect, Color.White);

			// Draw the ball.
			spriteBatch.Draw(spriteSheet, ball, ballSrcRect, Color.White);
			//spriteBatch.Draw(spriteSheet, ball2, ballSrcRect, Color.White);

			spriteBatch.End();

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}	// End "draw".

		#endregion

		#region InitBall

		/// <summary>
		///		Initializes the ball; called when the game starts and
		///		when points are scored.
		/// </summary>
		private void InitBall()
		{
			int speed = 5;	// Default velocity.
			Random rand = new Random();

			// Randomize the ball orientation.
			switch (rand.Next(4))
			{
				case 0: ballVelocity.X = speed; ballVelocity.Y = speed; break;
				case 1: ballVelocity.X = -speed; ballVelocity.Y = speed; break;
				case 2: ballVelocity.X = speed; ballVelocity.Y = -speed; break;
				case 3: ballVelocity.X = -speed; ballVelocity.Y = -speed; break;
			}

			// Initialize the ball to the center of the screen.
			ball.X = GraphicsDevice.Viewport.Bounds.Width / 2 - ball.Width / 2;
			ball.Y = GraphicsDevice.Viewport.Bounds.Height / 2 - ball.Height / 2;
		}	// End "initball".

		#endregion

    }	// End "wingdingpong".
}
