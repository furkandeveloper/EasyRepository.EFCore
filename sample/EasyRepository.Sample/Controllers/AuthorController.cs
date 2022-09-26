namespace EasyRepository.Sample.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dtos.Request;
using EFCore.Abstractions;
using EFCore.Ardalis.Specification;
using EFCore.Generic;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Specs;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthorController(IRepository repository, IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthorAsync([FromBody] AuthorRequestDto dto)
    {
        var entity = await this._unitOfWork.Repository.AddAsync<Author, Guid>(
            new Author
            {
                Name = dto.Name,
                Surname = dto.Surname
            });

        var book = await this._unitOfWork.Repository.AddAsync<Book, Guid>(
            new Book
            {
                Title = "Book 123",
                TotalPage = 124,
                AuthorId = entity.Id
            });

        await this._unitOfWork.Repository.CompleteAsync();

        return this.Ok(entity);
    }

    [HttpPost("range")]
    public async Task<IActionResult> AddAuthorAsync([FromBody] List<AuthorRequestDto> dto)
    {
        var entity = await this._unitOfWork.Repository.AddRangeAsync<Author, Guid>(
            dto.Select(
                    s => new Author
                    {
                        Name = s.Name,
                        Surname = s.Surname
                    })
                .ToList());

        return this.Ok(entity);
    }

    [HttpGet(Name = "FilterAuthor")]
    public async Task<IActionResult> FilterAuthorAsync([FromQuery] string name)
    {
        var queryable = this._unitOfWork.Repository.GetQueryable<Author>();
        //var spec = new AuthorByNameSpec(name);
        var spec = new AuthorOrderByNameSpec(name);
        var data = SpecificationConverter.Convert(queryable, spec);

        return this.Ok(data.ToList());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var entity = await this._unitOfWork.Repository.GetByIdAsync<Author>(true, id);

        return this.Ok(entity);
    }

    [HttpGet("filterable-multiple")]
    public async Task<IActionResult> GetFilterableMultipleAsync([FromQuery] AuthorFilterDto dto)
    {
        var entities = await this._unitOfWork.Repository.GetMultipleAsync<Author, AuthorFilterDto, object>(
            false,
            dto,
            select => new
            {
                SelectName = select.Name,
                SelectDate = select.CreationDate
            });

        return this.Ok(entities);
    }

    [HttpGet("includable-filterable-selectable-multiple")]
    public async Task<IActionResult> GetIncludeableFilterableMultipleAsync([FromQuery] AuthorFilterDto dto)
    {
        Func<IQueryable<Author>, IIncludableQueryable<Author, object>> include = a => a.Include(i => i.Books);
        var entities = await this._unitOfWork.Repository.GetMultipleAsync<Author, AuthorFilterDto, object>(
            false,
            dto,
            select => new
            {
                SelectName = select.Name,
                SelectDate = select.CreationDate
            },
            include);

        return this.Ok(entities);
    }

    [HttpGet("multiple")]
    public async Task<IActionResult> GetMultipleAsync()
    {
        var entities = await this._unitOfWork.Repository.GetMultipleAsync<Author>(false);

        return this.Ok(entities);
    }

    [HttpGet("selectable-multiple")]
    public async Task<IActionResult> GetSelectableMultipleAsync()
    {
        var entities = await this._unitOfWork.Repository.GetMultipleAsync<Author, object>(
            false,
            select => new
            {
                SelectName = select.Name,
                SelectDate = select.CreationDate
            });

        return this.Ok(entities);
    }

    [HttpDelete("{id}/hard")]
    public async Task<IActionResult> HardDeleteAsync([FromRoute] Guid id)
    {
        var entity = await this._unitOfWork.Repository.GetByIdAsync<Author>(true, id);
        await this._unitOfWork.Repository.HardDeleteAsync(entity);

        return this.NoContent();
    }

    [HttpDelete("{id}/soft")]
    public async Task<IActionResult> SoftDeleteAsync([FromRoute] Guid id)
    {
        var entity = await this._unitOfWork.Repository.GetByIdAsync<Author>(true, id);
        await this._unitOfWork.Repository.SoftDeleteAsync<Author, Guid>(entity);

        return this.NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthorAsync([FromRoute] Guid id, [FromBody] AuthorRequestDto dto)
    {
        var entity = await this._unitOfWork.Repository.GetByIdAsync<Author>(true, id);
        entity.Name = dto.Name;
        entity.Surname = dto.Surname;
        var result = await this._unitOfWork.Repository.UpdateAsync<Author, Guid>(entity);

        return this.Ok(result);
    }
}