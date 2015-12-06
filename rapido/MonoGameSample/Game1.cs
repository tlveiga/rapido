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
        Texture2D _player;
        Random rnd = new Random();
        SpriteFont font;
        int point = 0;

        World world;
        Color body1color;
        Color body2color;

        rapido.Shapes.Rectangle player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected Body createBody()
        {
            rapido.Shapes.Rectangle body;
            body = new rapido.Shapes.Rectangle(world, new rapido.Common.Point(world.Size.X * (float)rnd.NextDouble(), world.Size.Y * (float)rnd.NextDouble()), world.Size.X * 0.1f, world.Size.X * 0.1f);
            body.Velocity = new rapido.Common.Vector((float)(rnd.NextDouble() * Math.PI * 2), 0.1f * world.Size.X);
            body.Collision += Body_Collision;
            body.Collide = true;
            body.CollisionGroups.Add("group");
            body.CollideWithWorldBoundaries = true;
            body.OutBounds += Body_OutBounds;

            world.Groups[0].Add(body);

            return body;
        }


        protected Body fire()
        {
            rapido.Shapes.Rectangle body;
            body = new rapido.Shapes.Rectangle(world, player.Position, 10, 10);
            body.Velocity = new rapido.Common.Vector((float)(rnd.NextDouble() * Math.PI * 2), 0.52f * world.Size.X);
            body.CollideWithWorldBoundaries = true;
            body.OutBounds += Fire_OutBounds;

            return body;
        }

        private void Fire_OutBounds(object sender, Box target)
        {
            (sender as Body).WillDestroy = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            world = new World(new rapido.Common.Point(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            world.Groups.Add(new Group("group"));

            player = (rapido.Shapes.Rectangle)createBody();
            player.Velocity = Vector.Zero;
            world.Bodies.Add(player);

            base.Initialize();
        }

        private void Body_OutBounds(object sender, Box target)
        {
            body2color = Color.Yellow;
        }

        private void Body_Collision(object sender, object target)
        {
            if (sender == player)
            {
                (target as Body).WillDestroy = true;
                point++;
            }
            return;
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
            _player = Content.Load<Texture2D>("player");
            font = Content.Load<SpriteFont>("font");
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

            MouseState mouse = Mouse.GetState();
            rapido.Common.Point mousepoint = new rapido.Common.Point(mouse.X, mouse.Y);
            player.Velocity = new Vector(player.Position, mousepoint);

            if (mouse.LeftButton == ButtonState.Pressed)
                world.Bodies.Add(fire());

            if (world.Bodies.Count < 3)
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

            spriteBatch.Draw(_player, new Microsoft.Xna.Framework.Rectangle((int)player.Bounds.Left, (int)player.Bounds.Top, (int)player.Width, (int)player.Height), Color.White);
            foreach (rapido.Shapes.Rectangle body in world.Bodies)
            {
                if (body == player) continue;
                spriteBatch.Draw(_square, new Microsoft.Xna.Framework.Rectangle((int)body.Bounds.Left, (int)body.Bounds.Top, (int)body.Width, (int)body.Height), Color.White);
            }

            spriteBatch.DrawString(font, point.ToString(), new Vector2(0, 0), Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
