using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtoCommerce.CustomerModule.Core.Models
{
    public class WorkflowStateModel
    {
        public string Status { get; set; }
        public Dictionary<string, string[]> NextState { get; set; }
    }
}
