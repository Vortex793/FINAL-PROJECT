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
    public class gramps
    {
        Vector2 position;

        float speed = 2f;

        public List<Texture2D> downFrames;
        public List<Texture2D> upFrames;
        public List<Texture2D> rightFrames;
        public List<Texture2D> leftFrames;
        public List<Texture2D> currentFrames;
        public gramps(List<Texture2D> down, List<Texture2D> up, List<Texture2D> right, List<Texture2D> left)
        {
            downFrames = down;
            upFrames = up;
            rightFrames = right;
            leftFrames = left;
            currentFrames = downFrames;
            position = new Vector2(300, 200);
        }
        public void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.W))
            {
                position.Y -= speed;
                currentFrames = downFrames;
            }
            else if (keyboard.IsKeyDown(Keys.S))
            {
                position.Y += speed;
                currentFrames = upFrames;
            }
            else if (keyboard.IsKeyDown(Keys.A))
            {
                position.X -= speed;
                currentFrames = leftFrames;
            }
            else if (keyboard.IsKeyDown(Keys.D))
            {
                position.X += speed;
                currentFrames = rightFrames;
            }
        }
     
        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentFrames.Count > 0)
            {
                spriteBatch.Draw(currentFrames[0], position, Color.White);
            }
        }
    }

        public class Game1 : Game
        {
            private GraphicsDeviceManager _graphics;
            private SpriteBatch _spriteBatch;

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
                // TODO: Add your initialization logic here

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
            }

            protected override void Update(GameTime gameTime)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                // TODO: Add your update logic here
                KeyboardState keyboard = Keyboard.GetState();

                base.Update(gameTime);
            }

            protected override void Draw(GameTime gameTime)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // TODO: Add your drawing code here

                base.Draw(gameTime);
            }
        }
    }

