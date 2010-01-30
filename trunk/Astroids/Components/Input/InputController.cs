using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.Input
{
    using Microsoft.Xna.Framework;
    public abstract class InputController<T>
    {
        public abstract void ProcessInput(T object_,GameTime time_);
    }
}
