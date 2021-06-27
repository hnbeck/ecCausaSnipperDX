using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace UCausaDX
{ 
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D aGoal;
        Texture2D aStrategy;
        Texture2D aSolution;
        Texture2D background;
        SpriteFont symbol;
        Vector2 goalPos = new Vector2(100, 100);
        Vector2 oldGoalPos = new Vector2(180,60);
        Vector2 oldMousePos = Vector2.Zero;
        bool gDrag = false;
        Camera camera;
        float scrollValue = 0.0f;
        float vpScale = 1.0f; 

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            MouseState mState = Mouse.GetState();
            camera = new Camera(GraphicsDevice.Viewport);
            scrollValue = mState.ScrollWheelValue;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            aGoal = Content.Load<Texture2D>("goal");
            aStrategy = Content.Load<Texture2D>("strategy");
            aSolution = Content.Load<Texture2D>("solution");
            background = Content.Load<Texture2D>("background");
            symbol = Content.Load<SpriteFont>("symbol");
        }

        protected override void Update(GameTime gameTime)
        {
            Vector2 deltaPos = Vector2.Zero;
            Vector2 movement = Vector2.Zero;
            Vector2 mousePosition = new Vector2( 0,  0);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState keyState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();
            mousePosition = Vector2.Transform(mState.Position.ToVector2(), Matrix.Invert(camera.currentTransform));
            if (mState.LeftButton == ButtonState.Pressed)
            {
                
                Rectangle testRect =  aGoal.Bounds ;
              
                testRect.X = (int)goalPos.Y;
                testRect.Y = (int)goalPos.Y;

                if (!gDrag && testRect.Contains(mousePosition))
                {
                    gDrag = true;
                }
            }
            else
                gDrag = false;

            float delta = mState.ScrollWheelValue - scrollValue;
         
            if ( delta != 0)
            {
                scrollValue += delta; 
                vpScale += delta / 1000;
                camera.scaleVP(vpScale);
            }

            if (gDrag)
            {
                deltaPos = mousePosition - oldMousePos;
                movement += deltaPos;
            }
          

            if (keyState.IsKeyDown(Keys.D))
            {
      
            }

            if (keyState.IsKeyDown(Keys.A))
            {
 
            }

            // update positions
            goalPos += movement;
            oldGoalPos = goalPos;
            oldMousePos = mousePosition;

            base.Update(gameTime);
        }
       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            _spriteBatch.End(); 

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.currentTransform);
            _spriteBatch.Draw(aGoal, goalPos, Color.White);
            _spriteBatch.Draw(aStrategy, new Vector2(150, 10), Color.White);
            _spriteBatch.Draw(aSolution, new Vector2(10, 300), Color.White);
            _spriteBatch.DrawString(symbol, "Diedek", new Vector2(40, 140), Color.Blue);
            _spriteBatch.End();
           
            base.Draw(gameTime);
        }
    }
}
