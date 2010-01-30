using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.Common
{
    using System.Reflection;
    public struct CreateFromType
    {
        public static Object CreateClass(Type type_, Type[] argTypes_, params Object[] args_)
        {
            ConstructorInfo info = type_.GetConstructor(argTypes_);
            Object obj = null;

            if (info != null)
            {
                obj = info.Invoke(args_); 
            }

            return obj;
        }
    }
}
