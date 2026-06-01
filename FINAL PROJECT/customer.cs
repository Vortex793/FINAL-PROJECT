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
    class customer
    {
        public List<Texture2D> downFrames;
        public List<Texture2D> upFrames;
        public List<Texture2D> rightFrames;
        public List<Texture2D> leftFrames;

        public Rectangle position;

        public customer()
        {
            position = new Rectangle(300, 400, 35, 50);
        }

        public void Update(GameTime gameTime)
        {
           position.X += 1; 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(downFrames[0], position, Color.White);
        }
    }

}
