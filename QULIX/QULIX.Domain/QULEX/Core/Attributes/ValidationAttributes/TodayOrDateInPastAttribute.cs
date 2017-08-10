namespace QULIX.Domain.QULEX.Core.Attributes.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The today or date in past attribute (but not earlier that 100 years ago).
    /// </summary>
    public class TodayOrDateInPastAttribute : RequiredAttribute
    {
        /// <summary>
        /// Checks that date is todays date or date in past (but not earlier that 100 years ago).
        /// </summary>
        /// <param name="value"> The value. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public override bool IsValid(object value) => 
            base.IsValid(value) && (DateTime)value <= DateTime.Now.Date && (DateTime)value > DateTime.Now.AddYears(-100);
    }
}
