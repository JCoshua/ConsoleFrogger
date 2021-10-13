using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace MathForGames
{
    struct Icon
    {
        public char Symbol;
        public ConsoleColor Color;

    }

    class Actor
    {
        private Icon _icon;
        private string _name;
        private Vector2 _position;
        private bool _started;
        private float _speed;
        private bool _toBeRemoved;

        public bool Started
        {
            get { return _started; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public String Name
        {
            get { return _name; }
        }

        public Icon Icon
        {
           get { return _icon; }
        }

        public bool ToBeRemoved
        {
            get { return _toBeRemoved; }
        }
        public Actor(char icon, float x, float y, float speed, string name = "Actor", ConsoleColor color = ConsoleColor.White):
            this(icon, new Vector2 { x = x, y = y }, speed, name, color) {}

        public Actor(char icon, Vector2 position, float speed, string name = "Actor", ConsoleColor color = ConsoleColor.White)
        {
            _icon = new Icon { Symbol = icon, Color = color };
            _speed = speed;
            _position = position;
            _name = name;
        }

        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update()
        {
                _position.x += _speed;
            
        }

        public virtual void Draw()
        {
            Engine.Render(_icon, Position);
        }

        public virtual void End()
        {

        }

        public virtual void OnCollision(Actor actor)
        {
            if (actor.Icon.Symbol == '|')
            {
                _toBeRemoved = true;
            }
        }
    }
}
