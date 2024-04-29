using AutoMapper;
using CommonLibrary;
using CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using BlogService.Dtos;
using MigrationDB.Model;
using BlogService.Queries;
using BlogService.Commands;

namespace BlogService.Logic;
public class BlogBusinessProcessor : IBlogBusinessProcessor
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public BlogBusinessProcessor(ISender sender, IMapper mapper)
    {
        this._sender = sender;
        this._mapper = mapper;
    }

    public async Task<ResponseDto> Get()
    {        
            var expertsList = await _sender.Send(new GetBlogQuery());
            var expertsReadDtoList = _mapper.Map<List<BlogReadDto>>(expertsList);
            return new ResponseDto()
            {
                IsSuccess = true,
                Data = expertsReadDtoList,
            };           
    }
    public async Task<ResponseDto> Get(Guid id)
    {
        var experts = await _sender.Send(new GetBlogByIdQuery(id));
        if (experts == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = null,
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }
        var expertsReadDto = _mapper.Map<BlogReadDto>(experts);
        return new ResponseDto()
        {
            IsSuccess = true,
            Data = expertsReadDto,
        };
    }
    /// <inheritdoc/>
   
    //public async Task<ExpertsReadDto> Delete(Guid Id)
    //{
    //    var user = await _sender.Send(new DeleteExpertsCommand(Id));
    //    var userReadDto = _mapper.Map<ExpertsReadDto>(user);
    //    return userReadDto;
    //}

    public async Task<ResponseDto> Delete(Guid id)
    {
        var expert = await _sender.Send(new DeleteBlogCommand(id));
        var expertReadDto = _mapper.Map<ResponseDto>(expert);
        return expertReadDto;
    }

    public async Task<ResponseDto> Post(BLogCreateDto request)
    {
        var blogs = _mapper.Map<Blog>(request);

        var blogsExperts = await _sender.Send(new GetBlogByIdQuery(blogs.Id));
        if (blogsExperts != null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<BlogReadDto>(blogsExperts),
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertExistsWithMobileOrEmail }
            };
        }

        var result = await _sender.Send(new CreateBlogCommand(blogs));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<BlogReadDto>(blogsExperts),
                ErrorMessages = new List<string>() { AppConstants.Expert_FailedToCreateNewExpert }
            };
        }

        var resultDto = _mapper.Map<BlogReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Expert_ExpertCreated
        };
    }

    public async Task<ResponseDto> Put(Guid id, BLogCreateDto request)
    {
        var blogs = _mapper.Map<Blog>(request);

        var existingblogs = await _sender.Send(new GetBlogByIdQuery(id));
        if (existingblogs == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<BlogReadDto>(existingblogs),
                ErrorMessages = new List<string>() { AppConstants.Common_NoRecordFound }
            };
        }
        blogs.Id = id; // Assigning the provided Id to the experts
        var result = await _sender.Send(new PutBlogCommand(blogs));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<BlogReadDto>(existingblogs),
                ErrorMessages = new List<string>() { AppConstants.Expert_FailedToCreateNewExpert }
            };
        }

        var resultDto = _mapper.Map<BlogReadDto>(result);
        return new ResponseDto()
        {
            Data = resultDto,
            DisplayMessage = AppConstants.Expert_ExpertCreated
        };
    }

    public async Task<ResponseDto> Patch(Guid Id, JsonPatchDocument<BLogCreateDto> request)
    {
        var blogs = _mapper.Map<Blog>(request);

        var existingBlogs = await _sender.Send(new GetBlogByIdQuery(Id));
        if (existingBlogs == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                //   Data = _mapper.Map<ExpertsReadDto>(existingExperts),
                ErrorMessages = new List<string>() { AppConstants.Expert_ExpertNotFound }
            };
        }

        var result = await _sender.Send(new PatchBlogCommand(Id, request, existingBlogs));
        if (result == null)
        {
            return new ResponseDto()
            {
                IsSuccess = false,
                Data = _mapper.Map<BlogReadDto>(existingBlogs),
                ErrorMessages = new List<string>() { AppConstants.Expert_FailedToUpdateExpert }
            };
        }

        return new ResponseDto()
        {
            Data = _mapper.Map<BlogReadDto>(result),
            DisplayMessage = AppConstants.Expert_ExpertUpdated
        };
    }
}

