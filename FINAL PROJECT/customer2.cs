using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_PROJECT
{
    internal class customer2
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
        public Texture2D upIdle2;

        private gramps player;
        private static Random rng = new Random();
        public customer2(gramps g)
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

        public customer2()
        {
            position = new Rectangle(340, 600, 35, 50);

            state = customerState.Entering;
            currentFrames = downFrames;

            BuildPathForState(state);
        }

        // ---------------- PATH SYSTEM ----------------

        void BuildPathForState(customerState newState)
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

                if (player.RockCrateOwned)
                    availableBins.Add(0);

                if (player.MetalCrateOwned)
                    availableBins.Add(1);

                if (player.HipHopCrateOwned)
                    availableBins.Add(2);

                if (player.JazzCrateOwned)
                    availableBins.Add(3);

                if (player.CanadianCrateOwned)
                    availableBins.Add(4);

                int selectedBin = availableBins[rng.Next(availableBins.Count)];

                switch (selectedBin)
                {
                    case 0: // Rock
                        path.Add(new MovementStep { Direction = MoveDirection.Left, Time = 1.8f });
                        path.Add(new MovementStep { Direction = MoveDirection.Up, Time = 1.1f });
                        break;

                    case 1: // Metal
                        path.Add(new MovementStep { Direction = MoveDirection.Left, Time = 1.8f });
                        path.Add(new MovementStep { Direction = MoveDirection.Up, Time = 2.0f });
                        break;

                    case 2: // Hip Hop
                        path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 1.8f });
                        path.Add(new MovementStep { Direction = MoveDirection.Up, Time = 1.1f });
                        break;

                    case 3: // Jazz
                        path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 1.8f });
                        path.Add(new MovementStep { Direction = MoveDirection.Up, Time = 2.0f });
                        break;

                    case 4: // Canadian
                        path.Add(new MovementStep { Direction = MoveDirection.Up, Time = 1.5f });
                        break;
                }
            }
            else if (newState == customerState.Browsing)
            {
                path.Add(new MovementStep { Direction = MoveDirection.Idle, Time = 3f });
            }
            else if (newState == customerState.ToCheckout)
            {
                path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 3f });
            }
            else if (newState == customerState.Paying)
            {
                waitTimer = 0f;
                path.Add(new MovementStep { Direction = MoveDirection.Idle, Time = 3f });
                

            }
            else if (newState == customerState.Leaving)
            {
                path.Add(new MovementStep { Direction = MoveDirection.Down, Time = 1f });
                path.Add(new MovementStep { Direction = MoveDirection.Left, Time = 1f });
                path.Add(new MovementStep { Direction = MoveDirection.Down, Time = 1f });
            }
            
                

        }

        //Move

        void MoveStep()
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

        // ---------------- ANIMATION ----------------

        void UpdateAnimation()
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

        // ---------------- STATE LOGIC ----------------

        void ArrivedAtDestination()
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
                state = customerState.ToCheckout;
            }
            else if (state == customerState.ToCheckout)
            {
                state = customerState.Leaving;
            }

            BuildPathForState(state);
        }

        // ---------------- UPDATE ----------------

        public void Update(GameTime gameTime)
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
                    state = customerState.Leaving;
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
                spriteBatch.Draw(upIdle2, position, Color.White);
            }
            else
            {
                spriteBatch.Draw(currentFrames[frame], position, Color.White);
            }
        }
    }
}

