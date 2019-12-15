using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace In.Common
{
    public class TypeFactory : ITypeFactory
    {
        private static readonly Assembly[] Assemblies = AppDomain.CurrentDomain
            .GetAssemblies()
//            .Where(asm => !asm.FullName.StartsWith("System") && !asm.FullName.StartsWith("Microsoft"))
            .ToArray();

        private readonly ConcurrentDictionary<string, Type> _types = new ConcurrentDictionary<string, Type>();

        private static readonly Dictionary<string, Type> PrimitiveTypes = new Dictionary<string, Type>
        {
            {typeof(string).ToString(), typeof(string)},
            {typeof(char).ToString(), typeof(char)},
            {typeof(byte).ToString(), typeof(byte)},
            {typeof(sbyte).ToString(), typeof(sbyte)},
            {typeof(ushort).ToString(), typeof(ushort)},
            {typeof(short).ToString(), typeof(short)},
            {typeof(uint).ToString(), typeof(uint)},
            {typeof(int).ToString(), typeof(int)},
            {typeof(ulong).ToString(), typeof(ulong)},
            {typeof(long).ToString(), typeof(long)},
            {typeof(float).ToString(), typeof(float)},
            {typeof(double).ToString(), typeof(double)},
            {typeof(decimal).ToString(), typeof(decimal)},
            {typeof(DateTime).ToString(), typeof(DateTime)}
        };

        public Type Get(string typeName)
        {
            var isPrimitive = IsPrimitive(typeName);
            if (isPrimitive != null)
            {
                return isPrimitive;
            }

            string pureTypeName = typeName;
            string genericArg = null;
            if (typeName.IndexOf("[", StringComparison.Ordinal) > 0)
            {
                var startIdx = typeName.IndexOf("[", StringComparison.Ordinal) + 1;
                var endIdx = typeName.IndexOf("]", StringComparison.Ordinal);
                genericArg = typeName.Substring(startIdx, endIdx - startIdx);
                typeName = typeName.Replace(genericArg, "T");
            }

            if (_types.TryGetValue(pureTypeName, out Type cmdType))
                return cmdType;

            foreach (var assembly in Assemblies)
            {
                cmdType = assembly
                    .GetTypes()
                    .FirstOrDefault(type => type
                        .ToString()
                        .Equals(typeName)
                    );

                if (cmdType == null) continue;

                if (genericArg != null)
                {
                    var genericArgType = Get(genericArg);
                    cmdType = cmdType.MakeGenericType(genericArgType);
                }

                _types[pureTypeName] = cmdType;
                return cmdType;
            }

            return null;
        }

        private static Type IsPrimitive(string typeName)
        {
            PrimitiveTypes.TryGetValue(typeName, out Type type);
            return type;
        }
    }
}