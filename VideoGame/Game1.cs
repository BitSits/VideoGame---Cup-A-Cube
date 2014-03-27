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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace VideoGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Level level;
        KeyboardState currentKb, previousKb;
        float time = 0.0f;
        Song song;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            level = new Level(Services);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            song = Content.Load<Song>("Basse Dance");
            MediaPlayer.Play(song);
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.IsRepeating = true;

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            currentKb = Keyboard.GetState();
            if (level.gameState.state==State.over)
            {
                if (currentKb.IsKeyDown(Keys.Enter) && previousKb.IsKeyUp(Keys.Enter))
                {
                    level.gameState.state = State.menu;
                    level = new Level(Services);
                }
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            base.Update(gameTime);
            level.Update(gameTime);
            previousKb = currentKb;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            level.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
