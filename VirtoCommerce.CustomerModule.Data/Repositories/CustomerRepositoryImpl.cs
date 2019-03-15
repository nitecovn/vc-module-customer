using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VirtoCommerce.CustomerModule.Data.Model;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure.Interceptors;

namespace VirtoCommerce.CustomerModule.Data.Repositories
{
    public class CustomerRepositoryImpl : MemberRepositoryBase, ICustomerRepository, IOrganizationWorkflowRepository
    {
        public CustomerRepositoryImpl()
        {
        }

        public CustomerRepositoryImpl(string nameOrConnectionString, params IInterceptor[] interceptors)
            : base(nameOrConnectionString, interceptors)
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public CustomerRepositoryImpl(DbConnection existingConnection, IUnitOfWork unitOfWork = null,
            IInterceptor[] interceptors = null) : base(existingConnection, unitOfWork, interceptors)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            #region Contact
            modelBuilder.Entity<ContactDataEntity>().HasKey(x => x.Id)
                .Property(x => x.Id);
            modelBuilder.Entity<ContactDataEntity>().ToTable("Contact");

            #endregion

            #region Organization
            modelBuilder.Entity<OrganizationDataEntity>().HasKey(x => x.Id)
                .Property(x => x.Id);
            modelBuilder.Entity<OrganizationDataEntity>().ToTable("Organization");
            #endregion

            #region Employee
            modelBuilder.Entity<EmployeeDataEntity>().HasKey(x => x.Id)
                .Property(x => x.Id);
            modelBuilder.Entity<EmployeeDataEntity>().ToTable("Employee");

            #endregion

            #region Vendor
            modelBuilder.Entity<VendorDataEntity>().HasKey(x => x.Id)
                .Property(x => x.Id);
            modelBuilder.Entity<VendorDataEntity>().ToTable("Vendor");

            #endregion

            #region OrganizationWorkflow
            modelBuilder.Entity<OrganizationWorkflowEntity>().ToTable("OrganizationWorkflow").HasKey(x => x.Id).Property(x => x.Id);
            #endregion

            base.OnModelCreating(modelBuilder);
        }


        #region ICustomerRepository Members
        public IQueryable<OrganizationDataEntity> Organizations
        {
            get { return GetAsQueryable<OrganizationDataEntity>(); }
        }

        public IQueryable<ContactDataEntity> Contacts
        {
            get { return GetAsQueryable<ContactDataEntity>(); }
        }

        public IQueryable<EmployeeDataEntity> Employees
        {
            get { return GetAsQueryable<EmployeeDataEntity>(); }
        }

        public IQueryable<VendorDataEntity> Vendors
        {
            get { return GetAsQueryable<VendorDataEntity>(); }
        }
        #endregion

        #region IOrganizationWorkflowRepository
        public IQueryable<OrganizationWorkflowEntity> OrganizationWorkflows => GetAsQueryable<OrganizationWorkflowEntity>();

        public Task<OrganizationWorkflowEntity[]> GetByOrganizationIdAsync(string organizationId)
        {
            return OrganizationWorkflows.Where(x => x.OrganizationId == organizationId).ToArrayAsync();
        }
        public void Add(OrganizationWorkflowEntity entity)
        {
            Add<OrganizationWorkflowEntity>(entity);
        }
        public async Task UpdateAsync(OrganizationWorkflowEntity entity)
        {
            var organizationWorkflows = await OrganizationWorkflows.Where(x => x.OrganizationId == entity.OrganizationId).ToArrayAsync();
            var organizationWorkflow = organizationWorkflows.FirstOrDefault();
            if (organizationWorkflow != null)
            {
                Update<OrganizationWorkflowEntity>(entity);
            }
        }
        public OrganizationWorkflowEntity[] Search(Expression<Func<OrganizationWorkflowEntity, bool>> filter)
        {
            return LinqKit.Extensions.AsExpandable(OrganizationWorkflows).Where(filter).ToArray();

        }
        #endregion

    }

}
