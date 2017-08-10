namespace QULIX.Domain.QULEX.Core.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;

    using QULIX.Domain.QULEX.Core.Attributes;

    /// <summary>
    /// Object extensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets the attribute of the requested type that is bound to the specified object.
        /// </summary>
        /// <typeparam name="TObjectType">The type of the object.</typeparam>
        /// <typeparam name="TAttribute">Type of the attribute to get.</typeparam>
        /// <param name="object">The object.</param>
        /// <returns>
        /// The attribute of the requested type that is bound to the specified object.
        /// </returns>
        public static TAttribute GetAttribute<TObjectType, TAttribute>(this TObjectType @object)
            where TAttribute : Attribute
        {
            TAttribute[] result = @object.GetAttributes<TObjectType, TAttribute>();

            return result.Length > 0 ? result[0] : null;
        }

        /// <summary>
        /// Gets the list of attributes of the requested type that are bound to the specified object.
        /// </summary>
        /// <typeparam name="TObjectType">The type of the object.</typeparam>
        /// <typeparam name="TAttribute">Type of the attributes to get.</typeparam>
        /// <param name="object">The object.</param>
        /// <returns>
        /// The list of attributes of the requested type that are bound to the specified object.
        /// </returns>
        public static TAttribute[] GetAttributes<TObjectType, TAttribute>(this TObjectType @object)
            where TAttribute : Attribute
        {
            var result = new TAttribute[0];

            if (@object != null)
            {
                Type type = @object.GetType(), attributeType = typeof(TAttribute);
                MemberInfo mi = typeof(StringValueAttribute).IsAssignableFrom(attributeType)
                                    ? type.GetField(@object.ToString())
                                    : type.GetMember(@object.ToString()).FirstOrDefault();

                if (mi != null)
                {
                    var attributes = (TAttribute[])mi.GetCustomAttributes(attributeType, false);

                    if (attributes.Length > 0)
                    {
                        result = attributes;
                    }
                }
            }

            return result;
        }
    }
}
