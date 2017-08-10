namespace QULIX.WebUi.Models.Home
{
    using QULIX.Domain.QULEX.BusinessLayer.Entities;

    /// <summary>
    /// The create company type model.
    /// </summary>
    public class CreateCompanyTypeModel
    {
        /// <summary>
        /// Gets or sets the company type.
        /// </summary>
        public CompanyType CompanyType { get; set; }

        /// <summary>
        /// Gets or sets the html id attribute of company type dropdown.
        /// </summary>
        public string CompanyTypeDropdownId { get; set; }

        /// <summary>
        /// Gets or sets the id of element to be displayed while AJAX-request is performing.
        /// </summary>
        public string LoadingElementId { get; set; }
    }
}