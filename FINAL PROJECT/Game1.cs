using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Security.Cryptography.X509Certificates;

namespace FINAL_PROJECT
{

        public class Game1 : Game
        {
            private GraphicsDeviceManager _graphics;
            private SpriteBatch _spriteBatch;
            gramps gramps;

            Rectangle storeRect;
            Texture2D storeTexture;
            Rectangle window;
            
            List<Texture2D> grampsDownFrames = new List<Texture2D>();
            List<Texture2D> grampsUpFrames = new List<Texture2D>();
            List<Texture2D> grampsRightFrames = new List<Texture2D>();
            List<Texture2D> grampsLeftFrames = new List<Texture2D>();
            public Game1()
            {
                _graphics = new GraphicsDeviceManager(this);
                Content.RootDirectory = "Content";
                IsMouseVisible = true;
            }

            protected override void Initialize()
            {
                window = new Rectangle(0, 0, 800, 600);
                _graphics.PreferredBackBufferWidth = window.Width;
                _graphics.PreferredBackBufferHeight = window.Height;
                _graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            storeRect = new Rectangle(0, 0, 800, 600);
                base.Initialize();
            }

            protected override void LoadContent()
            {
                _spriteBatch = new SpriteBatch(GraphicsDevice);

                // TODO: use this.Content to load your game content here
                //Down
                grampsDownFrames.Add(Content.Load<Texture2D>("grampsIdleDown"));
                grampsDownFrames.Add(Content.Load<Texture2D>("grampsWalk1Down"));
                grampsDownFrames.Add(Content.Load<Texture2D>("grampsWalk2Down"));

                //Up
                grampsUpFrames.Add(Content.Load<Texture2D>("grampsIdleUp"));
                grampsUpFrames.Add(Content.Load<Texture2D>("grampsWalk1Up"));
                grampsUpFrames.Add(Content.Load<Texture2D>("grampsWalk2Up"));

                //Right
                grampsRightFrames.Add(Content.Load<Texture2D>("grampsIdleRight"));
                grampsRightFrames.Add(Content.Load<Texture2D>("grampsWalk1Right"));
                grampsRightFrames.Add(Content.Load<Texture2D>("grampsWalk2Right"));

                //Left
                grampsLeftFrames.Add(Content.Load<Texture2D>("grampsIdleLeft"));
                grampsLeftFrames.Add(Content.Load<Texture2D>("grampsWalk1Left"));
                grampsLeftFrames.Add(Content.Load<Texture2D>("grampsWalk2Left"));

                gramps = new gramps(grampsDownFrames, grampsUpFrames,grampsRightFrames, grampsLeftFrames);
                storeTexture = Content.Load<Texture2D>("store");
        }

            protected override void Update(GameTime gameTime)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                gramps.Update();

                base.Update(gameTime);
                // TODO: Add your update logic here

        }

            protected override void Draw(GameTime gameTime)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // TODO: Add your drawing code here


                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

                _spriteBatch.Draw(storeTexture, storeRect, Color.White);
                gramps.Draw(_spriteBatch);
                

                _spriteBatch.End();

                base.Draw(gameTime);
        }
        }
    }
