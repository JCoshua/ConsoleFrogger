using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames
{
    class Player : Actor
    {
        private Vector2 _velocity;
        private static int _lives = 3;

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public static int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }

        public Player(char icon, float x, float y, float speed, string name = "Actor", ConsoleColor color = ConsoleColor.White)
            : base(icon, x, y, speed, name, color)
        {
            Speed = speed;
        }

        public override void Update()
        {
            Vector2 moveDirection = new Vector2();

            ConsoleKey keyPressed = Engine.GetNextKey();
            if (keyPressed == ConsoleKey.A)
                moveDirection = new Vector2 { x = -1 };

            if (keyPressed == ConsoleKey.D)
                moveDirection = new Vector2 { x = 1 };

            if (keyPressed == ConsoleKey.W)
                moveDirection = new Vector2 { y = -1 };

            if (keyPressed == ConsoleKey.S)
                moveDirection = new Vector2 { y = 1 };

            Velocity = moveDirection * Speed;
            Position += Velocity;
        }

        public override void OnCollision(Actor actor)
        {
            if (actor.Icon.Symbol == '=')
            {
                Engine.PlayerWin = true;
            }
            else if (actor.Icon.Symbol == '-')
            {
                Position += new Vector2 { x = 0, y = -1 };
            }
            else if(actor.Name == "Left Wall")
            {
                Position += new Vector2 { x = 1, y = 0 };
            }
            else if (actor.Name == "Right Wall")
            {
                Position -= new Vector2 { x = 1, y = 0 };
            }
        }
    }
}
