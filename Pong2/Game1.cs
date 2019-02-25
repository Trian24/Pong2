using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState keyboardState;
        private MouseState oldState, newState;
        GameObject wallUp, wallDown, wallLeft, wallRight;
        Player player;
        Ball ball;
        AI ai;
        Texture2D texture, Start, Exit;
        SpriteFont font;
        int score1, score2;
        private enum GameState { StartMenu, Game, Pause};
        GameState gameState;
        Rectangle StartR, ExitR;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 400;
            graphics.PreferredBackBufferHeight = 400;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            score1 = 0;
            score2 = 0;
            gameState = GameState.StartMenu;
            oldState = Mouse.GetState();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            texture = Content.Load<Texture2D>("Texture");
            font = Content.Load<SpriteFont>("File");
            Start = Content.Load<Texture2D>("Start");
            Exit = Content.Load<Texture2D>("Pause");

            player = new Player(texture, new Vector2(70, 105), 10, 30);
            wallLeft = new GameObject(texture, new Vector2(50, 50), 10, 120);
            wallUp = new GameObject(texture, new Vector2(50, 50), 180, 10);
            wallRight = new GameObject(texture, new Vector2(230, 50), 10, 120);
            wallDown = new GameObject(texture, new Vector2(50, 160), 180, 10);
            ball = new Ball(texture, new Vector2(140, 105), 5, 5, new Vector2(-1, -1));
            ai = new AI(texture, new Vector2(210, 105), 10, 30);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if(gameState.Equals(GameState.StartMenu))
            {
                IsMouseVisible = true;
                newState = Mouse.GetState();
                if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    Rectangle mouseClick = new Rectangle(newState.X, newState.Y, 10, 10);
                    System.Diagnostics.Debug.WriteLine(newState.X.ToString() +
                                   "," + newState.Y.ToString());
                    if (mouseClick.Intersects(Start.Bounds))
                    {
                        gameState = GameState.Game;
                    }

                    if (mouseClick.Intersects(Exit.Bounds))
                    {
                        gameState = GameState.Game;
                    }
                }

                oldState = newState;
            }

            if (gameState.Equals(GameState.Game))
            {
                IsMouseVisible = false;
                player.isMovingDown = false;
                player.isMovingUp = false;
                player.playerMove(keyboardState);
                player.isPlayerIntersect(wallUp, wallDown);
                player.Update(gameTime);

                ai.AIMove(ball);
                ai.Update(gameTime);
                ai.isAiIntersect(wallUp, wallDown);

                ball.Update(gameTime);
                ball.checkBallIntersect(wallUp, wallDown, wallLeft, wallRight, player, ai);

                if (ball.Box.Intersects(wallLeft.Box))
                {
                    score1++;
                    ball.Position = new Vector2(140, 105);
                    player.Position = new Vector2(70, 105);
                    ai.Position = new Vector2(210, 105);
                }
                if (ball.Box.Intersects(wallRight.Box))
                {
                    score2++;
                    ball.Position = new Vector2(140, 105);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if(gameState.Equals(GameState.StartMenu))
            {
                spriteBatch.Draw(Start, new Vector2(200,10), Color.White);
                StartR = new Rectangle(200, 10, Start.Width, Start.Height);
                spriteBatch.Draw(Exit, new Vector2(200, 100), Color.White);
                ExitR = new Rectangle(200, 100, Exit.Width, Exit.Height);
                spriteBatch.DrawString(font, "width : " + Start.Width + " | height: " + Start.Height, new Vector2(200, 300), Color.White);
            }

            if (gameState.Equals(GameState.Game) || gameState.Equals(GameState.Pause))
            {
                player.Draw(spriteBatch);
                ball.Draw(spriteBatch);
                wallLeft.Draw(spriteBatch);
                wallRight.Draw(spriteBatch);
                wallUp.Draw(spriteBatch);
                wallDown.Draw(spriteBatch);
                ai.Draw(spriteBatch);
                spriteBatch.DrawString(font, "Score : " + score1 + " : " + score2, new Vector2(100, 200), Color.White);
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
