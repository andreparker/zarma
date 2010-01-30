using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.Objects.GameObject
{
    using Components.Objects.Common;
    using Microsoft.Xna.Framework;

    public class GameObjectManager : GameComponent
    {
        private Game game;
        private List<AbstractTypeFactory<GameObject>> factorys;
        private List<GameObject> gameObjects;

        public GameObjectManager(Game game_) :
            base( game_ )
        {
            game = game_;
            factorys = new List<AbstractTypeFactory<GameObject>>();
            gameObjects = new List<GameObject>();
        }

        public void RegisterFactory(AbstractTypeFactory<GameObject> factory_)
        {
             factorys.Add(factory_);
        }

        public GameObject CreateGameObject(int type_)
        {
            GameObject gameObject = null;
            foreach (AbstractTypeFactory<GameObject> f in factorys)
            {
                if (f.CanCreateType(type_) == true)
                {
                    gameObject = f.CreateFromType(type_);
                    gameObjects.Add( gameObject );
                    break;
                }
            }

            return gameObject;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameObject go in gameObjects)
            {
                if (go.Remove == true)
                {
                    gameObjects.Remove(go);
                }
                else
                {
                    go.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }
    }
}
