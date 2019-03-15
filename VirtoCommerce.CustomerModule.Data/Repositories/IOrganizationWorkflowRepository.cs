using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VirtoCommerce.CustomerModule.Data.Model;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.CustomerModule.Data.Repositories
{
    public interface IOrganizationWorkflowRepository : IRepository
    {
        IQueryable<OrganizationWorkflowEntity> OrganizationWorkflows { get; }
        Task<OrganizationWorkflowEntity[]> GetByOrganizationIdAsync(string organizationId);
        Task UpdateAsync(OrganizationWorkflowEntity entity);
        void Add(OrganizationWorkflowEntity entity);
        OrganizationWorkflowEntity[] Search(Expression<Func<OrganizationWorkflowEntity, bool>> filter);
    }
}
