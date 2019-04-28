using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace SyncJob.Todos
{
    public interface ITodoAppService
    {
        Task<PagedResultDto<TodoDto>> GetListAsync();
    }
}