﻿using System;
using System.Collections.Generic;

namespace In.Common
{
    public interface IDiScope
    {
        T Resolve<T>();
        object Resolve(Type type);
        
        IEnumerable<T> ResolveAll<T>();
    }
}