using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Internal;
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
                var propertyType = property.PropertyType;
                if (propertyType.IsValueType ||
                    propertyType.IsPrimitive ||
                    propertyType.IsEnum ||
                    propertyType == typeof(decimal) ||
                    propertyType == typeof(string))
                {
                    continue;
                }

                if (propertyType.IsGenericType &&
                    propertyType.IsCollection())
                {
                    var argType = propertyType.GetIEnumerableType().GetGenericArguments().FirstOrDefault();
                    if (argType?.IsPrimitive == true ||
                        argType?.IsEnum == true ||
                        argType == typeof(decimal) ||
                        argType == typeof(string))
                    {
                        continue;
                    }
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

                        if (ignoreSubTypes.Contains(propertyType))
                        {
                            continue;
                        }
                        else if (addSeenTypesToIgnoreList)
                        {
                            // add each type that we have processed to ignore list to prevent recursions
                            ignoreSubTypes.Add(type);
                        }

                        var isEnumerableType = propertyType.GetInterface(nameof(IEnumerable)) != null;
                        var genericArgs = propertyType.GetGenericArguments();
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
                            GetIncludeTypes(ref includes, propPath, propertyType, ref ignoreSubTypes,
                                addSeenTypesToIgnoreList, maxDepth);
                        }
                    }
                }
            }
        }
    }
}