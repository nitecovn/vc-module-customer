using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Practices.ObjectBuilder2;
using VirtoCommerce.CustomerModule.Core.Models;
using VirtoCommerce.CustomerModule.Core.Services;
using VirtoCommerce.CustomerModule.Web.Model;
using VirtoCommerce.CustomerModule.Web.Security;
using VirtoCommerce.Domain.Commerce.Model;
using VirtoCommerce.Domain.Commerce.Model.Search;
using VirtoCommerce.Domain.Customer.Model;
using VirtoCommerce.Domain.Customer.Services;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Core.Web.Security;

namespace VirtoCommerce.CustomerModule.Web.Controllers.Api
{
    [RoutePrefix("api")]
    [CheckPermission(Permission = CustomerPredefinedPermissions.Read)]
    public class OrganizationWorkflowController : ApiController
    {
        private readonly IMemberService _memberService;
        private readonly IMemberSearchService _memberSearchService;
        private readonly IImportWorkflowService _importWorkflowService;

        public OrganizationWorkflowController(IMemberService memberService, IMemberSearchService memberSearchService, IImportWorkflowService importWorkflowService)
        {
            _memberService = memberService;
            _memberSearchService = memberSearchService;
            _importWorkflowService = importWorkflowService;
        }

       
        [HttpGet]
        [Route("{organizationId}")]
        [ResponseType(typeof(WorkflowModel))]
        public IHttpActionResult Get(string organizationId)
        {
            var workflow = _importWorkflowService.GetWorkFlowByOrganizationId(organizationId);
            return Ok(new { data = workflow });
        }

    }
}

