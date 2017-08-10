namespace QULIX.Domain.QULEX.BusinessLayer.Attributes.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;

    using QULIX.Domain.QULEX.BusinessLayer.Entities;

    /// <summary>
    /// The must not be null zero or string empty.
    /// </summary>
    public class EmployeeCompanyIdMustNotBeZeroAttribute : RequiredAttribute
    {
        /// <summary>
        /// Checks that value is not null or empty string, also if value can be convert to integer, it checks if it is not zero.
        /// </summary>
        /// <param name="value"> The value. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public override bool IsValid(object value)
        {
            Company company = value as Company;
            if (company == null)
            {
                return false;
            }

            return base.IsValid(value) && company.CompanyId != 0;
        }
    }
}
