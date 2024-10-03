using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StorageSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
    }

    public class ApiControllerBase<TEntity, TDto, TDetailDto, TCreateInput, TUpdateInput, TKey> : ApiControllerBase
    {
        [HttpPost("Create")]
        public virtual Task<TKey> Create()
        {
            return null;
        }

        [HttpPut("Update")]
        public virtual Task<TKey> Update()
        {
            return null;
        }

        [HttpDelete("Delete")]
        public virtual Task<TKey> Delete()
        {
            return null;
        }

        [HttpGet("GetById")]
        public virtual Task<TKey> GetById()
        {
            return null;
        }

        [HttpPost("GetAll")]
        public virtual Task<TKey> GetAll()
        {
            return null;
        }

        [HttpPost("GetAllWithoutPaging")]
        public virtual Task<TKey> GetAllWithoutPaging()
        {
            return null;
        }
    }
}
