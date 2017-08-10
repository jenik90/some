namespace QULIX.WebUi.Models.Home
{
    using System.Linq;

    using QULIX.Domain.QULEX.BusinessLayer.Entities;

    public class CompanyTypeDropdownModel
    {
        public string AjaxLinkTarget { get; set; }

        public string CompanyTypeDropdownId { get; set; }

        public IOrderedEnumerable<CompanyType> CompanyTypes { get; set; }
    }
}