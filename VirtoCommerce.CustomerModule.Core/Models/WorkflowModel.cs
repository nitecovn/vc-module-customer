using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtoCommerce.CustomerModule.Core.Models
{
    public class WorkflowModel
    {
        public IEnumerable<WorkflowStateModel> WorkflowStates { get; set; }
    }
}
