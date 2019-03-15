using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtoCommerce.CustomerModule.Core.Models
{
    public class OrganizationWorkflowModel
    {
        public string OrganizationId { get; set; }
        public string WorkflowName { get; set; }
        public string JsonPath { get; set; }
        public bool Status { get; set; }
    }
}
