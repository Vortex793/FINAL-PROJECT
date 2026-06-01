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
            Texture2D storeTexture, itemTextures;
            Rectangle window;
            
            List<Texture2D> grampsDownFrames = new List<Texture2D>();
            List<Texture2D> grampsUpFrames = new List<Texture2D>();
            List<Texture2D> grampsRightFrames = new List<Texture2D>();
            List<Texture2D> grampsLeftFrames = new List<Texture2D>();
            
            List<customer> customerSprites = new List<customer>();
            customer customerNPC;
            Texture2D wallTexture, recordCrateBarrierTXR1, recordCrateBarrierTXR2;
            Rectangle wallBarrier, recordCrateBarrier1, recordCrateBarrier2;
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
                wallBarrier = new Rectangle(0, 0, 1000, 130);
                recordCrateBarrier1 = new Rectangle(160, 158, 322, 43);
                recordCrateBarrier2 = new Rectangle(160, 315, 322, 43);
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
             

                customer customer1 = new customer();
                
                customer1.downFrames = new List<Texture2D>()
                {
                    Content.Load<Texture2D>("customer1IdleDown"),
                    Content.Load<Texture2D>("customer1Walk1Down"),
                    Content.Load<Texture2D>("customer1Walk2Down")
                };
                customer1.upFrames = new List<Texture2D>()
                {
                    Content.Load<Texture2D>("customer1IdleUp"),
                    Content.Load<Texture2D>("customer1Walk1Up"),
                    Content.Load<Texture2D>("customer1Walk2Up")
                };
                customer1.rightFrames = new List<Texture2D>()
                {
                    Content.Load<Texture2D>("customer1IdleRight"),
                    Content.Load<Texture2D>("customer1Walk1Right"),
                    Content.Load<Texture2D>("customer1Walk2Right")
                };
                customer1.leftFrames = new List<Texture2D>()
                {
                    Content.Load<Texture2D>("customer1IdleLeft"),
                    Content.Load<Texture2D>("customer1Walk1Left"),
                    Content.Load<Texture2D>("customer1Walk2Left")
                };
                customerSprites.Add(customer1);
                customerNPC = customer1;
            gramps = new gramps(grampsDownFrames, grampsUpFrames,grampsRightFrames, grampsLeftFrames);
                storeTexture = Content.Load<Texture2D>("walls");
                itemTextures = Content.Load<Texture2D>("items");
                wallTexture = Content.Load<Texture2D>("wallTexture");
                recordCrateBarrierTXR1 = Content.Load<Texture2D>("wallTexture");
                recordCrateBarrierTXR2 = Content.Load<Texture2D>("wallTexture");
        }

            protected override void Update(GameTime gameTime)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();

                gramps.Update(gameTime);

                base.Update(gameTime);
            // TODO: Add your update logic here
                if (gramps.Hitbox.Intersects(wallBarrier) || gramps.Hitbox.Intersects(recordCrateBarrier1) || gramps.Hitbox.Intersects(recordCrateBarrier2))
                {
                    gramps.MoveBack(gramps.Velocity);
                }
                
                customerNPC.Update(gameTime);
        }

            protected override void Draw(GameTime gameTime)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // TODO: Add your drawing code here


                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

                _spriteBatch.Draw(wallTexture, wallBarrier, Color.White);
                _spriteBatch.Draw(storeTexture, storeRect, Color.White);
                _spriteBatch.Draw(wallTexture, recordCrateBarrier1, Color.White);
                _spriteBatch.Draw(wallTexture, recordCrateBarrier2, Color.White);
                _spriteBatch.Draw(itemTextures, storeRect, Color.White);
                gramps.Draw(_spriteBatch);
                customerNPC.Draw(_spriteBatch);
                _spriteBatch.End();

                base.Draw(gameTime);
        }
        }
    }



