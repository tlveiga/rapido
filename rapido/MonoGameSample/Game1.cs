using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rapido;
using rapido.Shapes;
using rapido.Common;
using System;

namespace MonoGameSample
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics = null;
        SpriteBatch spriteBatch;
        Texture2D _square;
        Random rnd = new Random();

        World world;
        Color body1color;
        float speed;
        Color body2color;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected Body createBody()
        {
            rapido.Shapes.Rectangle body;
            body = new rapido.Shapes.Rectangle(world, new rapido.Common.Point(world.Size.X * (float)rnd.NextDouble(), world.Size.Y * (float)rnd.NextDouble()), world.Size.X * 0.01f, world.Size.X * 0.01f);
            body.Velocity = new rapido.Common.Vector((float)(rnd.NextDouble() * Math.PI * 2), speed);
            body.Collision += Body_Collision;
            body.Collide = true;
            body.CollisionGroups.Add("group");
            body.CollideWithWorldBoundaries = true;
            body.OutBounds += Body_OutBounds;

            world.Groups[0].Add(body);

            return body;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            speed = graphics.PreferredBackBufferWidth * 0.75f;

            world = new World(new rapido.Common.Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            world.Groups.Add(new Group("group"));

            base.Initialize();
        }

        private void Body_OutBounds(object sender, Box target)
        {
            body2color = Color.Yellow;
        }

        private void Body_Collision(object sender, object target)
        {
            (sender as Body).WillDestroy = true;
            (target as Body).WillDestroy = true;
            body2color = Color.White * 0.5f;
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _square = Content.Load<Texture2D>("square");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (world.Bodies.Count < 100)
                world.Bodies.Add(createBody());

            body1color = Color.White;
            body2color = Color.White;

            world.UpdateWorld((float)gameTime.TotalGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (rapido.Shapes.Rectangle body in world.Bodies)
            {
                spriteBatch.Draw(_square, new Microsoft.Xna.Framework.Rectangle((int)body.Bounds.Left, (int)body.Bounds.Top, (int)body.Width, (int)body.Height), body2color);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
