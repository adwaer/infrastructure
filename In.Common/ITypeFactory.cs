using System;

namespace In.Common
{
    public interface ITypeFactory
    {
        Type Get(string name);
    }
}