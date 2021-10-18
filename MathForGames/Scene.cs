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
        private static Actor[] _UIElements;
        private int _timer;
       
        public Scene()
        {
            _actors = new Actor[0];
            _UIElements = new Actor[0];
        }

        /// <summary>
        /// Calls Start for every actor in the scene
        /// </summary>
        public virtual void Start()
        {
            //Creates a new instance of Player
            Player player = new Player('x', 59, 28, 1, "Player", ConsoleColor.Green);
            //Creates a new instance of the UI
            UIText roundDisplay = new UIText(30, 3, "Display Text", ConsoleColor.White, 10, 10, "Round: " + Engine.Round);
            UIText livesDisplay = new UIText(30, 4, "Display Text", ConsoleColor.White, 10, 10, "Lives: " + Player.Lives);

            //Adds The Player and UI Display to the scene
            AddActor(player);
            AddUIElement(roundDisplay);
            AddUIElement(livesDisplay);

            //Creates the Walls
            for (int j = 0; j < 29; j++)
            {
                Actor leftWall = new Actor('|', 44, j, 0, "Left Wall");
                Actor rightWall = new Actor('|', 74, j, 0, "Right Wall");
                AddActor(leftWall);
                AddActor(rightWall);
            }

            //Creates the Finish Line
            for (int p = 44; p < 75; p++)
            {
                Actor finish = new Actor('=', p, 0, 0);
                AddActor(finish);
            }

            //Starts all of the actors
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Start();
        }

        /// <summary>
        /// Updates the actors in the scene
        /// Calls start for an actor if it hasn't already been called
        /// </summary>
        public virtual void Update()
        {
            //Updates each actor
            for (int i = 0; i < _actors.Length; i++)
            {
                //If actor hasn't started
                if (!_actors[i].Started)
                    //Run its start function
                    _actors[i].Start();

                //Update the current actor
                for (int s = 0; s < _actors[i].Speed; s++)
                {
                    if (Engine.PlayerHitLog)
                        return;

                    _actors[i].Update();
                    for (int j = 0; j < _actors.Length; j++)
                    {
                        if (_actors[i].Position == _actors[j].Position && i != j)
                            //Checks for collision
                            _actors[i].OnCollision(_actors[j]);
                    }
                }
                
                //If the actor needs to be removed
                if (_actors[i].ToBeRemoved)
                    //Remove the Actor
                    RemoveActor(_actors[i]);
            }
            //Increase the timer
            _timer++;
            //If the Timer finishes during the first 5 rounds
            if (_timer >= 3)
            {
                //Set the Location of the obstacle
                int location = new Random().Next(2, 27);

                //Randomize the Speed from 1 to 3
                int speed = new Random().Next(1, 3);

                if (Engine.Round <= 5)
                {
                    //Creates a new log at the random location, with the speed random
                    Actor log = new Actor('O', 45, location, speed);

                    //Adds the log to the scene
                    AddActor(log);
                }
                //If the Timer finishes during the rounds between six and ten
                else if (Engine.Round > 5 && Engine.Round <= 10)
                {
                    //Creates 2 new logs at the same random location, with same speed
                    Actor log1 = new Actor('O', 45, location, speed);
                    Actor log2 = new Actor('O', 46, location, speed);

                    //Adds the Logs to the scene
                    AddActor(log1);
                    AddActor(log2);
                }
                //If the Timer finishes after round 10;
                else if (Engine.Round > 10)
                {
                    int logLength = new Random().Next(2, 4);
                    for (int i = 0; i < logLength; i++)
                    {
                        Actor log = new Actor('O', i + 45, location, speed);
                        AddActor(log);
                    }
                }

                //Resets the Timer
                _timer = 0;
            }
        }

        public virtual void UpdateUI()
        {
            for (int i = 0; i < _UIElements.Length; i++)
            {
                if (!_UIElements[i].Started)
                    _UIElements[i].Start();

                _UIElements[i].Update();
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

        public virtual void DrawUI()
        {
            for (int i = 0; i < _UIElements.Length; i++)
                _UIElements[i].Draw();
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
        /// Adds an UI to the scenes list of UI Elements
        /// </summary>
        /// <param name="actor"></param>
        public void AddUIElement(Actor UI)
        {
            //Creates a temp array larger than the original
            Actor[] tempArray = new Actor[_UIElements.Length + 1];

            //Copies all values from the orginal array into the temp array
            for (int i = 0; i < _UIElements.Length; i++)
                tempArray[i] = _UIElements[i];
            //Adds the new actor to the end of the new array
            tempArray[_UIElements.Length] = UI;

            //Merges the arrays
            _UIElements = tempArray;
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

        /// <summary>
        /// Removes the actor from the scene
        /// </summary>
        /// <param name="actor">The actor to remove</param>
        /// <returns>If the removal was successful</returns>
        public static bool RemoveUIElement(Actor UI)
        {
            //Creates a variable to store if the removal was successful
            bool actorRemoved = false;

            //Creates a new rray that is smaller than the original
            Actor[] tempArray = new Actor[_UIElements.Length - 1];

            //Copies all values from the orginal array into the temp array unless it is the removed actor
            int j = 0;
            for (int i = 0; i < _UIElements.Length; i++)
            {
                if (_actors[i] != UI)
                {
                    tempArray[j] = _UIElements[i];
                    j++;
                }
                else
                    actorRemoved = true;
            }

            //Merges the arrays
            if (actorRemoved)
                _UIElements = tempArray;

            return actorRemoved;
        }
    }
}
