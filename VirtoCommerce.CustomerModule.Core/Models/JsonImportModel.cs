using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtoCommerce.CustomerModule.Core.Models
{
    public class JsonImportModel
    {
        public string OrganizationId { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }
    }
}
