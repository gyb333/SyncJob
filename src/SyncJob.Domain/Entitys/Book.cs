using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VO;
using Volo.Abp.Domain.Entities.Auditing;

namespace Entitys
{
    [Table("Books")]
    public class Book : AuditedAggregateRoot<Guid>
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}
