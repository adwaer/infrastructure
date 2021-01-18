using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace In.DataAccess.EfCore.Config
{
    public static class EfExtensions
    {
        public static IQueryable<TEntity> IncludeAllRecursively<TEntity>(this IQueryable<TEntity> queryable,
            int maxDepth = int.MaxValue, bool addSeenTypesToIgnoreList = true, HashSet<Type> ignoreTypes = null)
            where TEntity : class
        {
            var type = typeof(TEntity);
            var includes = new List<string>();
            ignoreTypes ??= new HashSet<Type>();
            GetIncludeTypes(ref includes, prefix: string.Empty, type, ref ignoreTypes, addSeenTypesToIgnoreList,
                maxDepth);

            foreach (var include in includes)
            {
                queryable = queryable.Include(include);
            }

            return queryable;
        }

        private static void GetIncludeTypes(ref List<string> includes, string prefix, Type type,
            ref HashSet<Type> ignoreSubTypes,
            bool addSeenTypesToIgnoreList = true, int maxDepth = int.MaxValue)
        {
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                // skip not ref types and primitives
                if (property.PropertyType.IsValueType ||
                    property.PropertyType.IsPrimitive ||
                    property.PropertyType == typeof(decimal) ||
                    property.PropertyType == typeof(string))
                {
                    continue;
                }

                var getter = property.GetGetMethod();
                if (getter != null)
                {
                    var isVirtual = getter.IsVirtual;
                    if (isVirtual)
                    {
                        var propPath = (prefix + "." + property.Name).TrimStart('.');
                        if (maxDepth <= propPath.Count(c => c == '.'))
                        {
                            break;
                        }

                        includes.Add(propPath);

                        var subType = property.PropertyType;
                        if (ignoreSubTypes.Contains(subType))
                        {
                            continue;
                        }
                        else if (addSeenTypesToIgnoreList)
                        {
                            // add each type that we have processed to ignore list to prevent recursions
                            ignoreSubTypes.Add(type);
                        }

                        var isEnumerableType = subType.GetInterface(nameof(IEnumerable)) != null;
                        var genericArgs = subType.GetGenericArguments();
                        if (isEnumerableType && genericArgs.Length == 1)
                        {
                            // sub property is collection, use collection type and drill down
                            var subTypeCollection = genericArgs[0];
                            GetIncludeTypes(ref includes, propPath, subTypeCollection, ref ignoreSubTypes,
                                addSeenTypesToIgnoreList, maxDepth);
                        }
                        else
                        {
                            // sub property is no collection, drill down directly
                            GetIncludeTypes(ref includes, propPath, subType, ref ignoreSubTypes,
                                addSeenTypesToIgnoreList, maxDepth);
                        }
                    }
                }
            }
        }
    }
}