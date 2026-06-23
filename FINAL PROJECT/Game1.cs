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
        gameOver
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














        List<Texture2D> customer1DownFrames = new List<Texture2D>();
        List<Texture2D> customer1UpFrames = new List<Texture2D>();
        List<Texture2D> customer1LeftFrames = new List<Texture2D>();
        List<Texture2D> customer1RightFrames = new List<Texture2D>();

        List<Texture2D> customer2DownFrames = new List<Texture2D>();
        List<Texture2D> customer2UpFrames = new List<Texture2D>();
        List<Texture2D> customer2LeftFrames = new List<Texture2D>();
        List<Texture2D> customer2RightFrames = new List<Texture2D>();

        List<Texture2D> customer3DownFrames = new List<Texture2D>();
        List<Texture2D> customer3UpFrames = new List<Texture2D>();
        List<Texture2D> customer3LeftFrames = new List<Texture2D>();
        List<Texture2D> customer3RightFrames = new List<Texture2D>();

        List<Texture2D> customer4DownFrames = new List<Texture2D>();
        List<Texture2D> customer4UpFrames = new List<Texture2D>();
        List<Texture2D> customer4LeftFrames = new List<Texture2D>();
        List<Texture2D> customer4RightFrames = new List<Texture2D>();

        Random rng = new Random();

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
        Song canadianRockAlbum, metalAlbum;
        Texture2D canadianRecordTexture, metalRecordTexture;
        Rectangle canadianAlbumRect = new Rectangle(50, 100, 300, 300);
        Rectangle metalAlbumRect = new Rectangle(450, 100, 300, 300);
        bool recordPlaying = false;

        MouseState currentMouseState;
        MouseState prevMouseState;
        Screen screen;
        Record record;
        GameState gameState;

        float xpTimer = 0f;
        Texture2D whiteXPBar;
        Rectangle whiteXPRect = new Rectangle(10, 550, 200, 30);
        Rectangle XPBarRect = new Rectangle(10, 550, 0, 30);
        

        Texture2D upIdle;
        Texture2D upIdle2;
        Texture2D downIdle;
        Texture2D rightIdle;
        Texture2D leftIdle;

        Texture2D upgradesButton, upgradesScreen;
        Rectangle upgradesRect = new Rectangle(600, 0, 200, 150); //button
        Rectangle upgradesScreenRect = new Rectangle(0, 0, 800, 600);

        Rectangle turntablePurchaseRect = new Rectangle(65, 400, 150, 100);
        Rectangle cratePurchaseRect = new Rectangle(325, 400, 150, 100);
        Rectangle postersPurchaseRect = new Rectangle(560, 400, 150, 100);
        
        Rectangle upgradesExitRect = new Rectangle(0, 0, 200, 150);
        Texture2D purchaseButtonTexture;

        Rectangle storeRect;
        Texture2D storeTexture, itemTextures, menuTexture, pauseTexture, postersTexture, howToPlayTexture;
        Rectangle window;

        KeyboardState keyboard;

        SpriteFont moneyFont, mainFont;
        List<Texture2D> grampsDownFrames = new List<Texture2D>();
        List<Texture2D> grampsUpFrames = new List<Texture2D>();
        List<Texture2D> grampsRightFrames = new List<Texture2D>();
        List<Texture2D> grampsLeftFrames = new List<Texture2D>();

        Rectangle menuStart, menuQuit, menuHTP;


        List<customer> customerSprites = new List<customer>();
        customer customerNPC;
        customer customerNPC2;
        Texture2D wallTexture, recordCrateBarrierTXR1, recordCrateBarrierTXR2;
        Rectangle wallBarrier, recordCrateBarrier1, recordCrateBarrier2, recordCrateBarrier3, recordCrateBarrier4, recordCrateBarrier5, recordCrateBarrier6, turntableBarrier;
        Rectangle restockTrigger1, restockTrigger2, restockTrigger3, restockTrigger4, restockTrigger5, restockTrigger6;
        Rectangle cashRegisterCollision;
        Rectangle recordBinScreenTrigger;
        Texture2D rockCrateTexture, metalCrateTexture, hiphopCrateTexture, jazzCrateTexture, canadianCrateTexture, essentialCrateTexture, cashRegisterTexture, halfCashRegisterTexture,topCashRegisterTexture, doorsTexture;
        Rectangle menuScreenRect = new Rectangle(0, 0, 800, 600);
        Rectangle howToPlayScreenRect = new Rectangle(0, 0, 800, 600);
        Rectangle rockRect = new Rectangle(0, 0, 800, 600);
        Rectangle metalRect = new Rectangle(0, 0, 800, 600);
        Rectangle hiphopRect = new Rectangle(0, 0, 800, 600);
        Rectangle jazzRect = new Rectangle(0, 0, 800, 600);
        Rectangle canadianRect = new Rectangle(0, 0, 800, 600);
        Rectangle essentialRect = new Rectangle(0, 0, 800, 600);
        
        Rectangle cashRegisterRect = new Rectangle(0, 0, 800, 600);
        Rectangle doorsRect = new Rectangle(0, 0, 800, 600);
        Rectangle customerPayTrigger = new Rectangle(550, 250, 100, 50);
        Rectangle cashRegisterTrigger = new Rectangle(510, 180, 140, 50);
        Rectangle postersRect = new Rectangle(0, 0, 800, 600);

        Texture2D xTexture;
        Rectangle XRect = new Rectangle(120, 172, 71, 71); //for turntable purchase
        Rectangle XRectPoster = new Rectangle(650, 172, 71, 71); //poster purchase


        //public int rockCrateStock = 3;            //moved to gramps class
        //public int metalCrateStock = 3;
        //public int hiphopCrateStock = 3;
        //public int jazzCrateStock = 3;
        //public int canadianCrateStock = 3;
        //public int essentialCrateStock = 3;
        float customerSpawnTime = 30f;
        float customerSpawnTimer = 0f;
        List<customer> customers = new List<customer>();
        int missedCustomers = 0;

        bool restock = false;
        Texture2D restockSign;
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
            recordCrateBarrier1 = new Rectangle(160, 158, 108, 43);
            recordCrateBarrier2 = new Rectangle(268, 158, 108, 43);
            recordCrateBarrier3 = new Rectangle(376, 158, 108, 43);

            recordCrateBarrier4 = new Rectangle(160, 315, 108, 43);
            recordCrateBarrier5 = new Rectangle(268, 315, 108, 43);
            recordCrateBarrier6 = new Rectangle(376, 315, 108, 43);


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
            customer customer2 = new customer(gramps);

            turntableTexture = Content.Load<Texture2D>("turntable");
            rockCrateTexture = Content.Load<Texture2D>("Rock Record Crate");
            metalCrateTexture = Content.Load<Texture2D>("Record Crate Metal");
            hiphopCrateTexture = Content.Load<Texture2D>("Record Crate Hip Hop");
            jazzCrateTexture = Content.Load<Texture2D>("Record Crate Jazz");
            canadianCrateTexture = Content.Load<Texture2D>("Record Crate Canadian");
            cashRegisterTexture = Content.Load<Texture2D>("Cash Register");
            halfCashRegisterTexture = Content.Load<Texture2D>("cash register half");
            topCashRegisterTexture = Content.Load<Texture2D>("fixed top cash register");
            doorsTexture = Content.Load<Texture2D>("doors");
            essentialCrateTexture = Content.Load<Texture2D>("essential record crate");
            upgradesButton = Content.Load<Texture2D>("upgrades");
            upgradesScreen = Content.Load<Texture2D>("upgrades screen");
            purchaseButtonTexture = Content.Load<Texture2D>("purchase button");
            postersTexture = Content.Load<Texture2D>("Record Store Posters ");
            xTexture = Content.Load<Texture2D>("X");


            //Customer 1 Sprites
            customer1DownFrames = new List<Texture2D>()
                {
                    Content.Load<Texture2D>("customer1IdleDown"),
                    Content.Load<Texture2D>("customer1Walk1Down"),
                    Content.Load<Texture2D>("customer1Walk2Down")
                };
            customer1UpFrames = new List<Texture2D>()
                {
                    Content.Load<Texture2D>("customer1IdleUp"),
                    Content.Load<Texture2D>("customer1Walk1Up"),
                    Content.Load<Texture2D>("customer1Walk2Up")
                };
            customer1RightFrames = new List<Texture2D>()
                {
                    Content.Load<Texture2D>("customer1IdleRight"),
                    Content.Load<Texture2D>("customer1Walk1Right"),
                    Content.Load<Texture2D>("customer1Walk2Right")
                };
            customer1LeftFrames = new List<Texture2D>()
                {
                    Content.Load<Texture2D>("customer1IdleLeft"),
                    Content.Load<Texture2D>("customer1Walk1Left"),
                    Content.Load<Texture2D>("customer1Walk2Left")
                };

            //Customer 2 Sprites 
            customer2DownFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer2IdleDown"),
                    Content.Load<Texture2D>("customer2Walk1Down"),
                    Content.Load<Texture2D>("customer2Walk2Down")
            };
            customer2UpFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer2IdleUp"),
                    Content.Load<Texture2D>("customer2Walk1Up"),
                    Content.Load<Texture2D>("customer2Walk2Up")
            };
            customer2RightFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer2IdleRight"),
                    Content.Load<Texture2D>("customer2Walk1Right"),
                    Content.Load<Texture2D>("customer2Walk2Right")
            };
            customer2LeftFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer2IdleLeft"),
                    Content.Load<Texture2D>("customer2Walk1Left"),
                    Content.Load<Texture2D>("customer2Walk2Left")
            };

            //Customer 3 Sprites
            customer3DownFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer3IdleDown"),
                    Content.Load<Texture2D>("customer3Walk1Down"),
                    Content.Load<Texture2D>("customer3Walk2Down")
            };
            customer3UpFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer3IdleUp"),
                    Content.Load<Texture2D>("customer3Walk1Up"),
                    Content.Load<Texture2D>("customer3Walk2Up")
            };
            customer3RightFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer3IdleRight"),
                    Content.Load<Texture2D>("customer3Walk1Right"),
                    Content.Load<Texture2D>("customer3Walk2Right")
            };
            customer3LeftFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer3IdleLeft"),
                    Content.Load<Texture2D>("customer3Walk1Left"),
                    Content.Load<Texture2D>("customer3Walk2Left")
            };

            //Customer 4 Sprites
            customer4DownFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer4IdleDown"),
                    Content.Load<Texture2D>("customer4Walk1Down"),
                    Content.Load<Texture2D>("customer4Walk2Down")
            };
            customer4UpFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer4IdleUp"),
                    Content.Load<Texture2D>("customer4Walk1Up"),
                    Content.Load<Texture2D>("customer4Walk2Up")
            };
            customer4RightFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer4IdleRight"),
                    Content.Load<Texture2D>("customer4Walk1Right"),
                    Content.Load<Texture2D>("customer4Walk2Right")
            };
            customer4LeftFrames = new List<Texture2D>()
            {
                    Content.Load<Texture2D>("customer4IdleLeft"),
                    Content.Load<Texture2D>("customer4Walk1Left"),
                    Content.Load<Texture2D>("customer4Walk2Left")
            };

            customer1.upIdle = Content.Load<Texture2D>("customer1IdleUp");
            customer1.currentFrames = customer1.downFrames;
            customer2.currentFrames = customer2.downFrames;
            downIdle = Content.Load<Texture2D>("customer1IdleDown");
            rightIdle = Content.Load<Texture2D>("customer1IdleRight");
            leftIdle = Content.Load<Texture2D>("customer1IdleLeft");
            customerSprites.Add(customer1);
            //customerNPC = customer1;
            //customerNPC2 = customer2;
            
            storeTexture = Content.Load<Texture2D>("walls");
            menuTexture = Content.Load<Texture2D>("record game menu");
            pauseTexture = Content.Load<Texture2D>("record game pause");
            howToPlayTexture = Content.Load<Texture2D>("how to play");
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
            metalAlbum = Content.Load<Song>("MetalAlbum");
            canadianRecordTexture = Content.Load<Texture2D>("Canadian Rock");
            metalRecordTexture = Content.Load<Texture2D>("Metal Album Cover");

            moneyFont = Content.Load<SpriteFont>("font");
            mainFont = Content.Load<SpriteFont>("Remaing Albums font size");
            restockSign = Content.Load<Texture2D>("restock sign");
            whiteXPBar = Content.Load<Texture2D>("white");
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            keyboard = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            //this.Window.Title = currentMouseState.Position.ToString();
            
            if (MediaPlayer.State == MediaState.Playing)
            {
                xpTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (xpTimer  >= 25f)
                {
                    gramps.xp += 25;
                    xpTimer = 0f;
                    XPBarRect.Width += 50;
                }
            }
            //Leveling up
            if (gramps.xp >= gramps.level * 100)
            {
                gramps.xp -= gramps.level * 100;
                gramps.level++;
                gramps.money += 100;
            }
            XPBarRect.Width = (int)((gramps.xp / (float)(gramps.level * 100)) * 200);




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
                if (keyboard.IsKeyDown(Keys.Space)) //dosent work
                {
                    screen = Screen.pause;
                }

                if (currentMouseState.LeftButton == ButtonState.Pressed && upgradesRect.Contains(currentMouseState.Position))
                {
                    screen = Screen.upgrades;
                }

                //First 3 crates and wall
                if (gramps.Hitbox.Intersects(wallBarrier) || gramps.Hitbox.Intersects(recordCrateBarrier1) && gramps.RockCrateOwned|| gramps.Hitbox.Intersects(recordCrateBarrier2) && gramps.MetalCrateOwned|| gramps.Hitbox.Intersects(recordCrateBarrier3) && gramps.HipHopCrateOwned)
                {
                    gramps.MoveBack(gramps.Velocity);
                }
                //Last 3 crates
                if (gramps.Hitbox.Intersects(recordCrateBarrier4) && gramps.JazzCrateOwned|| gramps.Hitbox.Intersects(recordCrateBarrier5) && gramps.CanadianCrateOwned|| gramps.Hitbox.Intersects(recordCrateBarrier6) && gramps.EssentialsCrateOwned)
                {
                    gramps.MoveBack(gramps.Velocity);
                }

                if (gramps.Hitbox.Intersects(cashRegisterCollision))
                {
                    gramps.MoveBack(gramps.Velocity);
                }

                if (gramps.Hitbox.Intersects(turntableBarrier) && gramps.turntableOwned)//Collision if the turntable is owned
                {
                    gramps.MoveBack(gramps.Velocity);


                }
                if (gramps.Hitbox.Intersects(turntableButtonTrigger) && gramps.turntableOwned)//Turntable trigger
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
                if (gramps.RockCrateOwned && gramps.turntableOwned)
                {
                    gameState = GameState.openingStore;
                }
                if (gameState == GameState.openingStore)
                {
                    //customerNPC.Update(gameTime);
                    //customerNPC2.Update(gameTime);
                    //if (customerNPC2.waitingForService && gramps.Hitbox.Intersects(cashRegisterTrigger) && !customerNPC2.hasPaid)
                    //{

                    //gramps.money += 40;
                    //switch (customerNPC2.selectedBin)
                    //{
                    //    case 0:
                    //        rockCrateStock--;
                    //    break;

                    //case 1:
                    //    metalCrateStock--;
                    //    break;

                    //case 2:
                    //    hiphopCrateStock--;
                    //    break;

                    //case 3:
                    //    jazzCrateStock--;
                    //    break;

                    //case 4:
                    //    canadianCrateStock--;
                    //        break;
                    //    case 5:
                    //        essentialCrateStock--;
                    //        break;
                    //}
                    //customerNPC2.ServeCustomer();
                    //}

                    foreach (var c in customers)
                    {
                        if (c.waitingForService &&
                            gramps.Hitbox.Intersects(cashRegisterTrigger) &&
                            !c.hasPaid)
                        {
                            gramps.money += 40;

                            switch (c.selectedBin)
                            {
                                case 0:
                                    gramps.rockCrateStock--;
                                    break;

                                case 1:
                                    gramps.metalCrateStock--;
                                    break;

                                case 2:
                                    gramps.hiphopCrateStock--;
                                    break;

                                case 3:
                                    gramps.jazzCrateStock--;
                                    break;

                                case 4:
                                    gramps.canadianCrateStock--;
                                    break;

                                case 5:
                                    gramps.essentialCrateStock--;
                                    break;
                            }

                            c.ServeCustomer();
                        }
                    }
                    customerSpawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    customerSpawnTime = Math.Max(2f, 20f - gramps.level);

                    //Window.Title = customerSpawnTimer.ToString();
                    if (customerSpawnTimer >= customerSpawnTime)
                    {

                        // create a customer
                        customer newCustomer = new customer(gramps);

                        int type = rng.Next(4);
                        customerSpawnTimer = 0f;
                        if (type == 0)//Customer 1
                        {
                            newCustomer.downFrames = customer1DownFrames;
                            newCustomer.upFrames = customer1UpFrames;
                            newCustomer.leftFrames = customer1LeftFrames;
                            newCustomer.rightFrames = customer1RightFrames;

                            newCustomer.upIdle = customer1UpFrames[0];
                        }
                        else if (type == 1)//Customer 2
                        {
                            newCustomer.downFrames = customer2DownFrames;
                            newCustomer.upFrames = customer2UpFrames;
                            newCustomer.leftFrames = customer2LeftFrames;
                            newCustomer.rightFrames = customer2RightFrames;

                            newCustomer.upIdle = customer2UpFrames[0];
                        }
                        else if (type == 2)//Customer 3
                        {
                            newCustomer.downFrames = customer3DownFrames;
                            newCustomer.upFrames = customer3UpFrames;
                            newCustomer.leftFrames = customer3LeftFrames;
                            newCustomer.rightFrames = customer3RightFrames;

                            newCustomer.upIdle = customer3UpFrames[0];
                        }
                        else if (type == 3)//Customer 4
                        {
                            newCustomer.downFrames = customer4DownFrames;
                            newCustomer.upFrames = customer4UpFrames;
                            newCustomer.leftFrames = customer4LeftFrames;
                            newCustomer.rightFrames = customer4RightFrames;

                            newCustomer.upIdle = customer4UpFrames[0];
                        }
                        newCustomer.currentFrames = newCustomer.downFrames;

                        customers.Add(newCustomer);
                    }
                    foreach (var c in customers)
                    {
                        c.Update(gameTime);

                        if (c.countedAsMissed)
                        {
                            missedCustomers++;
                            c.countedAsMissed = false;
                        }
                    }



                }
                if (gramps.RockCrateOwned && gramps.MetalCrateOwned && gramps.HipHopCrateOwned && gramps.JazzCrateOwned && gramps.CanadianCrateOwned && gramps.EssentialsCrateOwned)
                {
                    screen = Screen.gameOver;
                }

                if (gramps.rockCrateStock == 0 || gramps.metalCrateStock == 0 || gramps.hiphopCrateStock == 0 || gramps.jazzCrateStock == 0 || gramps.canadianCrateStock == 0 || gramps.essentialCrateStock == 0 && !restock)
                {
                    restock = true;
                }

                //Rock
                if (currentMouseState.LeftButton == ButtonState.Pressed && recordCrateBarrier1.Contains(currentMouseState.Position) && restock)
                {
                    gramps.money -= 50;
                    gramps.rockCrateStock = 3;
                    restock = false;
                }


                //Metal
                if (currentMouseState.LeftButton == ButtonState.Pressed && recordCrateBarrier2.Contains(currentMouseState.Position) && restock)
                {
                    gramps.money -= 50;
                    gramps.metalCrateStock = 3;
                    restock = false;
                }

                //HipHop
                if (currentMouseState.LeftButton == ButtonState.Pressed && recordCrateBarrier3.Contains(currentMouseState.Position) && restock)
                {
                    gramps.money -= 50;
                    gramps.hiphopCrateStock = 3;
                    restock = false;
                }

                //Jazz
                if (currentMouseState.LeftButton == ButtonState.Pressed && recordCrateBarrier4.Contains(currentMouseState.Position) && restock)
                {
                    gramps.money -= 50;
                    gramps.jazzCrateStock = 3;
                    restock = false;
                }

                //Canadian
                if (currentMouseState.LeftButton == ButtonState.Pressed && recordCrateBarrier5.Contains(currentMouseState.Position) && restock)
                {
                    gramps.money -= 50;
                    gramps.canadianCrateStock = 3;
                    restock = false;
                }

                //Essential
                if (currentMouseState.LeftButton == ButtonState.Pressed && recordCrateBarrier6.Contains(currentMouseState.Position) && restock)
                {
                    gramps.money -= 50;
                    gramps.essentialCrateStock = 3;
                    restock = false;
                }
                //if (customerNPC.Hitbox.Intersects(customerPayTrigger) && !customerNPC.hasPaid)
                //{
                //    gramps.money += 40;
                //    customerNPC.hasPaid = true;
                //}
                //if (customerNPC2.Hitbox.Intersects(customerPayTrigger) && !customerNPC2.hasPaid)
                //{
                //    gramps.money += 40;
                //    customerNPC2.hasPaid = true;
                //}

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
                //play button for canadian album
                if (currentMouseState.LeftButton == ButtonState.Pressed && turntablePlay.Contains(currentMouseState.Position) && record == Record.canadianRock && albumSelected)
                {
                    MediaPlayer.Play(canadianRockAlbum);
                    MediaPlayer.Volume = 0.25f;
                }

                //play button for metal album
                if (currentMouseState.LeftButton == ButtonState.Pressed && turntablePlay.Contains(currentMouseState.Position) && record == Record.Metal && albumSelected)
                {
                    MediaPlayer.Play(metalAlbum);
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

                if (NewClick() && metalAlbumRect.Contains(currentMouseState.Position))
                {
                    albumSelected = true;
                    record = Record.Metal;
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
                //Turntable purchase
                if (currentMouseState.LeftButton == ButtonState.Pressed && turntablePurchaseRect.Contains(currentMouseState.Position))
                {
                    if (!gramps.turntableOwned && gramps.money >= 300)
                    {
                        gramps.money -= 300;
                        gramps.turntableOwned = true;
                    }
                }
                //Crate purchase
                if (NewClick() && cratePurchaseRect.Contains(currentMouseState.Position))
                {
                    if (gramps.money >= 100)
                    {
                        switch (gramps.crateOrder)
                        {
                            case 0://Rock Record Purchase
                                gramps.money -= 100;
                                gramps.RockCrateOwned = true;
                                gramps.crateOrder++;
                                break;
                            case 1:
                                gramps.money -= 100;
                                gramps.MetalCrateOwned = true; 
                                gramps.crateOrder++;
                                break;
                            case 2:
                                gramps.money -= 100;
                                gramps.HipHopCrateOwned = true;
                                gramps.crateOrder++ ; 
                                break;
                            case 3:
                                gramps.money -= 100;
                                gramps.JazzCrateOwned = true;
                                gramps.crateOrder++;
                                break;
                            case 4:
                                gramps.money -= 100;
                                gramps.CanadianCrateOwned = true;
                                gramps.crateOrder++;
                                break;
                            case 5:
                                gramps.money -= 100;
                                gramps.EssentialsCrateOwned = true;
                                gramps.crateOrder++;
                                break;
                        }
                    }
                }
                //Posters purchase
                if (currentMouseState.LeftButton == ButtonState.Pressed && postersPurchaseRect.Contains(currentMouseState.Position))
                {
                    if (!gramps.PostersOwned && gramps.money >= 20)
                    {
                        gramps.money -= 20;
                        gramps.PostersOwned = true;
                    }
                }

                if (currentMouseState.LeftButton == ButtonState.Pressed && upgradesExitRect.Contains(currentMouseState.Position))
                {
                    screen = Screen.store;
                }
            }
            else if (screen == Screen.gameOver)
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && upgradesExitRect.Contains(currentMouseState.Position))
                {
                    screen = Screen.menu;
                }
            }
            else if (screen == Screen.howToPlay)
            {
                if (currentMouseState.LeftButton == ButtonState.Pressed && menuStart.Contains(currentMouseState.Position))
                {
                    screen = Screen.menu;
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

                
                
                
                
                
                

                _spriteBatch.Draw(wallTexture, wallBarrier, Color.White);
                if (gramps.turntableOwned)
                {
                    _spriteBatch.Draw(wallTexture, turntableBarrier, Color.White);
                }
                _spriteBatch.Draw(wallTexture, customerPayTrigger, Color.White);
                _spriteBatch.Draw(wallTexture, cashRegisterTrigger, Color.White);
                _spriteBatch.Draw(wallTexture, turntableButtonTrigger, Color.White);
                _spriteBatch.Draw(wallTexture, cashRegisterCollision, Color.White);
                //put customer pay trigger back here

                _spriteBatch.Draw(storeTexture, storeRect, Color.White);
                _spriteBatch.Draw(halfCashRegisterTexture, cashRegisterRect, Color.White);
                _spriteBatch.Draw(topCashRegisterTexture, cashRegisterRect, Color.White);




                //_spriteBatch.Draw(itemTextures, storeRect, Color.White);

                if (gameState == GameState.settingUp)
                {
                    _spriteBatch.DrawString(mainFont, "CLICK UPGRADES TO GET STARTED", new Vector2(100, 100), Color.Red);
                }
                if (gramps.PostersOwned)
                {
                    _spriteBatch.Draw(postersTexture, postersRect, Color.White);
                }
                if (gramps.turntableOwned)
                {
                    _spriteBatch.Draw(turntableTexture, turntableRect, Color.White);
                }
                if (gramps.RockCrateOwned)
                {
                    _spriteBatch.Draw(wallTexture, recordCrateBarrier1, Color.White);
                    _spriteBatch.Draw(rockCrateTexture, rockRect, Color.White);
                    _spriteBatch.DrawString(mainFont, "Albums=" +   gramps.rockCrateStock, new Vector2(160, 200), Color.Green);

                    //Restock sign
                    if (gramps.rockCrateStock == 0 && restock)
                    {
                        _spriteBatch.Draw(restockSign, recordCrateBarrier1, Color.White);
                    }

                }
                if (gramps.MetalCrateOwned)
                {
                    _spriteBatch.Draw(wallTexture, recordCrateBarrier2, Color.White);
                    _spriteBatch.Draw(metalCrateTexture, metalRect, Color.White);
                    _spriteBatch.DrawString(mainFont, "Albums=" +   gramps.metalCrateStock, new Vector2(270, 200), Color.Green);//adjust vector2 to

                    //Restock sign
                    if (gramps.metalCrateStock == 0 && restock)
                    {
                        _spriteBatch.Draw(restockSign, recordCrateBarrier2, Color.White);
                    }
                }
                if (gramps.HipHopCrateOwned)
                {
                    _spriteBatch.Draw(wallTexture, recordCrateBarrier3, Color.White);
                    _spriteBatch.Draw(hiphopCrateTexture, hiphopRect, Color.White);
                    _spriteBatch.DrawString(mainFont, "Albums=" + gramps.hiphopCrateStock, new Vector2(380, 200), Color.Green);

                    //Restock sign
                    if (gramps.hiphopCrateStock == 0 && restock)
                    {
                        _spriteBatch.Draw(restockSign, recordCrateBarrier3, Color.White);
                    }
                }
                if (gramps.JazzCrateOwned)
                {
                    _spriteBatch.Draw(wallTexture, recordCrateBarrier4, Color.White);
                    _spriteBatch.Draw(jazzCrateTexture, jazzRect, Color.White);
                    _spriteBatch.DrawString(mainFont, "Albums=" + gramps.jazzCrateStock, new Vector2(160, 360), Color.Green);

                    //Restock sign
                    if (gramps.jazzCrateStock == 0 && restock)
                    {
                        _spriteBatch.Draw(restockSign, recordCrateBarrier4, Color.White);
                    }

                }
                if (gramps.CanadianCrateOwned)
                {
                    _spriteBatch.Draw(wallTexture, recordCrateBarrier5, Color.White);
                    _spriteBatch.Draw(canadianCrateTexture, canadianRect, Color.White);
                    _spriteBatch.DrawString(mainFont, "Albums=" + gramps.canadianCrateStock, new Vector2(270, 360), Color.Green);

                    //Restock sign
                    if (gramps.canadianCrateStock == 0 && restock)
                    {
                        _spriteBatch.Draw(restockSign, recordCrateBarrier5, Color.White);
                    }
                }
                if (gramps.EssentialsCrateOwned)
                {
                    _spriteBatch.Draw(wallTexture, recordCrateBarrier6, Color.White);
                    _spriteBatch.Draw(essentialCrateTexture, essentialRect, Color.White);
                    _spriteBatch.DrawString(mainFont, "Albums=" + gramps.essentialCrateStock, new Vector2(380, 360), Color.Green);

                    //Restock sign
                    if (gramps.essentialCrateStock == 0 && restock)
                    {
                        _spriteBatch.Draw(restockSign, recordCrateBarrier6, Color.White);
                    }
                }
                if (turntableSelectButton)
                {
                    _spriteBatch.Draw(turntableButton, turntableButtonRect, Color.White);
                }




                gramps.Draw(_spriteBatch);
                //customerNPC.Draw(_spriteBatch);

                //customerNPC2.Draw(_spriteBatch);
                foreach (var c in customers)
                {
                    c.Draw(_spriteBatch);
                }
                _spriteBatch.Draw(doorsTexture, doorsRect, Color.White);
                _spriteBatch.Draw(topCashRegisterTexture, cashRegisterRect, Color.White);
                _spriteBatch.DrawString(moneyFont, "Money: $" + gramps.money, new Vector2(250, 0), Color.Green);
                _spriteBatch.Draw(upgradesButton, upgradesRect, Color.White);
                




                _spriteBatch.Draw(whiteXPBar, whiteXPRect, Color.White);
                _spriteBatch.Draw(whiteXPBar, XPBarRect, Color.Green);
                _spriteBatch.DrawString(mainFont, "Level: " + gramps.level, new Vector2(10, 550), Color.Black);

                _spriteBatch.Draw(wallTexture, restockTrigger1, Color.White);
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
                _spriteBatch.Draw(metalRecordTexture, metalAlbumRect, Color.White);
                _spriteBatch.Draw(turntableExitTexture, ownedRecordsExit, Color.White);
            }
            else if (screen == Screen.upgrades)
            {
                _spriteBatch.Draw(upgradesScreen, upgradesScreenRect, Color.White);
                _spriteBatch.Draw(turntableExitTexture, upgradesExitRect, Color.White);

                _spriteBatch.Draw(purchaseButtonTexture, turntablePurchaseRect, Color.White);
                _spriteBatch.Draw(purchaseButtonTexture, cratePurchaseRect, Color.White);
                _spriteBatch.Draw(purchaseButtonTexture, postersPurchaseRect, Color.White);
                if (!gramps.turntableOwned && !gramps.RockCrateOwned)
                {
                    _spriteBatch.DrawString(mainFont, "PURCHASE THE RECORD PLAYER AND ", new Vector2(200, 500), Color.Red);
                    _spriteBatch.DrawString(mainFont, "1 RECORD CRATE TO OPEN THE STORE", new Vector2(200, 520), Color.Red);
                }
                if (gramps.turntableOwned)
                {
                    _spriteBatch.Draw(xTexture, XRect, Color.White);
                }
                if (gramps.PostersOwned)
                {
                    _spriteBatch.Draw(xTexture, XRectPoster, Color.White);
                }
                _spriteBatch.DrawString(moneyFont, "Money: $" + gramps.money, new Vector2(500, 0), Color.Green);
            }
            else if (screen == Screen.gameOver)
            {
                _spriteBatch.DrawString(mainFont, "YOU WON", new Vector2(250, 200), Color.Red);

                _spriteBatch.DrawString(mainFont, "ALL CRATES UNLOCKED", new Vector2(220, 260), Color.White);
            }
            else if (screen == Screen.howToPlay)
            {
                _spriteBatch.Draw(howToPlayTexture, howToPlayScreenRect, Color.White);
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



