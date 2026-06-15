using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FINAL_PROJECT
{
    class customer
    {
        enum customerState
        {
            Entering,
            FindingBin,
            Browsing,
            ToCheckout,
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

        public List<Texture2D> downFrames;
        public List<Texture2D> upFrames;
        public List<Texture2D> rightFrames;
        public List<Texture2D> leftFrames;
        public List<Texture2D> currentFrames;

        public Rectangle position;

        float speed = 2f;

        int frame;
        float frameTimer;

        // NEW movement system
        List<MovementStep> path = new List<MovementStep>();
        int currentStep = 0;
        float stepTimer = 0f;

        MoveDirection currentDirection = MoveDirection.Idle;

        public customer()
        {
            position = new Rectangle(300, 400, 35, 50);

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
                path.Add(new MovementStep { Direction = MoveDirection.Left, Time = 3f });
            }
            else if (newState == customerState.Browsing)
            {
                path.Add(new MovementStep { Direction = MoveDirection.Right, Time = 3f });
            }
            else if (newState == customerState.ToCheckout)
            {
                path.Add(new MovementStep { Direction = MoveDirection.Down, Time = 2f });
            }
            else if (newState == customerState.Leaving)
            {
                path.Add(new MovementStep { Direction = MoveDirection.Down, Time = 2f });
            }
        }

        // ---------------- MOVEMENT ----------------

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

            if (path.Count > 0)
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
        }

        // ---------------- DRAW ----------------

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentFrames[frame], position, Color.White);
        }
    }
}