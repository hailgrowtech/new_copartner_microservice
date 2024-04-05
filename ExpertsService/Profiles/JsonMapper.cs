using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace ExpertService.Profiles;
public class JsonMapper : IJsonMapper
{
    private readonly IMapper _mapper;

    public JsonMapper(IMapper mapper)
    {
        this._mapper = mapper;
    }

    public TDomain ToDomain<TDto, TDomain>(TDto dto) where TDto : class where TDomain : class
    {
        return this._mapper.Map<TDomain>(dto);
    }

    public TDomain ToDomain<TDto, TDomain>(JsonPatchDocument<TDto> data, TDomain domain) where TDto : class where TDomain : class
    {
        var objDto = this._mapper.Map<TDto>(domain);
        data.ApplyTo(objDto);
        return this._mapper.Map<TDomain>(objDto);
    }

    public TDto ToDto<TDomain, TDto>(TDomain domain) where TDto : class where TDomain : class
    {
        return this._mapper.Map<TDto>(domain);
    }
}