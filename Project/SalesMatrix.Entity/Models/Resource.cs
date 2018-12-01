using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesMatrix.Entity.Models
{
    public class Resource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResourceId { get; set; }

        public string ResourceType { get; set; }
        //[Index(IsUnique = true)]
        public string ResourceName { get; set; }
        public string Description { get; set; }
        public string Parent { get; set; }
        public string Sequence { get; set; }
        public string ModuleId { get; set; }
        public bool IsGlobal { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedFrom { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedFrom { get; set; }
        public bool Status { get; set; }

        public virtual Module Module { get; set; }
    }
}
