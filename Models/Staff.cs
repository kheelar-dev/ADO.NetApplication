using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NetApplication.Models
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffID { get; set; }

        [Column(TypeName = "nvarchar")]
        [MaxLength(50)]
        public string StaffName { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string JobTitle { get; set; }

        public int? Salary { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string CreatedUser { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string UpdatedUser { get; set; }
    }
}
