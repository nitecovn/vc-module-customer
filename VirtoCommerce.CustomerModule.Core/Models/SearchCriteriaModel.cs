using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtoCommerce.CustomerModule.Core.Models
{
    public class SearchCriteriaModel
    {
        public string OrganizationId { get; set; }
        public string WorkflowName { get; set; }
        public bool? Active { get; set; }       
    }
}
