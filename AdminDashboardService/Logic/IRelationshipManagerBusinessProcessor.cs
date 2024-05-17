using AdminDashboardService.Dtos;
using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace AdminDashboardService.Logic
{
    public interface IRelationshipManagerBusinessProcessor
    {
        Task<ResponseDto> Get(int page = 1, int pageSize = 10);
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(RelationshipManagerCreateDto relationshipManagerCreateDto);
        Task<ResponseDto> Put(Guid id, RelationshipManagerCreateDto relationshipManagerCreateDto);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<RelationshipManagerCreateDto> relationshipManagerDto);
        Task<ResponseDto> Delete(Guid id);
    }
}
