using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace FINAL_PROJECT
{
    class gramps
    {
        private Rectangle position;

        float speed;
        bool playerWalking = false;
        Vector2 velocity;
        float frameTimer;
        int frame;
        public bool turntableOwned = true;
        public bool RockCrateOwned = true;
        public bool MetalCrateOwned = true;
        public bool HipHopCrateOwned = false;
        public bool JazzCrateOwned = false;
        public bool CanadianCrateOwned = true;
        public List<Texture2D> downFrames;
        public List<Texture2D> upFrames;
        public List<Texture2D> rightFrames;
        public List<Texture2D> leftFrames;
        public List<Texture2D> currentFrames;

        public int money = 500;
        public gramps(List<Texture2D> down, List<Texture2D> up, List<Texture2D> right, List<Texture2D> left)
        {
            velocity = Vector2.Zero;
            speed = 2f;
            downFrames = down;
            upFrames = up;
            rightFrames = right;
            leftFrames = left;
            currentFrames = downFrames;
            position = new Rectangle(300, 200, 35, 50);
          
        }
        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            playerWalking = false;
            
            velocity = Vector2.Zero;
            if (keyboard.IsKeyDown(Keys.W))
            {
                velocity.Y -= speed;
                currentFrames = upFrames;
                playerWalking = true;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                velocity.Y += speed;
                currentFrames = downFrames;
                playerWalking = true;
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                velocity.X -= speed;
                currentFrames = leftFrames;
                playerWalking = true;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                velocity.X += speed;
                currentFrames = rightFrames;
                playerWalking = true;
            }

            if (!(velocity.X != 0 && velocity.Y != 0))
            {
                position.X += (int)velocity.X;
                position.Y += (int)velocity.Y;
            }


            

            if (playerWalking)
            {
                frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (frameTimer > 0.20f)
                {
                    frame++;
                    if(frame > 2)
                    {
                        frame = 1;
                    }
                    frameTimer = 0f;
                   
                }
                //DO FOUR
            }
            else
            {
                frame = 0;
            }
        }
        public Rectangle Hitbox
        {
            get { return position; }
        }
        //public bool RockCrateOwned
        //{
        //    get { return RockCrateOwned; }
        //    set { RockCrateOwned = value; }
        //}
        public void MoveBack(Vector2 amount)
        {
            position.X -= (int)amount.X;
            position.Y -= (int)amount.Y;
        }
        public Vector2 Velocity
        {
            get { return velocity; }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentFrames.Count > 0)
            {
                spriteBatch.Draw(currentFrames[frame], position, Color.White);
            }


        }
    }
}
