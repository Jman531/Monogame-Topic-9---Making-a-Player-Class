using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Monogame_Topic_9___Making_a_Player_Class
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        int player1Score, player2Score;

        Player amoebaPlayer1, amoebaPlayer2;

        Rectangle window;

        KeyboardState keyboardState;

        Texture2D amoebaTexture;
        Texture2D wallTexture;
        Texture2D foodTexture;

        List<Rectangle> barriers;
        List<Food> food;

        SpriteFont scoreFont;

        Random generator = new Random();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 800, 500);
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.ApplyChanges();

            barriers = new List<Rectangle>();
            barriers.Add(new Rectangle(100, 100, 10, 200));
            barriers.Add(new Rectangle(400, 400, 100, 10));

            player1Score = 0;
            player2Score = 0;

            base.Initialize();
            food = new List<Food>();
            for (int i = 0; i < 10; i++)
                food.Add(new Food(foodTexture, new Rectangle(generator.Next(0, 790), generator.Next(0, 490), 10, 10), new Vector2(2, -2)));

            amoebaPlayer1 = new Player(amoebaTexture, 10, 10);
            amoebaPlayer2 = new Player(amoebaTexture, 760, 460);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            amoebaTexture = Content.Load<Texture2D>("amoeba");
            wallTexture = Content.Load<Texture2D>("rectangle");
            foodTexture = Content.Load<Texture2D>("circle");
            scoreFont = Content.Load<SpriteFont>("Score");


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            keyboardState = Keyboard.GetState();

            amoebaPlayer1.HSpeed = 0;
            amoebaPlayer1.VSpeed = 0;

            amoebaPlayer2.HSpeed = 0;
            amoebaPlayer2.VSpeed = 0;

            if (keyboardState.IsKeyDown(Keys.D))
            {
                amoebaPlayer1.HSpeed = 3;
            }
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                amoebaPlayer1.HSpeed = -3;
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                amoebaPlayer1.VSpeed = -3;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                amoebaPlayer1.VSpeed = 3;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                amoebaPlayer2.HSpeed = 3;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                amoebaPlayer2.HSpeed = -3;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                amoebaPlayer2.VSpeed = -3;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                amoebaPlayer2.VSpeed = 3;
            }

            amoebaPlayer1.Update();
            amoebaPlayer2.Update();

            foreach (Food bit in food)
                bit.Move(window);

            amoebaPlayer1.Offscreen(window);
            amoebaPlayer2.Offscreen(window);

            foreach (Rectangle barrier in barriers)
                if (amoebaPlayer1.Collide(barrier))
                    amoebaPlayer1.UndoMove();

            foreach (Rectangle barrier in barriers)
                if (amoebaPlayer2.Collide(barrier))
                    amoebaPlayer2.UndoMove();

            foreach (Rectangle barrier in barriers)
                foreach (Food bit in food)
                    bit.Collide(barrier);

            for (int i = 0; i < food.Count; i++)
            {
                for (int j = 0; j < food.Count; j++)
                {
                    if (i != j)
                    {
                        food[i].Collide(food[j].Bounds);
                    }
                }
            }

            for (int i = 0; i < food.Count; i++)
                if (amoebaPlayer1.Collide(food[i].Bounds))
                {
                    amoebaPlayer1.Grow();
                    food.RemoveAt(i);
                    i--;
                    player1Score++;
                }

            for (int i = 0; i < food.Count; i++)
                if (amoebaPlayer2.Collide(food[i].Bounds))
                {
                    amoebaPlayer2.Grow();
                    food.RemoveAt(i);
                    i--;
                    player2Score++;
                }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            foreach (Rectangle barrier in barriers)
                _spriteBatch.Draw(wallTexture, barrier, Color.White);
            foreach (Food bit in food)
                _spriteBatch.Draw(bit.Texture, bit.Bounds, Color.White);
            amoebaPlayer1.Draw(_spriteBatch);
            amoebaPlayer2.Draw(_spriteBatch);
            _spriteBatch.DrawString(scoreFont, ("Player 1 Score: " + player1Score), new Vector2(0, 0), Color.Black);
            _spriteBatch.DrawString(scoreFont, ("Player 2 Score: " + player2Score), new Vector2(0, 25), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
