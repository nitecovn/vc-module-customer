using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtoCommerce.CustomerModule.Core.Models;

namespace VirtoCommerce.CustomerModule.Core.Services
{
    public interface IImportWorkflowService
    {
        OrganizationWorkflowModel ImportWorkflow(JsonImportModel jsonImportInfo);

        WorkflowModel GetWorkFlowByOrganizationId(string organizationId);

        void ChangeWorkflowStatus(bool status, string organizationId);

        List<OrganizationWorkflowModel> Search(SearchCriteriaModel searchWorkflowCriteria);
    }
}
