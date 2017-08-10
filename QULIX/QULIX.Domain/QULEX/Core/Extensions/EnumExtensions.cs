namespace QULIX.Domain.QULEX.Core.Extensions
{
    using System;
    using System.Linq;
    using QULIX.Domain.QULEX.Core.Attributes;

    public static class EnumExtension
    {
        /// <summary>
        /// Gets the attribute of the requested type that is bound to the specified enumeration value.
        /// </summary>
        /// <typeparam name="T">Type of the attribute to get.</typeparam>
        /// <param name="value">The enumeration value that is marked with the attribute.</param>
        /// <returns>The attribute of the requested type that is bound to the specified enumeration value.</returns>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            return value.GetAttribute<Enum, T>();
        }

        /// <summary>
        /// Return the value stored in the string value attribute 
        /// </summary>
        /// <param name="enumeration">enumeration to get the string value from</param>
        /// <returns>string value</returns>
        public static string GetEnumStringValue(this Enum enumeration)
        {
            var attribute = enumeration.GetAttribute<StringValueAttribute>();
            return attribute == null ? string.Empty : attribute.Value;
        }

        /// <summary>
        /// Checks if all enumeration fields have attribute of specified type.
        /// </summary>
        /// <param name="enumeration"> The enumeration. </param>
        /// <typeparam name="T">The specified attribute to check. </typeparam>
        /// <returns> The <see cref="bool"/>. </returns>
        public static bool DoesAllEnumFieldsHaveAttribute<T>(this Enum enumeration) where T : Attribute
        {
            var enumType = enumeration.GetType();
            var fields = enumType.GetFields().Where(field => !field.IsSpecialName);
            return fields.All(field => Attribute.IsDefined(field, typeof(T)));
        }
    }
}
