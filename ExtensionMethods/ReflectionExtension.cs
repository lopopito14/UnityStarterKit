//-----------------------------------------------------------------------
// <copyright file="ReflectionExtension.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.ExtensionMethods
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// This class defines extensions methods for reflection.
    /// </summary>
    public static class ReflectionExtension
    {
        /// <summary>
        /// Returns the list of all method info owning the flag (defines as parameter) for a specific type.
        /// </summary>
        /// <remarks>
        /// Recursive method which navigates through base classes of the current type.
        /// </remarks>
        /// <param name="objType">The type of the current class.</param>
        /// <param name="flags">The flag used to look for the methods.</param>
        /// <returns>Returns the list of all method info.</returns>
        public static IEnumerable<MethodInfo> GetAllMethods(this Type objType, BindingFlags flags)
        {
            List<MethodInfo> methodInfos = new List<MethodInfo>();
            methodInfos.AddRange(objType.GetMethods(flags).ToList());
            if (objType.BaseType != null)
            {
                methodInfos.AddRange(objType.BaseType.GetAllMethods(flags));
            }

            return methodInfos;
        }

        /// <summary>
        /// Returns the list of all properties info owning the flag (defines as parameter) for a specific type.
        /// </summary>
        /// <remarks>
        /// Recursive method which navigates through base classes of the current type.
        /// </remarks>
        /// <param name="objType">The type of the current class.</param>
        /// <param name="flags">The flag used to look for the methods.</param>
        /// <returns>Returns the list of all property info.</returns>
        public static IEnumerable<PropertyInfo> GetAllProperties(this Type objType, BindingFlags flags)
        {
            List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
            propertyInfos.AddRange(objType.GetProperties(flags).ToList());
            if (objType.BaseType != null)
            {
                propertyInfos.AddRange(objType.BaseType.GetAllProperties(flags));
            }

            return propertyInfos;
        }

        /// <summary>
        /// Returns the list of all field info owning the flag (defines as parameter) for a specific type.
        /// </summary>
        /// <remarks>
        /// Recursive method which navigates through base classes of the current type.
        /// </remarks>
        /// <param name="objType">The type of the current class.</param>
        /// <param name="flags">The flag used to look for the methods.</param>
        /// <returns>Returns the list of all field info.</returns>
        public static IEnumerable<FieldInfo> GetAllFields(this Type objType, BindingFlags flags)
        {
            List<FieldInfo> fieldInfos = new List<FieldInfo>();
            fieldInfos.AddRange(objType.GetFields(flags).ToList());
            if (objType.BaseType != null)
            {
                fieldInfos.AddRange(objType.BaseType.GetAllFields(flags));
            }

            return fieldInfos;
        }
    }
}