using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using VirtoCommerce.CustomerModule.Core.Models;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.CustomerModule.Data.Model
{
    public class OrganizationWorkflowEntity : AuditableEntity
    {
        [StringLength(128)]
        [Required]
        public string OrganizationId { get; set; }
        [StringLength(128)]
        [Required]
        public string WorkflowName { get; set; }
        [StringLength(500)]
        [Required]
        public string JsonPath { get; set; }
        public bool Status { get; set; }
        
        public OrganizationWorkflowModel ToModel()
        {
            return new OrganizationWorkflowModel
            {
                OrganizationId = OrganizationId,
                WorkflowName = WorkflowName,
                JsonPath = JsonPath,
                Status = Status
            };
        }
    }


}
