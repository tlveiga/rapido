using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rapido;
using rapido.Shapes;
using rapido.Common;
using System;
using System.Collections.Generic;

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

        List<Body> _enemies = new List<Body>();

        rapido.Shapes.Rectangle player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected Body createBody()
        {
            rapido.Shapes.Rectangle body;
            body = new rapido.Shapes.Rectangle(new rapido.Common.Point(world.Size.X * (float)rnd.NextDouble(), world.Size.Y * (float)rnd.NextDouble()), world.Size.X * 0.1f, world.Size.X * 0.1f);
            body.Velocity = new rapido.Common.Vector((float)(rnd.NextDouble() * Math.PI * 2), 0.5f * world.Size.X);
            body.DidCollide += Body_DidCollide;
            body.Collide = true;
            body.CollideWithWorldBoundaries = true;
            body.Bounce = true;
            body.DidWorldCollide += Body_DidWorldCollide;

            body.CollisionGroups.Add("group");

            world.Groups[0].Add(body);

            return body;
        }

        private void Body_DidWorldCollide(object sender, BodyEventArgs args)
        {
        }

        private void Body_DidCollide(object sender, BodyEventArgs args)
        {
            return;
            if (sender == player)
            {
                (args.Target as Body).MarkDestroy = true;
                point++;
            }
        }

        protected Body fire()
        {
            rapido.Shapes.Rectangle body;
            body = new rapido.Shapes.Rectangle(player.Position, 10, 10);
            body.Velocity = new rapido.Common.Vector((float)(rnd.NextDouble() * Math.PI * 2), 0.52f * world.Size.X);
            body.CollideWithWorldBoundaries = true;
            body.DidWorldCollide += Fire_DidWorldCollide; 

            return body;
        }

        private void Fire_DidWorldCollide(object sender, BodyEventArgs args)
        {
            (sender as Body).MarkDestroy = true;
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
            world.Groups.Add(new Group("bullet"));

            player = (rapido.Shapes.Rectangle)createBody();
            player.CollisionGroups.Add("group");
            player.ID = "player";

            player.Velocity = Vector.Zero;
            world.Bodies.Add(player);

            base.Initialize();
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseState mouse = Mouse.GetState();
            rapido.Common.Point mousepoint = new rapido.Common.Point(mouse.X, mouse.Y);
            player.Velocity = new Vector(player.Position, mousepoint);

            if (mouse.LeftButton == ButtonState.Pressed)
                world.Bodies.Add(fire());

            if (world.Bodies.Count < 3)
            {
                Body body = createBody();
                body.BounceInWorldBoundaries = true;
                world.Bodies.Add(body);
            }
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
            GraphicsDevice.Clear(Color.Wheat);

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
