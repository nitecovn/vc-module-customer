using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CacheManager.Core;
using LinqKit;
using Newtonsoft.Json;
using VirtoCommerce.CustomerModule.Core.Models;
using VirtoCommerce.CustomerModule.Core.Services;
using VirtoCommerce.CustomerModule.Data.Model;
using VirtoCommerce.CustomerModule.Data.Repositories;
using VirtoCommerce.Platform.Core.Assets;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Common;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace VirtoCommerce.CustomerModule.Data.Services
{
    public class ImportWorkflowService : ServiceBase, IImportWorkflowService
    {
        private readonly IBlobStorageProvider _blobStorageProvider;
        private readonly ICacheManager<object> _cacheManager;
        private Func<IOrganizationWorkflowRepository> _repositoryFactory;

        public ImportWorkflowService(IBlobStorageProvider blobStorageProvider,
            ICacheManager<object> cacheManager,
            Func<IOrganizationWorkflowRepository> repositoryFactory)
        {
            _blobStorageProvider = blobStorageProvider;
            _cacheManager = cacheManager;
            _repositoryFactory = repositoryFactory;
        }

        public OrganizationWorkflowModel ImportWorkflow(JsonImportModel jsonImportInfo)
        {
            var workflow = new OrganizationWorkflowEntity
            {
                OrganizationId = jsonImportInfo.OrganizationId,
                WorkflowName = jsonImportInfo.FileName,
                JsonPath = jsonImportInfo.FileUrl,
                Status = false
            };

            using (var repository = _repositoryFactory())
            {
                using (var changeTracker = GetChangeTracker(repository))
                {
                    var workflows = repository.GetByOrganizationIdAsync(jsonImportInfo.OrganizationId);

                    if (workflows.Result.Any())
                    {
                        var updateWorkflow = workflows.Result[0];
                        changeTracker.Attach(updateWorkflow);

                        updateWorkflow.WorkflowName = jsonImportInfo.FileName;
                        updateWorkflow.JsonPath = jsonImportInfo.FileUrl;
                    }
                    else
                    {
                        repository.Add(workflow);
                    }
                    CommitChanges(repository);
                }
            }
            return workflow.ToModel();
        }
        public WorkflowModel GetWorkFlowByOrganizationId(string organizationId)
        {
            using (var repository = _repositoryFactory())
            {
                repository.DisableChangesTracking();
                var workflows = repository.GetByOrganizationIdAsync(organizationId);

                if (workflows.Result.Any())
                {
                    var workflow = workflows.Result[0];
                    return _cacheManager.Get("Workflow", "WorkflowRegion", () =>
                    {
                        string jsonValue;
                        using (var stream = _blobStorageProvider.OpenRead(workflow.JsonPath))
                        {
                            var reader = new StreamReader(stream);
                            jsonValue = reader.ReadToEnd();
                        }
                        var workFlow = JsonConvert.DeserializeObject<WorkflowModel>(jsonValue);
                        return workFlow;
                    });
                }
                return null;
            }
        }
        public void ChangeWorkflowStatus(bool status, string organizationId)
        {
            //Update status for organization workflow
            var workflowSettingId = string.Empty;
            using (var repository = _repositoryFactory())
            {
                using (var changeTracker = GetChangeTracker(repository))
                {
                    var workflows = repository.GetByOrganizationIdAsync(organizationId);

                    if (workflows.Result.Any())
                    {
                        var updateWorkflow = workflows.Result[0];
                        changeTracker.Attach(updateWorkflow);
                        updateWorkflow.Status = status;
                        CommitChanges(repository);
                    }
                }
            }
        }
        public List<OrganizationWorkflowModel> Search(SearchCriteriaModel searchWorkflowCriteria)
        {
            if (searchWorkflowCriteria == null) return null;

            var expandPredicate = LinqKit.PredicateBuilder.New<OrganizationWorkflowEntity>();
            if (!string.IsNullOrEmpty(searchWorkflowCriteria.OrganizationId))
            {
                expandPredicate = expandPredicate.And(x => x.OrganizationId == searchWorkflowCriteria.OrganizationId);
            }
            if (!string.IsNullOrEmpty(searchWorkflowCriteria.WorkflowName))
            {
                expandPredicate = expandPredicate.And(x => x.WorkflowName == searchWorkflowCriteria.WorkflowName);
            }
            if (searchWorkflowCriteria.Active != null)
            {
                expandPredicate = expandPredicate.And(x => x.Status == searchWorkflowCriteria.Active);
            }
            var predicate = (Expression<Func<OrganizationWorkflowEntity, bool>>)Extensions.Expand(expandPredicate);

            using (var repository = _repositoryFactory())
            {
                repository.DisableChangesTracking();
                var workflowEntites = repository.Search(predicate).ToArray();
                var organizationWorkflowDtos = new List<OrganizationWorkflowModel>();
                foreach (var item in workflowEntites)
                {
                    organizationWorkflowDtos.Add(item.ToModel());
                }
                return organizationWorkflowDtos;
            }
        }
    }
}
