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
using wing_ding_pong.GameStateManagement;

namespace wing_ding_pong
{
    /// <summary>
    ///		Main game type.
	///		
	///		Abstracted out a "show" method for later use if
	///		we want it to check things.
    /// </summary>
    public class wingdingpong : Microsoft.Xna.Framework.Game
    {
		#region ClassMemberData

		static readonly string[] preloadAssets =
        {
            "gradient",
        };

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		SpriteFont font;

        // Scores.
        Score gameScore;

        TimeSpan dTime;
        _2D.Vector ballVector;
        _2D.Speed ballSpeed;
        ArenaWall leftWall, rightWall, topWall, bottomWall; // Creating the walls.
        Ball ball;
        Paddle paddle1, paddle2;

		// Clone textures.
		Texture2D grass;		// Texture used for whatever.
		Texture2D wallTexture;	// Texture used for walls.
		Texture2D ballTexture;	// Texture used for the ball.
		Texture2D spriteSheet;	// General texture sheet.
        _2D.Point center = new _2D.Point(375.0, 225.0);

		// Sound effects.
		SoundEffect ballBounce;
		SoundEffect playerScored;
        CollidableObjects.Rectangle lWallRect, rWallRect,tWallRect,bWallRect;
        CollidableObjects.Circle ballCircle;
        CollidableObjects.Rectangle pad1Rect, pad2Rect;


        // Screens.
        //ControllerDetectScreen mControllerScreen;
        //TitleScreen mTitleScreen;
        ScreenManager screenManager;

		#endregion

		#region Constructor

		public wingdingpong()
        {
			#if WINDOWS
            GraphicsAdapter.UseReferenceDevice = true;  //Requires DirectX SDK and enables software based DirectX (Slow)
			#endif

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

			// Ideal resolution for the XBox 360.
			this.graphics.PreferredBackBufferWidth = 1360;
			this.graphics.PreferredBackBufferHeight = 768;
			
			// Create the screen manager component.
			screenManager = new ScreenManager(this);

			Components.Add(screenManager);

			// Activate the first screens.
			screenManager.AddScreen(new BackgroundScreen(), null);
			screenManager.AddScreen(new MainMenuScreen(), null);
		}
        //CollidableObjects.Rectangle wallRect = new CollidableObjects.Rectangle(1, GraphicsDevice.Viewport.Bounds.Height / 2, 80.0, 0.0);
		#endregion

		#region LoadContent

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			foreach (string asset in preloadAssets)
			{
				Content.Load<object>(asset);
			}

			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load textures from the Content Pipeline
			grass = Content.Load<Texture2D>(@"Textures/Funky");
			wallTexture = Content.Load<Texture2D>(@"Textures/stonewall");
			ballTexture = Content.Load<Texture2D>(@"Textures/Ball1");
			spriteSheet = Content.Load<Texture2D>(@"Textures/Objects");

			// Load sound effects from the Content Pipeline
			ballBounce = Content.Load<SoundEffect>(@"Sounds/Bounce");
			playerScored = Content.Load<SoundEffect>(@"Sounds/Supporters");

			// Load our score font.
			font = Content.Load<SpriteFont>(@"ScoreFont");

			// Initialize screens.
			//mControllerScreen = new ControllerDetectScreen(this.Content, new EventHandler(ControllerDetectScreenEvent));
			//mTitleScreen = new TitleScreen(this.Content, new EventHandler(TitleScreenEvent));
            
			// Current screen.
			//mCurrentScreen = mControllerScreen;

			// Walls for the arena.
            leftWall = new ArenaWall(wallTexture, lWallRect);
            rightWall = new ArenaWall(wallTexture, rWallRect);
            topWall = new ArenaWall(wallTexture, tWallRect);
            bottomWall = new ArenaWall(wallTexture, bWallRect);
            paddle1 = new Paddle(grass, pad1Rect);
            paddle2 = new Paddle(grass, pad2Rect);
            ball = new Ball(ballTexture, center);
            InitBall();

            gameScore = new Score(font);
            gameScore.RightScore = 0;
            gameScore.LeftScore = 0;
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
			#if WINDOWS
            lWallRect = new CollidableObjects.Rectangle(0.0, 0.0, 5.0, 
			(double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            rWallRect = new CollidableObjects.Rectangle(794.0, 0.0, 5.0, 
			(double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            tWallRect = new CollidableObjects.Rectangle(0.0, 0.0,
			(double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, 5.0);

            bWallRect = new CollidableObjects.Rectangle(0.0, 594.0,
			(double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, 5.0);

            pad1Rect = new CollidableObjects.Rectangle(40.0, 
			(double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height-900, 40.0, 150.0);

            pad2Rect = new CollidableObjects.Rectangle(724.0, 
			(double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height-900, 40.0, 150.0);
			#endif

            ballCircle = new CollidableObjects.Circle(center, 5.0);
            
            //ballCircle = new CollidableObjects.Circle(new _2D.Point(300.0, 300.0), 70);
            //blueBar = new Rectangle(
            //      32, // "X" coordinate of the upper left corner of our rectangle
            //      GraphicsDevice.Viewport.Bounds.Height / 2 - 64, // "Y" coordinate of the upper left corner
            //      32, // Width
            //      128); // Height
            //redBar = new Rectangle(
            //      GraphicsDevice.Viewport.Bounds.Width - 64, // "X" coordinate of the upper left corner of our rectangle
            //      GraphicsDevice.Viewport.Bounds.Height / 2 - 64, // "Y" coordinate of the upper left corner
            //      32, // Width
            //      128); // Height
            //ball = new Rectangle(
            //      GraphicsDevice.Viewport.Bounds.Width / 2 - 16, // "X" coordinate of the upper left corner of our rectangle
            //      GraphicsDevice.Viewport.Bounds.Height / 2 - 16, // "Y" coordinate of the upper left corner
            //      32, // Width
            //      32); // Height
			
			base.Initialize();
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
            //// Check that both controllers are connected to the game.
            //// If one or more is not, pause the game.
            //if ((GamePad.GetState(PlayerIndex.One).IsConnected == false) 
            //    || (GamePad.GetState(PlayerIndex.Two).IsConnected == false))
            //{
            //    // Pause the game.
            //}

            //// Allows the game to exit by pressing the "back" button on the Xbox controller.
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            //    Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
            //{
            //    this.Exit();
            //}

            // By taking advantage of Polymorphism, we can call update on the current screen class, 
            // but the Update in the subclass is the one that will be executed.
            //mCurrentScreen.Update(gameTime);

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
                || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y >= 0.5f ||
                Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up))
            {
                paddle1.Y -= 10;
            }
            else if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y <= -0.5f ||
                Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
            {
                paddle1.Y += 10;
            }

            // Player two controls (red).
            if (GamePad.GetState(PlayerIndex.Two).DPad.Up == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y >= 0.5f ||
                Keyboard.GetState(PlayerIndex.Two).IsKeyDown(Keys.Up))
            {
                paddle2.Y -= 10;
            }
            else if (GamePad.GetState(PlayerIndex.Two).DPad.Down == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y <= -0.5f ||
                Keyboard.GetState(PlayerIndex.Two).IsKeyDown(Keys.Down))
            {
                paddle2.Y += 10;
            }

            // Limit the bars' movement to the screen bounds.
            if (paddle2.Y < 0) // Upper bound.
            {
                paddle2.Y = 0; // Limit.
            }

            if (paddle1.Y < 0)
            {
                paddle1.Y = 0;
            }

            if (paddle2.Y + paddle2.Height > GraphicsDevice.Viewport.Bounds.Height)
            {
                paddle2.Y = GraphicsDevice.Viewport.Bounds.Height - paddle2.Height;
            }

            if (paddle1.Y + paddle1.Height > GraphicsDevice.Viewport.Bounds.Height)
            {
                paddle1.Y = GraphicsDevice.Viewport.Bounds.Height - paddle1.Height;
            }

            //ballVector = new _2D.Vector(-5.0, 5.0);

            // Move the ball.
            ball.Update(gameTime);

            // Handling ball initialization; use Navigation Button to reset.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed ||
                Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))
            {
                InitBall();
            }

            // Collision handling. //
            // Wall collisions.
            if (ball.Y < 0.0 || // if the ball reach the upper bound of the screen
                  ball.Y + ball.Radius > 750.0) // or the lower one
            {
                ballVector.X = -ballVector.X; // make if bounce by inverting the Y velocity
                ballSpeed = new _2D.Speed(ballVector, dTime);
               // ballBounce.Play(); // Bounce sound.
            }

            // Bar collisions.
            if((ball.X == paddle1.X) || (ball.X == paddle1.Y))
            {
                ballVector.X = -ballVector.X; // Make it bounce by inverting the X velocity.
               // ballBounce.Play(); // Bounce sound.

            }

            // Scoring.
            if (ball.X < 0) // Red scores a point.
            {
                gameScore.RightScore++;
                //playerScored.Play(); // Play a sound when a point is scored.
                InitBall(); // Re-init the ball.
            }
            else if (ball.X + ball.Radius > GraphicsDevice.Viewport.Bounds.Width) // Blue scores a point.
            {
                gameScore.LeftScore++;
                //playerScored.Play();
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
		
			// This code draws the menus.
			//spriteBatch.Begin();

			//// Again, using Polymorphism, we can call draw on the current screen class
			//// and the Draw in the subclass is the one that will be executed.
			//mCurrentScreen.Draw(spriteBatch);

			//spriteBatch.End();

            // Uncomment this section to render the background image.
            // Grass background.
            //spriteBatch.Begin();
            //spriteBatch.Draw(
            //      grass, // Grass texture.
            //      GraphicsDevice.Viewport.Bounds, // Stretch the texture to the whole screen.
            //    // GraphicsDevice.Viewport.Bounds is Rectangle corresponding to the actual viewport (meaning the entire screen no matter the resolution), only available as of XNA 4.0
            //      Color.White);
            //spriteBatch.End();
            
			//leftWall = new ArenaWall(grass, lWallRect);
            //rightWall = new ArenaWall(grass, rWallRect);
            //topWall = new ArenaWall(grass, tWallRect);
            //bottomWall = new ArenaWall(grass, bWallRect);
            //paddle1 = new Paddle(grass, pad1Rect);
            //paddle2 = new Paddle(grass, pad2Rect);

            //ball = new Ball(grass, _2D.Point(60.0,60.0));
            //spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            //leftWall.Draw(gameTime, spriteBatch);//-------testing wall draw
            ///*****************************/
            //spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            
             /***************************************/
			//rightWall.Draw(gameTime, spriteBatch);
			//leftWall.Draw(gameTime, spriteBatch);
			//topWall.Draw(gameTime, spriteBatch);
			//bottomWall.Draw(gameTime, spriteBatch);
            paddle1.Draw(gameTime, spriteBatch);
            paddle2.Draw(gameTime, spriteBatch);
            ball.Draw(gameTime, spriteBatch);
            gameScore.Draw(gameTime, spriteBatch);
            /*****************************/
            spriteBatch.End();

            

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
            ballVector = new _2D.Vector(5.0, 100.0);
            dTime = new TimeSpan(100000);
            
            //// Randomize the ball orientation.
            switch (rand.Next(4))
            {
                case 0: 
                    ballSpeed = new _2D.Speed(ballVector, dTime);
                    ballVector.X = speed;
                    ballVector.Y = speed;
                    ball.BallSpeed = ballSpeed;
                    break;
                case 1: 
                    ballVector.X = -speed; 
                    ballVector.Y = speed;
                    ballSpeed = new _2D.Speed(ballVector, dTime);
                    ball.BallSpeed = ballSpeed;
                    break;
                case 2: 
                    ballVector.X = speed; 
                    ballVector.Y = -speed;
                    ballSpeed = new _2D.Speed(ballVector, dTime);
                    ball.BallSpeed = ballSpeed;
                    break;
                case 3: 
                    ballVector.X = -speed;
                    ballVector.Y = -speed;
                    ballSpeed = new _2D.Speed(ballVector, dTime);
                    ball.BallSpeed = ballSpeed;
                    break;
            }

            //// Initialize the ball to the center of the screen.
            ball.X = GraphicsDevice.Viewport.Bounds.Width / 2 - ball.Radius / 2;
            ball.Y = GraphicsDevice.Viewport.Bounds.Height / 2 - ball.Radius / 2;
		}	// End "initball".

		#endregion

		#region ScreenEvents

		//This event fires when the Controller detect screen is returning control back to the main game class
		//public void ControllerDetectScreenEvent(object obj, EventArgs e)
		//{
		//    //Switch to the title screen, the Controller detect screen is finished being displayed
		//    mCurrentScreen = mTitleScreen;
		//}

		//This event is fired when the Title screen is returning control back to the main game class
		//public void TitleScreenEvent(object obj, EventArgs e)
		//{
		//    //Switch to the controller detect screen, the Title screen is finished being displayed
		//    mCurrentScreen = mControllerScreen;
		//}

		#endregion
      
		#region Show

		public void show()
		{
			// Print stuff out here, such as teams
			// and whatever. Not sure if it'll be
			// useful, but I'll just drop this here
			// anyway.
		}

		#endregion

	}	// End "wingdingpong".
}
