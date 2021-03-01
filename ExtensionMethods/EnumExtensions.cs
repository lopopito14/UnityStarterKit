//-----------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="Lopopito Corporation">
//     Copyright (c) Lopopito. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UnityStarterKit.ExtensionMethods
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description of the enum value.
        /// </summary>
        /// <param name="@enum">The enum value.</param>
        /// <returns>
        /// The description of the enum value.
        /// </returns>
        public static string GetDescription(this Enum @enum)
        {
            FieldInfo fieldInfo = @enum.GetType().GetField(@enum.ToString());
            if (fieldInfo != null)
            {
                if (fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute)) is DescriptionAttribute descriptionAttribute)
                {
                    return descriptionAttribute.Description;
                }
            }

            return @enum.ToString();
        }
    }
}
