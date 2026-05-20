using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FINAL_PROJECT
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window

        List<Texture2D> grampsDownFrames = new List<Texture2D>();
        List<Texture2D> grampsUpFrames = new List<Texture2D>();
        List<Texture2D> grampsRightFrames = new List<Texture2D>();
        List<Texture2D> grampsLeftFrames = new List<Texture2D>();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
