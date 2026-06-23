using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_PROJECT
{
    internal class customer
    {

        enum customerState
        {
            Entering,
            FindingBin,
            Browsing,
            ToCheckout,
            Paying,
            Leaving,
        }

        enum MoveDirection
        {
            Up,
            Down,
            Left,
            Right,
            Idle
        }


        class MovementStep
        {
            public MoveDirection Direction;
            public float Time;
        }

        customerState state;

        MoveDirection facingDirection = MoveDirection.Down;
        public List<Texture2D> downFrames;
        public List<Texture2D> upFrames;
        public List<Texture2D> rightFrames;
        public List<Texture2D> leftFrames;
        public List<Texture2D> currentFrames;
        public Texture2D upIdle;

        public int selectedBin;
        public bool waitingForService
        {
            get { return state == customerState.Paying;}
        }
        public void ServeCustomer()
        {
            hasBeenServed = true;
            hasPaid = true;
            state = customerState.Leaving;
            BuildPathForState(state);
        }
        private gramps player;
        private static Random rng = new Random();
        public customer(gramps g)
        {
            player = g;

            position = new Rectangle(340, 600, 35, 50);

            state = customerState.Entering;
            BuildPathForState(state);
        }
        public bool hasPaid = false;
        public Rectangle position;

        float speed = 2f;

        int frame;
        float frameTimer;

        // NEW movement system
        List<MovementStep> path = new List<MovementStep>();
        int currentStep = 0;
        float stepTimer = 0f;

        MoveDirection currentDirection = MoveDirection.Idle;

        bool hasBeenServed = false;
        float waitTimer = 0f;
        float waitingTime = 15f;
        public bool countedAsMissed = false;

        public customer()
        {
            position = new Rectangle(340, 600, 35, 50);

            state = customerState.Entering;
            currentFrames = downFrames;

            BuildPathForState(state);
        }

       

        void BuildPathForState(customerState newState)          //paths for each record crate
        {
            path.Clear();
            currentStep = 0;
            stepTimer = 0f;
          
            if (newState == customerState.Entering)
            {
                path.Add(new MovementStep { Direction = MoveDirection.Up, Time = 2f });
            }
            else if (newState == customerState.FindingBin)
            {
                List<int> availableBins = new List<int>(1);

                if (player.RockCrateOwned && player.rockCrateStock > 0)
                    availableBins.Add(0);

                if (player.MetalCrateOwned && player.metalCrateStock > 0)
                    availableBins.Add(1);

                if (player.HipHopCrateOwned && player.hiphopCrateStock > 0)
                    availableBins.Add(2);

                if (player.JazzCrateOwned && player.jazzCrateStock > 0)
                    availableBins.Add(3);

                if (player.CanadianCrateOwned && player.jazzCrateStock > 0)
                    availableBins.Add(4);

                if (player.EssentialsCrateOwned && player.essentialCrateStock > 0)
                    availableBins.Add(5);


                if (availableBins.Count == 0)
                {
                    state = customerState.Leaving;
                    BuildPathForState(state);
                    return;
                }

                selectedBin = availableBins[rng.Next(availableBins.Count)];

                switch (selectedBin)
                {
                    case 0: // Rock
                        path.Add(new MovementStep { Direction = MoveDirection.Left, Time = 1.8f });
                        path.Add(new MovementStep { Direction = MoveDirection.Up, Time = 1.1f });
                        path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 0.50f });


                        break;

                    case 1: // Metal
                        path.Add(new MovementStep { Direction = MoveDirection.Left, Time = 1.8f });
                        path.Add(new MovementStep { Direction = MoveDirection.Up, Time = 1.1f });
                        path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 1.4f });
                        break;

                    case 2: // Hip Hop

                        path.Add(new MovementStep { Direction = MoveDirection.Left, Time = 1.8f });
                        path.Add(new MovementStep { Direction = MoveDirection.Up, Time = 1.1f });
                        path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 2.5f });
                        break;

                    case 3: // Jazz
                        path.Add(new MovementStep { Direction = MoveDirection.Left, Time = 1.3f });
                       
                        break;

                    case 4: // Canadian
                        path.Add(new MovementStep { Direction = MoveDirection.Left, Time = 0.50f });
                        break;

                    case 5: //Essential
                        path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 0.5f });
                        break;
                }
            }
            else if (newState == customerState.Browsing)
            {
                path.Add(new MovementStep { Direction = MoveDirection.Idle, Time = 3f });


            }
            else if (newState == customerState.ToCheckout)
            {
                //path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 3f });
                switch (selectedBin)
                {
                    case 0: // Rock
                        path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 3f });

                        break;

                    case 1: // Metal
                        path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 2f });
                        break;

                    case 2: // Hip Hop
                        path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 1.2f });
                        break;

                    case 3: // Jazz
                        path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 3f });
                        path.Add(new MovementStep { Direction = MoveDirection.Up, Time = 1.0f });
                        break;

                    case 4: // Canadian
                        path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 2.5f });
                        path.Add(new MovementStep { Direction = MoveDirection.Up, Time = 1.0f });
                        break;

                    case 5: //Essential
                        path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 1.0f });

                        path.Add(new MovementStep { Direction = MoveDirection.Up, Time = 1f });
                        break;
                }
            }
            else if (newState == customerState.Paying)
            {
                waitTimer = 0f;
                path.Add(new MovementStep { Direction = MoveDirection.Idle, Time = 3f });
                

            }
            else if (newState == customerState.Leaving)
            {
                path.Add(new MovementStep { Direction = MoveDirection.Down, Time = 2f });
                path.Add(new MovementStep { Direction = MoveDirection.Left, Time = 1f });
                path.Add(new MovementStep { Direction = MoveDirection.Down, Time = 1f });
            }
            
                

        }

     

        void MoveStep()    //movement
        {
            if (path.Count == 0 || currentStep >= path.Count)
                return;

            MovementStep step = path[currentStep];

            currentDirection = step.Direction;

            switch (step.Direction)
            {
                case MoveDirection.Up:
                    position.Y -= (int)speed;
                    break;

                case MoveDirection.Down:
                    position.Y += (int)speed;
                    break;

                case MoveDirection.Left:
                    position.X -= (int)speed;
                    break;

                case MoveDirection.Right:
                    position.X += (int)speed;
                    break;
            }

            stepTimer += 1f / 60f;

            if (stepTimer >= step.Time)
            {
                currentStep++;
                stepTimer = 0f;
            }

            if (currentStep >= path.Count)
            {
                ArrivedAtDestination();
            }
            currentDirection = step.Direction;

            if (currentDirection != MoveDirection.Idle)
            {
                facingDirection = currentDirection;
            }
        }

   

        void UpdateAnimation()           //animating the frames
        {
            switch (currentDirection)
            {
                case MoveDirection.Up:
                    currentFrames = upFrames;
                    break;

                case MoveDirection.Down:
                    currentFrames = downFrames;
                    break;

                case MoveDirection.Left:
                    currentFrames = leftFrames;
                    break;

                case MoveDirection.Right:
                    currentFrames = rightFrames;
                    break;
                case MoveDirection.Idle:
                    frame = 0;
                    break;
            }
        }

     

        void ArrivedAtDestination()   //customers state logic
        {
            if (state == customerState.Entering)
            {
                state = customerState.FindingBin;
            }
            else if (state == customerState.FindingBin)
            {
                state = customerState.Browsing;
            }
            else if (state == customerState.Browsing)
            {
                bool inStock = true;

                switch (selectedBin)
                {
                    case 0:
                        inStock = player.rockCrateStock > 0;
                        break;

                    case 1:
                        inStock = player.metalCrateStock > 0;
                        break;
                    case 2:
                        inStock = player.hiphopCrateStock > 0;
                        break;
                    case 3:
                        inStock = player.jazzCrateStock > 0;
                        break;
                    case 4:
                        inStock = player.canadianCrateStock > 0;
                        break;
                    case 5:
                        inStock = player.essentialCrateStock > 0;
                        break;

                    if (inStock)
                    {
                        state = customerState.ToCheckout;
                    }
                    else
                    {
                        state = customerState.Leaving;               //leaves if no stock
                    }
                }
                state = customerState.ToCheckout;
            }
            else if (state == customerState.ToCheckout)
            {
                state = customerState.Paying;
            }
            else if (state == customerState.Paying)
            {
                state = customerState.Leaving;
            }

            BuildPathForState(state);
        }
        


        public void Update(GameTime gameTime) //update
        {
            MoveStep();
            UpdateAnimation();

            if (currentDirection != MoveDirection.Idle)
            {
                frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (frameTimer >= 0.2f)
                {
                    frame++;
                    if (frame > 2) frame = 1;
                    frameTimer = 0f;
                }
            }
            else
            {
                frame = 0;
            }
            if (state == customerState.Paying)
            {
                waitTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // if too slow → leave, no money
                if (waitTimer >= waitingTime)
                {
                    countedAsMissed = true;
                    state = customerState.Leaving;
                    BuildPathForState(state);
                    return;
                }

                if (!hasBeenServed) 
                {

                }
            }

        }

        public Rectangle Hitbox
        {
            get { return position; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (state == customerState.Browsing || state == customerState.Paying)
            {
                spriteBatch.Draw(upIdle, position, Color.White);
            }
            else
            {
                spriteBatch.Draw(currentFrames[frame], position, Color.White);
            }
        }
    }
}

