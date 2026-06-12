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
        customerState state;

        public List<Texture2D> downFrames;
        public List<Texture2D> upFrames;
        public List<Texture2D> rightFrames;
        public List<Texture2D> leftFrames;
        public List<Texture2D> currentFrames;

        public Rectangle position;

        float speed;
        bool walking;
        int frame;
        float frameTimer;
        Vector2 destination;
        Vector2 velocity;

        public customer()
        {
            position = new Rectangle(300, 400, 35, 50);
            destination = new Vector2(300, 250);
            speed = 2f;
            state = customerState.Entering;
            currentFrames = downFrames;
        }

        private void MoveTowardsDestination()
        {
            Vector2 currentPos = new Vector2(position.X, position.Y);
            Vector2 direction = destination - currentPos;
            if (direction.Length() > 2f)
            {
                direction.Normalize();
                velocity = direction * speed;
                position.X += (int)(velocity.X);
                position.Y += (int)(velocity.Y);
                walking = true;

                UpdateAnimationDirection();
            }
            else
            {
                velocity = Vector2.Zero;
                walking = false;
                position.X = (int)destination.X;
                position.Y = (int)destination.Y;
                ArrivedAtDestination();
            }
        }
        private void UpdateAnimationDirection()
        {
            if (Math.Abs(velocity.X) > Math.Abs(velocity.Y))
            {
                if (velocity.X > 0)     //Right
                {
                    currentFrames = rightFrames;
                }
                else    //Left
                {
                    currentFrames = leftFrames;
                }

            }
            else
            {
                if (velocity.Y > 0)     //Down
                {
                    currentFrames = downFrames;
                }
                else    //Up
                {
                    currentFrames = upFrames;
                }
            }
        }
        private void ArrivedAtDestination()
        {
            if (state == customerState.Entering)
            {
                state = customerState.FindingBin;
                destination = new Vector2(100, 250);
            }
            else if (state == customerState.FindingBin)
            {
                state = customerState.Browsing;
                // Set a timer for browsing duration
            }
            else if (state == customerState.Browsing)
            {
                state = customerState.ToCheckout;
                destination = new Vector2(400, 250);
            }
            else if (state == customerState.ToCheckout)
            {
                state = customerState.Leaving;
                destination = new Vector2(300, 400);
            }
        }
        public void Update(GameTime gameTime)
        {
            MoveTowardsDestination();
            if (walking)
            {
                frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (frameTimer >= 0.20f)
                {
                    frame++;

                    if (frame > 2)
                    {
                        frame = 1;
                    }

                    frameTimer = 0f;
                }

            }
            else
            {
                frame = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currentFrames[frame], position, Color.White);
        }
    }

}
