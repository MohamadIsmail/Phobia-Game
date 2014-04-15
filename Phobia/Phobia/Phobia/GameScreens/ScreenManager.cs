using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Phobia
{
    public enum GameState { Menu, Play, intro, gameOver, Win, Credits }
    class ScreenManager : DrawableGameComponent
    {

        #region Fields

        public static GameState gameState = GameState.Menu;
        private GameState GS = GameState.Menu;
        public static Input keyboard;
        public static Camera camera;
        public static bool intro_once = false;
        public static bool isFirst = true;
        public static SpriteFont spriteFont_mainMenu, spriteFont_gameplay, spriteFont_intro, spriteFont_title, spriteFont_gameOver;
        public static bool IsExiting = false;
        public static GameScreen gameScreen;
        Screen currentScreen;
        SpriteBatch spriteBatch;

        #endregion

        #region Functions

        #region Constructor
        public ScreenManager(Game game)
            : base(game)
        {
            gameScreen = new GameScreen();
            keyboard = new Input();
        }
        #endregion 

        // Load your graphics content, camera settings, current screen using polymorphism.
        protected override void LoadContent()
        {
            camera = new Camera(GraphicsDevice.Viewport);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont_mainMenu = Game1.content.Load<SpriteFont>("Font/Mainmenu");
            spriteFont_gameplay = Game1.content.Load<SpriteFont>("Font/Gameplay");
            spriteFont_intro = Game1.content.Load<SpriteFont>("Font/Intro");
            spriteFont_title = Game1.content.Load<SpriteFont>("Font/Title");
            if (isFirst)
            {
                gameScreen.LoadContent();
                MediaPlayer.IsRepeating = true;
                isFirst = false;
            }
            spriteFont_gameOver = Game1.content.Load<SpriteFont>("Font/GameOver");
            if (gameState == GameState.Menu)
                currentScreen = new MenuScreen();
            else if (gameState == GameState.Credits)
                currentScreen = new Credits();
            else if (gameState == GameState.Win)
                currentScreen = new WinScreen();
            else if (gameState == GameState.gameOver)
                currentScreen = new GameOver();
            else if (gameState == GameState.intro)
                currentScreen = new Intro();
            else
                currentScreen = null;
            if (currentScreen != null)
                currentScreen.LoadContent();
        }

        #region Update and Draw 
        //Responds to user input and updates the current screen.
        public override void Update(GameTime gameTime)
        {
            keyboard.Update();
            if (GS != gameState)
            {
                GS = gameState;
                LoadContent();
            }
            if (currentScreen != null)
                currentScreen.Update(gameTime);
            if (gameState == GameState.Play)
            {
                gameScreen.Update(gameTime);
                currentScreen = null;
            }
            if (IsExiting)
                this.Game.Exit();
            base.Update(gameTime);
        }
        //Draws the current screen.
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (currentScreen != null)
                currentScreen.Draw(spriteBatch);
            if (gameState == GameState.Play)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
                gameScreen.Draw(spriteBatch, gameTime, GraphicsDevice);

            }
            base.Draw(gameTime);
            spriteBatch.End();


        }
        #endregion 

        #endregion
    }
}