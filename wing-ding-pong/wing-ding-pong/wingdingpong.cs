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
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
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
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit by pressing the "back" button on the
			// Xbox controller, OR by pressed the "escape" key on the keyboard.
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			{
				this.Exit();
			}

			// Handling keyboard inputs.

			// Blue bar ("E" for up, "D" for down).
			if (Keyboard.GetState().IsKeyDown(Keys.E)) // E key down ?
			{
				blueBar.Y -= 10;		// Move the blue bar up.
			}
			else if (Keyboard.GetState().IsKeyDown(Keys.D)) // D key down ?
			{
				blueBar.Y += 10;	// Move the blue bar down.
			}

			// Red bar (Keyboard up for up, keyboard down for down).
			if (Keyboard.GetState().IsKeyDown(Keys.Up)) // Up key down ?
			{
				redBar.Y -= 10;		// Move the red bar up.
			}
			else if (Keyboard.GetState().IsKeyDown(Keys.Down)) // Down key down ?
			{
				redBar.Y += 10;		// Move the red bar down.
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

			// Handling ball initialization; use spacebar to reset.
			if (Keyboard.GetState().IsKeyDown(Keys.Space))
			{
				InitBall();
			}

			// Collision handling.

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
				ballVelocity.X = -ballVelocity.X; // make if bounce by inverting the X velocity
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
				  font, // our score font
				  blueScore.ToString() + " - " + redScore.ToString(), // building the string
				  new Vector2( // text position
				  GraphicsDevice.Viewport.Bounds.Width / 2 - 25, // half the screen and a little to the left
				  10.0f),
				  Color.Yellow); // yellow text

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
    }
}
