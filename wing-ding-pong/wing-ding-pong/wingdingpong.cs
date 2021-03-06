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
		#region MemberData

		static readonly string[] preloadAssets =
        {
            @"Textures/gradient",
        };

        IList<CollidableObjects.Collidable2DBase> _collidableObjects = new List<CollidableObjects.Collidable2DBase>(); //objects that can collide with each other will exist here
        IList<IDrawable> _drawObjects = new List<IDrawable>(); //Anything drawn on the screen will exist here

		GraphicsDeviceManager _graphics;
		SpriteBatch _spriteBatch;
		SpriteFont _font;

        // Scores.
        Score _gameScore;

        TimeSpan _dTime;
        _2D.Vector _ballVector;
        ArenaWall _leftWall, _rightWall, _topWall, _bottomWall; // Creating the walls.
        Ball _ball;
        wing_ding_pong._2D.Speed _ballSpeed;
        IList<Player> _players;
        Paddle _paddle1, _paddle2;
        List<ArenaWall> walls;
		// Clone textures.
		Texture2D _grass;		// Texture used for whatever.
		IList<Texture2D> _vertWallTextures;	// Texture used for walls.
		IList<Texture2D> _horzWallTextures;	// Texture used for walls.
		Texture2D _ballTexture;	// Texture used for the ball.
        Texture2D _paddle1Texture;
        Texture2D _paddle2Texture;
        _2D.Point _center = new _2D.Point(375.0, 225.0);

		// Sound effects.
		SoundEffect _ballBounce;
		SoundEffect _playerScored;
        CollidableObjects.Rectangle _lWallRect, _rWallRect, _tWallRect, _bWallRect;
        CollidableObjects.Circle _ballCircle;
        CollidableObjects.Rectangle _pad1Rect, _pad2Rect;
        Rules _rules = new Rules();
        bool _isGameStarted = false;

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

            this._graphics.PreferredBackBufferWidth = 800;
            this._graphics.PreferredBackBufferHeight = 600;
			
			// Create the screen manager component.
			//_screenManager = new ScreenManager(this);

			Components.Add(_screenManager);

			// Activate the first screens.
			//_screenManager.AddScreen(new BackgroundScreen(), null);
			//_screenManager.AddScreen(new MainMenuScreen(), null);
		}
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
			//_grass = Content.Load<Texture2D>(@"Textures/Funky");
            _horzWallTextures = new List<Texture2D>();
            _horzWallTextures.Add(Content.Load<Texture2D>(@"Textures/wall_tile_horz"));
            _vertWallTextures = new List<Texture2D>();
            _vertWallTextures.Add(Content.Load<Texture2D>(@"Textures/wall_tile_vert"));
			_ballTexture = Content.Load<Texture2D>(@"Textures/Ball1");
			//_spriteSheet = Content.Load<Texture2D>(@"Textures/Objects");
            _paddle1Texture = Content.Load<Texture2D>(@"Textures/paddle_red");
            _paddle2Texture = Content.Load<Texture2D>(@"Textures/paddle_blue");

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
			double width, height;
            width = (double)GraphicsDevice.Viewport.Bounds.Width;
            height = (double)GraphicsDevice.Viewport.Bounds.Height;
			base.Initialize();

            _center.X = width / 2;
            _center.Y = height / 2;

            _lWallRect = new CollidableObjects.Rectangle(0, _center.Y, _horzWallTextures[0].Width / 2, height / 2);

            _rWallRect = new CollidableObjects.Rectangle(width, _center.Y, _horzWallTextures[0].Width / 2, height / 2);

            _tWallRect = new CollidableObjects.Rectangle(_center.X, 0, width / 2, _vertWallTextures[0].Height / 2);

            _bWallRect = new CollidableObjects.Rectangle(_center.X, height, width / 2, _vertWallTextures[0].Height / 2);
            
            _pad1Rect = new CollidableObjects.Rectangle(60, _center.Y, _paddle1Texture.Width / 2.0, _paddle1Texture.Height / 2.0);

            _pad2Rect = new CollidableObjects.Rectangle(width - 60, _center.Y, _paddle2Texture.Width / 2.0, _paddle2Texture.Height / 2.0);

            _ballCircle = new CollidableObjects.Circle(_center.X, _center.Y, 5.0);

            _leftWall = new ArenaWall(_horzWallTextures, _lWallRect);
            _rightWall = new ArenaWall(_horzWallTextures, _rWallRect);
            _topWall = new ArenaWall(_vertWallTextures, _tWallRect);
            _bottomWall = new ArenaWall(_vertWallTextures, _bWallRect);
            _players = new List<Player>() {new Player(PlayerIndex.One), new Player(PlayerIndex.Two)};
            _paddle1 = new Paddle(_paddle1Texture, _pad1Rect, _players[0]);
            _paddle2 = new Paddle(_paddle2Texture, _pad2Rect, _players[1]);
            _leftWall.Owner = _paddle1.Owner;
            _rightWall.Owner = _paddle2.Owner;
            _topWall.Owner = null;
            _bottomWall.Owner = null;
            _ballVector = new _2D.Vector(50.0, 0);
            _dTime = new TimeSpan(1000000);
            _ball = new Ball(_ballTexture, _center, new _2D.Speed(_ballVector, _dTime));

            /*paddles = new List<Paddle>();
            paddles.Add(_paddle1);
            paddles.Add(_paddle2);*/

            walls = new List<ArenaWall>();
            walls.Add(_leftWall);
            walls.Add(_rightWall);
            walls.Add(_topWall);
            walls.Add(_bottomWall);

            _gameScore = new Score(_font, _players);
            _collidableObjects.Add(_leftWall);
            _collidableObjects.Add(_rightWall);
            _collidableObjects.Add(_topWall);
            _collidableObjects.Add(_bottomWall);
            _collidableObjects.Add(_paddle1);
            _collidableObjects.Add(_paddle2);
            _collidableObjects.Add(_ball);

            _drawObjects.Add(_ball);
            _drawObjects.Add(_paddle1);
            _drawObjects.Add(_paddle2);
            _drawObjects.Add(_leftWall);
            _drawObjects.Add(_rightWall);
            _drawObjects.Add(_topWall);
            _drawObjects.Add(_bottomWall);
            _drawObjects.Add(_gameScore);
            wing_ding_pong.CollisionDetection.RegisterCollisionTrait<wing_ding_pong.CollidableObjects.Circle,
                                                                    wing_ding_pong.CollidableObjects.Rectangle>
                                                                    (new wing_ding_pong.Traits.CircleRecCollisionCheckTraits());
            wing_ding_pong.CollisionDetection.RegisterCollisionTrait<wing_ding_pong.CollidableObjects.Circle,
                                                                    wing_ding_pong.CollidableObjects.Circle>
                                                                    (new wing_ding_pong.Traits.CircleCircleCollisionCheckTraits());
            wing_ding_pong.CollisionDetection.RegisterCollisionTrait<wing_ding_pong.CollidableObjects.Circle,
                                                                    wing_ding_pong.CollidableObjects.Triangle>
                                                                    (new wing_ding_pong.Traits.CircleTriangleCollisionCheckTraits());
            wing_ding_pong.CollisionDetection.RegisterCollisionTrait<wing_ding_pong.CollidableObjects.Rectangle,
                                                                    wing_ding_pong.CollidableObjects.Rectangle>
                                                                    (new wing_ding_pong.Traits.RecRecCollisionCheckTraits());
            
            _rules.RegisterRule<Ball, ArenaWall>(new Traits.BallArenaWallCollisionRules(_center, _ballVector.Clone(), _ballBounce, _playerScored));
            _rules.RegisterRule<Ball, Paddle>(new Traits.BallPaddleCollisionRules(_ballBounce));
            _rules.RegisterRule<Paddle, ArenaWall>(new Traits.PaddleArenaWallCollisionRules());
            _rules.RegisterRule<Ball, Powerup>(new Traits.BallPowerupCollisionRules());
		}

		#endregion
				
		#region UnloadContent

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
            Content.Unload();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed
                 || Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Space))
            {
                _isGameStarted = !_isGameStarted;
            }

            if (true)
            {
                for (int i = 0; i < _collidableObjects.Count; i++)
                {
                    for (int j = i + 1; j < _collidableObjects.Count; j++)
                    {
                        wing_ding_pong._2D.Vector obj1PosDp;
                        wing_ding_pong._2D.Vector obj2PosDp;
                        wing_ding_pong._2D.Vector obj1CollDirection;
                        wing_ding_pong._2D.Vector obj2CollDirection;
                        if (CollisionDetection.isCollision(_collidableObjects[i].CollidableObjects.ToArray(),
                                                            _collidableObjects[i].Speed.GetVector(gameTime.ElapsedGameTime),
                                                            _collidableObjects[j].CollidableObjects.ToArray(),
                                                            _collidableObjects[j].Speed.GetVector(gameTime.ElapsedGameTime),
                                                            out obj1PosDp, out obj1CollDirection, out obj2PosDp,
                                                            out obj2CollDirection))
                        {
                            _rules.ProcessCollsions(_collidableObjects[i], _collidableObjects[j], obj1PosDp, obj1CollDirection,
                                                    obj2PosDp, obj2CollDirection);
                        }
                    }
                }
                foreach (CollidableObjects.Collidable2DBase obj in _collidableObjects)
                {
                    obj.Update(gameTime);
                }
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
            
            //_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap,
                DepthStencilState.Default, RasterizerState.CullNone);
            
            foreach (IDrawable item in _drawObjects)
            {
                item.Draw(gameTime, _spriteBatch);
            }
            /*****************************/
            _spriteBatch.End();

			base.Draw(gameTime);
		}	// End "draw".

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
