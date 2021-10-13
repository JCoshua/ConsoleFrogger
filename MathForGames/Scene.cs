using System;
using System.Collections.Generic;
using System.Text;

namespace MathForGames
{
    class Scene
    {
        /// <summary>
        /// An Array containing all actors in a scene
        /// </summary>
        private static Actor[] _actors;
        private int _timer;
       
        public Scene()
        {
            _actors = new Actor[0];
        }

        /// <summary>
        /// Calls Start for every actor in the scene
        /// </summary>
        public virtual void Start()
        {
            Player player = new Player('x', 15, 28, 1, "Player", ConsoleColor.Green);
            AddActor(player);
            for (int j = 0; j < 29; j++)
            {
                Actor leftWall = new Actor('|', 0, j, 0, "Left Wall");
                Actor rightWall = new Actor('|', 29, j, 0, "Right Wall");
                AddActor(leftWall);
                AddActor(rightWall);
            }
            for (int p = 0; p < 30; p++)
            {
                Actor finish = new Actor('=', p, 0, 0);
                AddActor(finish);
            }
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Start();
        }

        /// <summary>
        /// Updates the actors in the scene
        /// Calls start for an actor if it hasn't already been called
        /// </summary>
        public virtual void Update()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i].Started)
                    _actors[i].Start();

                _actors[i].Update();

                //Checks for collision
                for (int j = 0; j < _actors.Length; j++)
                        if (_actors[i].Position == _actors[j].Position && i != j)
                            _actors[i].OnCollision(_actors[j]);

                if (_actors[i].ToBeRemoved)
                    RemoveActor(_actors[i]);
            }
            _timer++;
            if (_timer >= 6-Engine.Round && Engine.Round <= 5)
            {
                int location = new Random().Next(2, 27);
                int speed = new Random().Next(1, 3);
                Actor log = new Actor('O', 1, location, speed);
                AddActor(log);
                _timer = 0;
            }
            else if(_timer > 6-Engine.Round && Engine.Round > 5 && Engine.Round <= 10)
            {
                int location = new Random().Next(2, 27);
                int speed = new Random().Next(1, 3);
                Actor log1 = new Actor('O', 1, location, speed);
                Actor log2 = new Actor('O', 2, location, speed);
                AddActor(log1);
                AddActor(log2);
                _timer = 0;
            }
            else if (_timer > 6 - Engine.Round && Engine.Round > 10)
            {
                int logLenght = new Random().Next(2, 6);
                int location = new Random().Next(2, 27);
                int speed = new Random().Next(1, 3);
                for (int i = 0; i < logLenght; i++)
                {
                    Actor log = new Actor('O', i, location, speed);
                    AddActor(log);
                }
                _timer = 0;
            }
        }

        /// <summary>
        /// Draws every actor in the scene
        /// </summary>
        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Draw();
        }

        public virtual void End()
        {

        }

        /// <summary>
        /// Adds an actor to the scenes list of actors
        /// </summary>
        /// <param name="actor"></param>
        public void AddActor(Actor actor)
        {
            //Creates a temp array larger than the original
            Actor[] tempArray = new Actor[_actors.Length + 1];

            //Copies all values from the orginal array into the temp array
            for (int i = 0; i < _actors.Length; i++)
                tempArray[i] = _actors[i];
            //Adds the new actor to the end of the new array
            tempArray[_actors.Length] = actor;

            //Merges the arrays
            _actors = tempArray;
        }

        /// <summary>
        /// Removes the actor from the scene
        /// </summary>
        /// <param name="actor">The actor to remove</param>
        /// <returns>If the removal was successful</returns>
        public static bool RemoveActor(Actor actor)
        {
            //Creates a variable to store if the removal was successful
            bool actorRemoved = false;

            //Creates a new rray that is smaller than the original
            Actor[] tempArray = new Actor[_actors.Length - 1];

            //Copies all values from the orginal array into the temp array unless it is the removed actor
            int j = 0;
            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i] != actor)
                {
                    tempArray[j] = _actors[i];
                    j++;
                }
                else
                    actorRemoved = true;
            }  
            
            //Merges the arrays
            if(actorRemoved)
            _actors = tempArray;

            return actorRemoved;
        }
    }
}
