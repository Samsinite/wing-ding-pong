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

        IList<CollidableObjects.Collidable2DBase> _collidableObjects; //objects that can collide with each other will exist here
        IList<IDrawable> _drawObjects; //Anything drawn on the screen will exist here

		GraphicsDeviceManager _graphics;
		SpriteBatch _spriteBatch;
		SpriteFont _font;

        // Scores.
        Score _gameScore;

        TimeSpan _dTime;
        _2D.Vector _ballVector;
        _2D.Speed _ballSpeed;
        ArenaWall _leftWall, _rightWall, _topWall, _bottomWall; // Creating the walls.
        Ball _ball;
        Paddle _paddle1, _paddle2;

		// Clone textures.
		Texture2D _grass;		// Texture used for whatever.
		Texture2D _wallTexture;	// Texture used for walls.
		Texture2D _ballTexture;	// Texture used for the ball.
		Texture2D _spriteSheet;	// General texture sheet.
        _2D.Point _center = new _2D.Point(375.0, 225.0);

		// Sound effects.
		SoundEffect _ballBounce;
		SoundEffect _playerScored;
        CollidableObjects.Rectangle _lWallRect, _rWallRect, _tWallRect, _bWallRect;
        CollidableObjects.Circle _ballCircle;
        CollidableObjects.Rectangle _pad1Rect, _pad2Rect;


        // Screens.
        //ControllerDetectScreen mControllerScreen;
        //TitleScreen mTitleScreen;
        ScreenManager _screenManager;

		#endregion

		#region Constructor

		public wingdingpong()
        {
			#if WINDOWS
            GraphicsAdapter.UseReferenceDevice = true;  //Requires DirectX SDK and enables software based DirectX (Slow)
			#endif

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

			// Ideal resolution for the XBox 360.
			this._graphics.PreferredBackBufferWidth = 1360;
			this._graphics.PreferredBackBufferHeight = 768;
			
			// Create the screen manager component.
			_screenManager = new ScreenManager(this);

			Components.Add(_screenManager);

			// Activate the first screens.
			_screenManager.AddScreen(new BackgroundScreen(), null);
			_screenManager.AddScreen(new MainMenuScreen(), null);
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
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load textures from the Content Pipeline
			_grass = Content.Load<Texture2D>(@"Textures/Funky");
			_wallTexture = Content.Load<Texture2D>(@"Textures/stonewall");
			_ballTexture = Content.Load<Texture2D>(@"Textures/Ball1");
			_spriteSheet = Content.Load<Texture2D>(@"Textures/Objects");

			// Load sound effects from the Content Pipeline
			_ballBounce = Content.Load<SoundEffect>(@"Sounds/Bounce");
			_playerScored = Content.Load<SoundEffect>(@"Sounds/Supporters");

			// Load our score font.
			_font = Content.Load<SpriteFont>(@"ScoreFont");

			// Initialize screens.
			//mControllerScreen = new ControllerDetectScreen(this.Content, new EventHandler(ControllerDetectScreenEvent));
			//mTitleScreen = new TitleScreen(this.Content, new EventHandler(TitleScreenEvent));
            
			// Current screen.
			//mCurrentScreen = mControllerScreen;

			// Walls for the arena.
            _leftWall = new ArenaWall(_wallTexture, _lWallRect);
            _rightWall = new ArenaWall(_wallTexture, _rWallRect);
            _topWall = new ArenaWall(_wallTexture, _tWallRect);
            _bottomWall = new ArenaWall(_wallTexture, _bWallRect);
            _paddle1 = new Paddle(_grass, _pad1Rect);
            _paddle2 = new Paddle(_grass, _pad1Rect);
            _ball = new Ball(_ballTexture, _center);
            InitBall();

            _gameScore = new Score(_font);
            _gameScore.RightScore = 0;
            _gameScore.LeftScore = 0;
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

            _center.X = (double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2;
            _center.Y = (double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;

			#if WINDOWS
            _lWallRect = new CollidableObjects.Rectangle(0.0, 0.0, 5.0, 
			(double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            _rWallRect = new CollidableObjects.Rectangle(794.0, 0.0, 5.0, 
			(double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);

            _tWallRect = new CollidableObjects.Rectangle(0.0, 0.0,
			(double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, 5.0);

            _bWallRect = new CollidableObjects.Rectangle(0.0, 594.0,
			(double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, 5.0);

            _pad1Rect = new CollidableObjects.Rectangle(40.0, 
			(double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height-900, 40.0, 150.0);

            _pad2Rect = new CollidableObjects.Rectangle(724.0, 
			(double)GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height-900, 40.0, 150.0);
			#endif

            _ballCircle = new CollidableObjects.Circle(_center.X, _center.Y, 5.0);
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
                _paddle1.Y -= 10;
            }
            else if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y <= -0.5f ||
                Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
            {
                _paddle1.Y += 10;
            }

            // Player two controls (red).
            if (GamePad.GetState(PlayerIndex.Two).DPad.Up == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y >= 0.5f ||
                Keyboard.GetState(PlayerIndex.Two).IsKeyDown(Keys.Up))
            {
                _paddle2.Y -= 10;
            }
            else if (GamePad.GetState(PlayerIndex.Two).DPad.Down == ButtonState.Pressed
                || GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y <= -0.5f ||
                Keyboard.GetState(PlayerIndex.Two).IsKeyDown(Keys.Down))
            {
                _paddle2.Y += 10;
            }

            // Limit the bars' movement to the screen bounds.
            if (_paddle2.Y < 0) // Upper bound.
            {
                _paddle2.Y = 0; // Limit.
            }

            if (_paddle1.Y < 0)
            {
                _paddle1.Y = 0;
            }

            if (_paddle2.Y + _paddle2.Height > GraphicsDevice.Viewport.Bounds.Height)
            {
                _paddle2.Y = GraphicsDevice.Viewport.Bounds.Height - _paddle2.Height;
            }

            if (_paddle1.Y + _paddle1.Height > GraphicsDevice.Viewport.Bounds.Height)
            {
                _paddle1.Y = GraphicsDevice.Viewport.Bounds.Height - _paddle1.Height;
            }

            //ballVector = new _2D.Vector(-5.0, 5.0);

            // Move the ball.
            _ball.Update(gameTime);

            // Handling ball initialization; use Navigation Button to reset.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed ||
                Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))
            {
                InitBall();
            }

            // Collision handling. //
            // Wall collisions.
            if (_ball.Y < 0.0 || // if the ball reach the upper bound of the screen
                  _ball.Y + _ball.Radius > 750.0) // or the lower one
            {
                _ballVector.X = -_ballVector.X; // make if bounce by inverting the Y velocity
                _ballSpeed = new _2D.Speed(_ballVector, _dTime);
               // ballBounce.Play(); // Bounce sound.
            }

            // Bar collisions.
            if((_ball.X == _paddle1.X) || (_ball.X == _paddle1.Y))
            {
                _ballVector.X = -_ballVector.X; // Make it bounce by inverting the X velocity.
               // ballBounce.Play(); // Bounce sound.

            }

            // Scoring.
            if (_ball.X < 0) // Red scores a point.
            {
                _gameScore.RightScore++;
                //playerScored.Play(); // Play a sound when a point is scored.
                InitBall(); // Re-init the ball.
            }
            else if (_ball.X + _ball.Radius > GraphicsDevice.Viewport.Bounds.Width) // Blue scores a point.
            {
                _gameScore.LeftScore++;
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
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            
             /***************************************/
			//rightWall.Draw(gameTime, spriteBatch);
			//leftWall.Draw(gameTime, spriteBatch);
			//topWall.Draw(gameTime, spriteBatch);
			//bottomWall.Draw(gameTime, spriteBatch);
            _paddle1.Draw(gameTime, _spriteBatch);
            _paddle2.Draw(gameTime, _spriteBatch);
            _ball.Draw(gameTime, _spriteBatch);
            _gameScore.Draw(gameTime, _spriteBatch);
            /*****************************/
            _spriteBatch.End();

            

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
            _ballVector = new _2D.Vector(5.0, 100.0);
            _dTime = new TimeSpan(100000);
            
            //// Randomize the ball orientation.
            switch (rand.Next(4))
            {
                case 0: 
                    _ballSpeed = new _2D.Speed(_ballVector, _dTime);
                    _ballVector.X = speed;
                    _ballVector.Y = speed;
                    _ball.BallSpeed = _ballSpeed;
                    break;
                case 1: 
                    _ballVector.X = -speed; 
                    _ballVector.Y = speed;
                    _ballSpeed = new _2D.Speed(_ballVector, _dTime);
                    _ball.BallSpeed = _ballSpeed;
                    break;
                case 2: 
                    _ballVector.X = speed; 
                    _ballVector.Y = -speed;
                    _ballSpeed = new _2D.Speed(_ballVector, _dTime);
                    _ball.BallSpeed = _ballSpeed;
                    break;
                case 3: 
                    _ballVector.X = -speed;
                    _ballVector.Y = -speed;
                    _ballSpeed = new _2D.Speed(_ballVector, _dTime);
                    _ball.BallSpeed = _ballSpeed;
                    break;
            }

            //// Initialize the ball to the center of the screen.
            _ball.X = GraphicsDevice.Viewport.Bounds.Width / 2 - _ball.Radius / 2;
            _ball.Y = GraphicsDevice.Viewport.Bounds.Height / 2 - _ball.Radius / 2;
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
