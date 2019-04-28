using System;
using Volo.Abp.Application.Dtos;

namespace SyncJob.Todos
{
    public class TodoDto : EntityDto<Guid>
    {
        public string Text { get; set; }
    }
}