namespace QULIX.Domain.QULEX.Core.Attributes.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The enumeration validate attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class EnumValidateAttribute : RequiredAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumValidateAttribute"/> class.
        /// </summary>
        /// <param name="enumType"> The enumeration type. </param>
        /// <exception cref="ArgumentException"> </exception>
        public EnumValidateAttribute(Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentException("Type cannot be null");
            }

            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Type must be an enum");
            }

            this.EnumType = enumType;
        }

        /// <summary>
        /// Gets the enumeration type.
        /// </summary>
        public Type EnumType { get; }

        /// <summary>
        /// Returns true if specified value is defined by enumeration type, false otherwise.
        /// </summary>
        /// <param name="value"> The value. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                return base.IsValid(value) && Enum.IsDefined(this.EnumType, value);
            }

            return false;
        }
    }
}
