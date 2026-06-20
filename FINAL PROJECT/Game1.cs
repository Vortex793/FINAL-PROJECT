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
        menu,
        howToPlay,

        store,
        pause,
        upgrades,
        turntable,
        ownedRecords,
        stock,
    }
    enum GameState
    {
        settingUp,
        openingStore,
    }
    enum Record 
    { 
        canadianRock,
        Metal,

    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        gramps gramps;

        Texture2D recordTexture, turntableScreen;
        Rectangle recordRect;
        Rectangle recordDestination = new Rectangle(150, 140, 150, 100);
        Rectangle recordInPlace = new Rectangle(-312, -240, 800, 600);
        bool isDraggingRecord = false;
        Rectangle turntableRect = new Rectangle(0, 0, 800, 600);
        Texture2D turntableExitTexture, turntablePlayTexture;
        Rectangle turntableExit = new Rectangle(0, 430, 200, 150);
        Rectangle ownedRecordsExit = new Rectangle(600, 430, 200, 150);
        Rectangle turntablePlay = new Rectangle(300, 200, 100, 50);
        Rectangle centeredRecordRect = new Rectangle(500, 200, 175, 125);
        Texture2D centeredRecordTexture;
        Texture2D turntableTexture, turntableButton;
        Rectangle turntableButtonRect = new Rectangle(30, 0, 160, 110), turntableButtonTrigger;
        bool turntableSelectButton;
        bool recordPlaced = false;
        //Rectangle turntableCenter = new Rectangle(312, 240, 175, 125);
        bool recordShown = false;
        bool albumSelected = false;
        Song canadianRockAlbum;
        Texture2D canadianRecordTexture;
        MouseState currentMouseState;
        MouseState prevMouseState;
        Screen screen;
        Record record;
        GameState gameState;

        float xpTimer = 0f;
        Texture2D whiteXPBar;

        Texture2D upIdle;
        Texture2D upIdle2;
        Texture2D downIdle;
        Texture2D rightIdle;
        Texture2D leftIdle;

        Texture2D upgradesButton, upgradesScreen;
        Rectangle upgradesRect = new Rectangle(600, 430, 200, 150);
        Rectangle upgradesScreenRect = new Rectangle(0, 0, 800, 600);
        Rectangle turntablePurchaseRect = new Rectangle(0, 380, 200, 150);
        Rectangle cratePurchaseRect = new Rectangle(280, 385, 180, 100);
        Rectangle postersPurchaseRect = new Rectangle(600, 380, 200, 150);

        Rectangle storeRect;
        Texture2D storeTexture, itemTextures, menuTexture, pauseTexture;
        Rectangle window;

        KeyboardState keyboard;

        SpriteFont hudFont;
        List<Texture2D> grampsDownFrames = new List<Texture2D>();
        List<Texture2D> grampsUpFrames = new List<Texture2D>();
        List<Texture2D> grampsRightFrames = new List<Texture2D>();
        List<Texture2D> grampsLeftFrames = new List<Texture2D>();

        Rectangle menuStart, menuQuit, menuHTP;

        List<customer> customerSprites = new List<customer>();
        customer customerNPC;
        customer2 customerNPC2;
        Texture2D wallTexture, recordCrateBarrierTXR1, recordCrateBarrierTXR2;
        Rectangle wallBarrier, recordCrateBarrier1, recordCrateBarrier2, turntableBarrier;
        Rectangle cashRegisterCollision;
        Rectangle recordBinScreenTrigger;
        Texture2D rockCrateTexture, metalCrateTexture, hiphopCrateTexture, jazzCrateTexture, canadianCrateTexture, essentialCrateTexture, cashRegisterTexture, halfCashRegisterTexture,topCashRegisterTexture, doorsTexture;
        Rectangle menuScreenRect = new Rectangle(0, 0, 800, 600);
        Rectangle rockRect = new Rectangle(0, 0, 800, 600);
        Rectangle metalRect = new Rectangle(0, 0, 800, 600);
        Rectangle hiphopRect = new Rectangle(0, 0, 800, 600);
        Rectangle jazzRect = new Rectangle(0, 0, 800, 600);
        Rectangle canadianRect = new Rectangle(0, 0, 800, 600);
        Rectangle essentialRect = new Rectangle(0, 0, 800, 600);
        Rectangle canadianAlbumRect = new Rectangle(200, 100, 400, 400);
        Rectangle cashRegisterRect = new Rectangle(0, 0, 800, 600);
        Rectangle doorsRect = new Rectangle(0, 0, 800, 600);
        Rectangle customerPayTrigger = new Rectangle(550, 250, 100, 50);
        public int rockCrateStock = 3;
        public int metalCrateStock = 3;
        public int hiphopCrateStock = 3;
        public int jazzCrateStock = 3;
        public int canadianCrateStock = 3;
        public int essentialCrateStock = 3;
        float customerSpawnTime;
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
            cashRegisterCollision = new Rectangle(505, 200, 155, 30);
            recordRect = new Rectangle(-312, -300, 800, 600);
            isDraggingRecord = false;

            menuStart = new Rectangle(280, 385, 180, 100);
            menuQuit = new Rectangle(0, 380, 200, 150);
            menuHTP = new Rectangle(600, 380, 200, 150);

            recordBinScreenTrigger = new Rectangle(455, 100, 190, 200);
            turntableBarrier = new Rectangle(34, 130, 106, 30);
            turntableButtonTrigger = new Rectangle(34, 160, 106, 30);   //When gramps enters it wil trigger the instruction to press enter
            isDraggingRecord = false;
            screen = Screen.menu;
            gameState = GameState.settingUp;
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

            gramps = new gramps(grampsDownFrames, grampsUpFrames, grampsRightFrames, grampsLeftFrames);
            customer customer1 = new customer();
            customer2 customer2 = new customer2(gramps);

            turntableTexture = Content.Load<Texture2D>("turntable");
            rockCrateTexture = Content.Load<Texture2D>("Rock Record Crate");
            metalCrateTexture = Content.Load<Texture2D>("Record Crate Metal");
            hiphopCrateTexture = Content.Load<Texture2D>("Record Crate Hip Hop");
            jazzCrateTexture = Content.Load<Texture2D>("Record Crate Jazz");
            canadianCrateTexture = Content.Load<Texture2D>("Record Crate Canadian");
            cashRegisterTexture = Content.Load<Texture2D>("Cash Register");
            halfCashRegisterTexture = Content.Load<Texture2D>("cash register half");
            topCashRegisterTexture = Content.Load<Texture2D>("cash register top");
            doorsTexture = Content.Load<Texture2D>("doors");
            essentialCrateTexture = Content.Load<Texture2D>("essential record crate");
            upgradesButton = Content.Load<Texture2D>("upgrades");
            upgradesScreen = Content.Load<Texture2D>("upgrades screen");

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


            customer2.downFrames = new List<Texture2D>()
                {
                    Content.Load<Texture2D>("customer2IdleDown"),
                    Content.Load<Texture2D>("customer2Walk1Down"),
                    Content.Load<Texture2D>("customer2Walk2Down")
            };
            customer2.upFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer2IdleUp"),
                    Content.Load<Texture2D>("customer2Walk1Up"),
                    Content.Load<Texture2D>("customer2Walk2Up")
            };
            customer2.rightFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer2IdleRight"),
                    Content.Load<Texture2D>("customer2Walk1Right"),
                    Content.Load<Texture2D>("customer2Walk2Right")
            };
            customer2.leftFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer2IdleLeft"),
                    Content.Load<Texture2D>("customer2Walk1Left"),
                    Content.Load<Texture2D>("customer2Walk2Left")
            };
            customer1.upIdle = Content.Load<Texture2D>("customer1IdleUp");
            customer2.upIdle2 = Content.Load<Texture2D>("customer2IdleUp");
            customer1.currentFrames = customer1.downFrames;
            customer2.currentFrames = customer2.downFrames;
            downIdle = Content.Load<Texture2D>("customer1IdleDown");
            rightIdle = Content.Load<Texture2D>("customer1IdleRight");
            leftIdle = Content.Load<Texture2D>("customer1IdleLeft");
            customerSprites.Add(customer1);
            customerNPC = customer1;
            customerNPC2 = customer2;
            
            storeTexture = Content.Load<Texture2D>("walls");
            menuTexture = Content.Load<Texture2D>("record game menu");
            pauseTexture = Content.Load<Texture2D>("record game pause");
            //itemTextures = Content.Load<Texture2D>("items");
            wallTexture = Content.Load<Texture2D>("wallTexture");
            recordCrateBarrierTXR1 = Content.Load<Texture2D>("wallTexture");
            recordCrateBarrierTXR2 = Content.Load<Texture2D>("wallTexture");

            recordTexture = Content.Load<Texture2D>("recordTexture");
            centeredRecordTexture = Content.Load<Texture2D>("centered record");

            turntableButton = Content.Load<Texture2D>("turntableButton");
            turntableExitTexture = Content.Load<Texture2D>("close");
            turntablePlayTexture = Content.Load<Texture2D>("playButton");
            turntableScreen = Content.Load<Texture2D>("turntableScreen");
            canadianRockAlbum = Content.Load<Song>("canadianRockAlbum");
            canadianRecordTexture = Content.Load<Texture2D>("Canadian Rock");

            hudFont = Content.Load<SpriteFont>("font");
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            keyboard = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            this.Window.Title = currentMouseState.Position.ToString();
            
            if (MediaPlayer.State == MediaState.Playing)
            {
                xpTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (xpTimer  >= 20f)
                {
                    gramps.xp += 20;
                    xpTimer = 0f;
                }
            }
            //Leveling up
            if (gramps.xp >= gramps.level * 100)
            {
                gramps.xp -= gramps.level * 100;
                gramps.level++;
               
            }
            customerSpawnTime = Math.Max(5f, 30f - gramps.level);
            if (screen == Screen.menu)
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (menuStart.Contains(currentMouseState.Position))
                    {
                        screen = Screen.store;
                    }
                    else if (menuQuit.Contains(currentMouseState.Position))
                    {
                        Exit();
                    }
                    else if (menuHTP.Contains(currentMouseState.Position))
                    {
                        screen = Screen.howToPlay;
                    }
                }
            }
            if (screen == Screen.pause)
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (menuStart.Contains(currentMouseState.Position))
                    {
                        screen = Screen.store;
                    }
                    else if (menuQuit.Contains(currentMouseState.Position))
                    {
                        Exit();
                    }
                    else if (menuHTP.Contains(currentMouseState.Position))
                    {
                        screen = Screen.howToPlay;
                    }
                }

            }
            if (screen == Screen.store)
            {
                gramps.Update(gameTime, keyboard);

                // TODO: Add your update logic here

                if (currentMouseState.LeftButton == ButtonState.Pressed && upgradesRect.Contains(currentMouseState.Position))
                {
                    screen = Screen.upgrades;
                }
                if (gramps.Hitbox.Intersects(wallBarrier) || gramps.Hitbox.Intersects(recordCrateBarrier1) || gramps.Hitbox.Intersects(recordCrateBarrier2))
                {
                    gramps.MoveBack(gramps.Velocity);
                }
                if (gramps.Hitbox.Intersects(cashRegisterCollision))
                {
                    gramps.MoveBack(gramps.Velocity);
                }
                if (gramps.Hitbox.Intersects(turntableBarrier))
                {
                    gramps.MoveBack(gramps.Velocity);


                }
                if (gramps.Hitbox.Intersects(turntableButtonTrigger))
                {
                    turntableSelectButton = true;

                    if (keyboard.IsKeyDown(Keys.Enter))
                    {
                        screen = Screen.turntable;
                    }
                }
                else
                {
                    turntableSelectButton = false;
                }
                if (customerNPC.Hitbox.Intersects(customerPayTrigger) && !customerNPC.hasPaid)
                {
                    gramps.money += 40;
                    customerNPC.hasPaid = true;
                }
                if (customerNPC2.Hitbox.Intersects(customerPayTrigger) && !customerNPC2.hasPaid)
                {
                    gramps.money += 40;
                    customerNPC2.hasPaid = true;
                }

                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                {
                    screen = Screen.pause;
                }
                customerNPC.Update(gameTime);
                customerNPC2.Update(gameTime);
            }
            else if (screen == Screen.turntable)
            {
                //if (currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                //{
                //    if (recordRect.Contains(currentMouseState.Position))
                //    {
                //        isDraggingRecord = true;
                //    }
                //}
                //else if (currentMouseState.LeftButton == ButtonState.Released)
                //{
                //    isDraggingRecord = false;
                //}
                //exit button
                if (currentMouseState.LeftButton == ButtonState.Pressed && turntableExit.Contains(currentMouseState.Position))
                {
                    screen = Screen.store;
                }
                //play button
                if (currentMouseState.LeftButton == ButtonState.Pressed && turntablePlay.Contains(currentMouseState.Position) && record == Record.canadianRock && albumSelected)
                {
                    MediaPlayer.Play(canadianRockAlbum);
                    MediaPlayer.Volume = 0.25f;
                }
                if (isDraggingRecord)
                {
                    recordRect.X = currentMouseState.X - recordRect.Width / 2;
                    recordRect.Y = currentMouseState.Y - recordRect.Height / 2;
                }
                if (NewClick() && centeredRecordRect.Contains(currentMouseState.Position))
                    isDraggingRecord = true;
                else if (isDraggingRecord && currentMouseState.LeftButton == ButtonState.Released)
                    isDraggingRecord = false;
  
                else if (isDraggingRecord)
                    centeredRecordRect.Offset(currentMouseState.X - prevMouseState.X,
                    currentMouseState.Y - prevMouseState.Y);
                if (centeredRecordRect.Intersects(recordDestination))
                {
                    recordShown = false;
                    recordPlaced = true;
                }

                else
                {
                    recordPlaced = false;
                }
                if (currentMouseState.LeftButton == ButtonState.Pressed && recordBinScreenTrigger.Contains(currentMouseState.Position))
                {
                    screen = Screen.ownedRecords;
                }

      
                
            }
            else if (screen == Screen.ownedRecords)
            {
                //if (currentMouseState.LeftButton == ButtonState.Pressed && canadianAlbumRect.Contains(currentMouseState.Position))
                //{
                //    albumSelected = true;
                //    record = Record.canadianRock;
                //}
                //else
                //{
                //    albumSelected = false;
                //}

                if (currentMouseState.LeftButton == ButtonState.Pressed && ownedRecordsExit.Contains(currentMouseState.Position))
                {
                    screen = Screen.turntable;
                }

                if (NewClick() && canadianAlbumRect.Contains(currentMouseState.Position))
                {
                    albumSelected = true;
                    record = Record.canadianRock;
                }

                if (currentMouseState.LeftButton == ButtonState.Pressed &&
                    ownedRecordsExit.Contains(currentMouseState.Position))
                {
                    screen = Screen.turntable;
                }

                recordShown = albumSelected;
            }
            else if (screen == Screen.upgrades)
            {
                // TODO: Add your update logic for the upgrades screen here
                if (currentMouseState.LeftButton == ButtonState.Pressed && turntablePurchaseRect.Contains(currentMouseState.Position))
                {
                    if (gramps.money >= 300)
                    {
                        gramps.money -= 300;
                        gramps.turntableOwned = true;
                    }
                }
                if (currentMouseState.LeftButton == ButtonState.Pressed && cratePurchaseRect.Contains(currentMouseState.Position))
                {
                    if (gramps.money >= 100)
                    {
                        gramps.money -= 100;
                        gramps.RockCrateOwned = true;
                    }
                }
                if (currentMouseState.LeftButton == ButtonState.Pressed && postersPurchaseRect.Contains(currentMouseState.Position))
                {
                    if (gramps.money >= 25)
                    {
                        gramps.money -= 25;
                        gramps.PostersOwned = true;
                    }
                }
            }

            prevMouseState = currentMouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here


            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);


            if (screen == Screen.menu)
            {
                _spriteBatch.Draw(wallTexture, menuQuit, Color.White);
                _spriteBatch.Draw(wallTexture, menuHTP, Color.White);
                _spriteBatch.Draw(wallTexture, menuStart, Color.White);

                //Visible
                _spriteBatch.Draw(menuTexture, menuScreenRect, Color.White);
                
                
            }
            else if (screen == Screen.pause)
            {
                _spriteBatch.Draw(wallTexture, menuQuit, Color.White);
                _spriteBatch.Draw(wallTexture, menuHTP, Color.White);
                _spriteBatch.Draw(wallTexture, menuStart, Color.White);

                
                _spriteBatch.Draw(pauseTexture, menuScreenRect, Color.White);
            }
            else if (screen == Screen.store)
            {
                _spriteBatch.Draw(wallTexture, recordCrateBarrier1, Color.White);
                _spriteBatch.Draw(wallTexture, recordCrateBarrier2, Color.White);
                _spriteBatch.Draw(wallTexture, wallBarrier, Color.White);
                _spriteBatch.Draw(wallTexture, turntableBarrier, Color.White);
                _spriteBatch.Draw(wallTexture, turntableButtonTrigger, Color.White);
                _spriteBatch.Draw(wallTexture, cashRegisterCollision, Color.White);
                _spriteBatch.Draw(wallTexture, customerPayTrigger, Color.White);

                _spriteBatch.Draw(storeTexture, storeRect, Color.White);
                _spriteBatch.Draw(halfCashRegisterTexture, cashRegisterRect, Color.White);
                _spriteBatch.Draw(topCashRegisterTexture, cashRegisterRect, Color.White);


                //_spriteBatch.Draw(itemTextures, storeRect, Color.White);

                if (gameState == GameState.settingUp)
                {
                    _spriteBatch.DrawString(hudFont, "click upgrades button to get started", new Vector2(300, 0), Color.Green);
                }
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
                if (gramps.EssentialsCrateOwned)
                {
                    _spriteBatch.Draw(essentialCrateTexture, essentialRect, Color.White);
                }
                if (turntableSelectButton)
                {
                    _spriteBatch.Draw(turntableButton, turntableButtonRect, Color.White);
                }



                gramps.Draw(_spriteBatch);
                customerNPC.Draw(_spriteBatch);
                customerNPC2.Draw(_spriteBatch);

                _spriteBatch.Draw(doorsTexture, doorsRect, Color.White);
                _spriteBatch.Draw(topCashRegisterTexture, cashRegisterRect, Color.White);
                _spriteBatch.DrawString(hudFont, "Money: $" + gramps.money, new Vector2(0, 0), Color.Green);
                _spriteBatch.Draw(upgradesButton, upgradesRect, Color.White);
            }
            else if (screen == Screen.turntable)
            {
                _spriteBatch.Draw(wallTexture, recordBinScreenTrigger, Color.White);
                _spriteBatch.Draw(wallTexture, recordDestination, Color.White);
                _spriteBatch.Draw(turntableScreen, turntableRect, Color.White);
                _spriteBatch.Draw(turntableExitTexture, turntableExit, Color.White);
                //_spriteBatch.Draw(recordTexture, recordInPlace, Color.White);

                if (recordShown && !recordPlaced)
                {
                    _spriteBatch.Draw(centeredRecordTexture, centeredRecordRect, Color.White);
                }

                if (recordPlaced)
                {
                    _spriteBatch.Draw(recordTexture, recordInPlace, Color.White);
                    _spriteBatch.Draw(turntablePlayTexture, turntablePlay, Color.White);
                }
            }
            else if (screen == Screen.ownedRecords)
            {
                _spriteBatch.Draw(canadianRecordTexture, canadianAlbumRect, Color.White);
                _spriteBatch.Draw(turntableExitTexture, ownedRecordsExit, Color.White);
            }
            else if (screen == Screen.upgrades)
            {
                _spriteBatch.Draw(upgradesScreen, upgradesScreenRect, Color.White);
            }


                _spriteBatch.End();

            base.Draw(gameTime);
        }
        protected bool NewClick()
        {
            return currentMouseState.LeftButton == ButtonState.Pressed &&
            prevMouseState.LeftButton == ButtonState.Released;
        }
    }
}



