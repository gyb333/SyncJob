using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Study.Common.Domain
{
    public abstract class EntityBase<TKey> : Entity<TKey>,IHasCreationTime, IHasModificationTime
    {
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual DateTime CreationTime { get; set; }
    }

    public abstract class EntityBase : Entity, IHasCreationTime, IHasModificationTime
    {
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual DateTime CreationTime { get; set; }
         
    }
}
