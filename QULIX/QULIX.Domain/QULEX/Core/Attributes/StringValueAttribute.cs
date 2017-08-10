namespace QULIX.Domain.QULEX.Core.Attributes
{
    using System;

    /// <summary>
    /// Attribute to supply fields with string values
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class StringValueAttribute : Attribute
    {
        /// <summary>
        /// The value associated with the field.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Initializes an instance of <see cref="StringValueAttribute"/>
        /// </summary>
        /// <param name="value">The value to associate with the field.</param>
        public StringValueAttribute(string value)
        {
            this.Value = value;
        }
    }
}
