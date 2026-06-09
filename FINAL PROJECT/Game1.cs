using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

namespace FINAL_PROJECT
{
    enum Screen
    {
        store,
        turntable,
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        gramps gramps;

        Texture2D recordTexture, turntableTexture, turntableButton;
        Rectangle recordRect = new Rectangle(0, 0, 800, 600);
        Rectangle turntableRect = new Rectangle(0, 0, 800, 600);
        bool isDraggingRecord;
        MouseState currentMouseState;
        MouseState prevMouseState;
        Screen screen;

        bool turntableSelectButton = false;
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
        Rectangle wallBarrier, recordCrateBarrier1, recordCrateBarrier2, turntableBarrier;
        Texture2D rockCrateTexture, metalCrateTexture, hiphopCrateTexture, jazzCrateTexture, canadianCrateTexture;  
        Rectangle rockRect = new Rectangle(0, 0, 800, 600);
        Rectangle metalRect = new Rectangle(0, 0, 800, 600);
        Rectangle hiphopRect = new Rectangle(0, 0, 800, 600);
        Rectangle jazzRect = new Rectangle(0, 0, 800, 600);
        Rectangle canadianRect = new Rectangle(0, 0, 800, 600);
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
            recordRect = new Rectangle(10, 10, 50, 50);
            turntableBarrier = new Rectangle(34, 130, 106, 30);
            isDraggingRecord = false;
            screen = Screen.store;
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

            turntableTexture = Content.Load<Texture2D>("turntable");
            rockCrateTexture = Content.Load<Texture2D>("Rock Record Crate");
            metalCrateTexture = Content.Load<Texture2D>("Record Crate Metal");
            hiphopCrateTexture = Content.Load<Texture2D>("Record Crate Hip Hop");
            jazzCrateTexture = Content.Load<Texture2D>("Record Crate Jazz");
            canadianCrateTexture = Content.Load<Texture2D>("Record Crate Canadian");

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
            gramps = new gramps(grampsDownFrames, grampsUpFrames, grampsRightFrames, grampsLeftFrames);
            storeTexture = Content.Load<Texture2D>("walls");
            itemTextures = Content.Load<Texture2D>("items");
            wallTexture = Content.Load<Texture2D>("wallTexture");
            recordCrateBarrierTXR1 = Content.Load<Texture2D>("wallTexture");
            recordCrateBarrierTXR2 = Content.Load<Texture2D>("wallTexture");

            recordTexture = Content.Load<Texture2D>("recordTexture");

            turntableButton = Content.Load<Texture2D>("turntableButton");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gramps.Update(gameTime);
            if (screen == Screen.turntable)
            {
                currentMouseState = Mouse.GetState();
                if (currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (recordRect.Contains(currentMouseState.Position))
                    {
                        isDraggingRecord = true;
                    }
                }
                else if (currentMouseState.LeftButton == ButtonState.Released)
                {
                    isDraggingRecord = false;
                }
                if (isDraggingRecord)
                {
                    recordRect.X = currentMouseState.X - recordRect.Width / 2;
                    recordRect.Y = currentMouseState.Y - recordRect.Height / 2;
                }
                prevMouseState = currentMouseState;
            }
            base.Update(gameTime);
            // TODO: Add your update logic here
            if (gramps.Hitbox.Intersects(wallBarrier) || gramps.Hitbox.Intersects(recordCrateBarrier1) || gramps.Hitbox.Intersects(recordCrateBarrier2))
            {
                gramps.MoveBack(gramps.Velocity);
            }
            if (gramps.Hitbox.Intersects(turntableBarrier))
            {
                gramps.MoveBack(gramps.Velocity);
                turntableSelectButton = true;

            }
            customerNPC.Update(gameTime);


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here


            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(wallTexture, recordCrateBarrier1, Color.White);
            _spriteBatch.Draw(wallTexture, recordCrateBarrier2, Color.White);
            _spriteBatch.Draw(wallTexture, wallBarrier, Color.White);
            _spriteBatch.Draw(wallTexture, turntableBarrier, Color.White);
            _spriteBatch.Draw(storeTexture, storeRect, Color.White);

            //_spriteBatch.Draw(itemTextures, storeRect, Color.White);
            if (gramps.turntableOwned)
            {
                _spriteBatch.Draw(turntableTexture, turntableRect, Color.White);
            }
            if (gramps.RockCrateOwned)
            {
                _spriteBatch.Draw(rockCrateTexture, rockRect, Color.White);
            }
            if (gramps.MetalCrateOwned)
            {
                _spriteBatch.Draw(metalCrateTexture, metalRect, Color.White);
            }
            if (gramps.HipHopCrateOwned)
            {
                _spriteBatch.Draw(hiphopCrateTexture, hiphopRect, Color.White);
            }
            if (gramps.JazzCrateOwned)
            {
                _spriteBatch.Draw(jazzCrateTexture, jazzRect, Color.White);
            }
            if (gramps.CanadianCrateOwned)
            {
                _spriteBatch.Draw(canadianCrateTexture, canadianRect, Color.White);
            }
            if (turntableSelectButton)
            {
                _spriteBatch.Draw(turntableButton,)
            }
            gramps.Draw(_spriteBatch);
            customerNPC.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}



