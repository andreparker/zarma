using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Asteroids.Components.Objects.VisualObject
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Components.Objects.GameObject;
    using Components.Objects.Common;
    using Components.Sprite;

    public class VisualObjectManager : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteManager spriteManager;
        private List<AbstractTypeFactory<VisualObject>> factorys;
        private List<VisualObject> visualObjects;
        private List<VisualObject> drawList;
        private List<VisualObject> removeList;

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public SpriteManager SpriteManager
        {
            get { return spriteManager; }
        }

        public VisualObjectManager(Game game_, SpriteBatch spriteBatch_, SpriteManager spriteManager_) :
            base( game_ )
        {
            spriteBatch = spriteBatch_;
            spriteManager = spriteManager_;
            factorys = new List<AbstractTypeFactory<VisualObject>>();
            visualObjects = new List<VisualObject>();
            drawList = new List<VisualObject>();
            removeList = new List<VisualObject>();
        }

        public void RegisterFactory(AbstractTypeFactory<VisualObject> factory_)
        {
             factorys.Add(factory_);
        }

        public VisualObject CreateVisualObject(int type_, GameObject gameObject_)
        {
            return CreateVisualObjectArg(type_, gameObject_, this);
        }

        public VisualObject CreateVisualObjectArg( int type_, params object[] args )
        {
            VisualObject vis = null;
            foreach (AbstractTypeFactory<VisualObject> vf in factorys)
            {
                if (vf.CanCreateType(type_) == true)
                {
                    vis = vf.CreateFromType(type_, args);
                    visualObjects.Add( vis );
                    break;
                }
            }

            return vis;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            drawList.Clear();
            removeList.Clear();

            foreach (VisualObject vo in visualObjects)
            {
                if (vo.IsVisible == true && vo.Remove == false )
                {
                    drawList.Add(vo);
                    vo.Update(gameTime);
                }else if( vo.Remove == true )
                {
                    removeList.Add(vo);
                }
            }

            foreach (VisualObject vo in removeList)
            {
                visualObjects.Remove(vo);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Visualize(gameTime);
            base.Draw(gameTime);
        }

        public void Visualize( GameTime time_ )
        {
            foreach (VisualObject vo in drawList )
            {
                vo.Draw(time_, spriteBatch);
            }
        }
    }
}
