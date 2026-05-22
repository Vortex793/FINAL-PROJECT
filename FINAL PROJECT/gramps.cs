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
        int frameTimer, frame;
        public List<Texture2D> downFrames;
        public List<Texture2D> upFrames;
        public List<Texture2D> rightFrames;
        public List<Texture2D> leftFrames;
        public List<Texture2D> currentFrames;
        public gramps(List<Texture2D> down, List<Texture2D> up, List<Texture2D> right, List<Texture2D> left)
        {
            velocity = Vector2.Zero;
            speed = 2f;
            downFrames = down;
            upFrames = up;
            rightFrames = right;
            leftFrames = left;
            currentFrames = downFrames;
            position = new Rectangle(300, 200, 50, 50);
        }
        public void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();
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
                frameTimer += GameTime.ElapsedGameTime.TotalSeconds;

                if (frameTimer > 0.08)
                {
                    frame++;
                    frameTimer = 0;
                    frame = frame %
                }
                //DO FOUR
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
}
