using AdminDashboardService.Dtos;
using CommonLibrary.CommonDTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace AdminDashboardService.Logic
{
    public interface IRelationshipManagerBusinessProcessor
    {
        Task<ResponseDto> Get();
        Task<ResponseDto> Get(Guid id);
        Task<ResponseDto> Post(RelationshipManagerCreateDto relationshipManagerCreateDto);
        Task<ResponseDto> Put(Guid id, RelationshipManagerCreateDto relationshipManagerCreateDto);
        Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<RelationshipManagerCreateDto> relationshipManagerDto);
        Task<ResponseDto> Delete(Guid id);
    }
}
