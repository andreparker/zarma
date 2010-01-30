using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.Objects.Common
{

    #region Using
    using Microsoft.Xna.Framework;
    #endregion



    public abstract class AbstractTypeFactory<T>
    {
        #region Fields
        Game game;
        int object_type; 
        #endregion

        #region Constructor

        public AbstractTypeFactory(Game game_)
        {
            game = game_;
        }

        #endregion

        #region Abstract Methods

        public abstract bool CanCreateType( int type_ );
        public abstract T CreateFromType( int type_ , params object[] args );

        #endregion

        #region Properties
        public Game Game
        {
            get { return game; }
        }

        protected int Type
        {
            get { return object_type; }
            set { object_type = value; }
        } 
        #endregion
    }

}
