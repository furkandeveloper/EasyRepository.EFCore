namespace EasyRepository.Sample.Controllers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dtos.Request;
using EFCore.Generic;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public BookController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    [HttpPost("authors/{authorId}")]
    public async Task<IActionResult> AddBookAsync([FromRoute] Guid authorId, [FromBody] BookRequestDto dto)
    {
        var entity = await this._unitOfWork.Repository.AddAsync<Book, Guid>(
            new Book
            {
                Title = dto.Title,
                TotalPage = dto.TotalPage,
                AuthorId = authorId
            });

        await this._unitOfWork.Repository.CompleteAsync();

        return this.Ok(entity);
    }

    [HttpPost("authors/{authorId}/range")]
    public async Task<IActionResult> AddBookAsync([FromRoute] Guid authorId, [FromBody] List<BookRequestDto> dto)
    {
        var entity = await this._unitOfWork.Repository.AddRangeAsync<Book, Guid>(
            dto.Select(
                    s => new Book
                    {
                        Title = s.Title,
                        TotalPage = s.TotalPage,
                        AuthorId = authorId
                    })
                .ToList());

        await this._unitOfWork.Repository.CompleteAsync();

        return this.Ok(entity);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var entity = await this._unitOfWork.Repository.GetByIdAsync<Book>(true, id);

        return this.Ok(entity);
    }

    [HttpGet("filterable-multiple")]
    public async Task<IActionResult> GetFilterableMultipleAsync([FromQuery] BookFilterDto dto)
    {
        var entities = await this._unitOfWork.Repository.GetMultipleAsync<Book, BookFilterDto, object>(
            false,
            dto,
            select => new
            {
                SelectTitle = select.Title,
                SelectTotalPage = select.TotalPage
            });

        return this.Ok(entities);
    }

    [HttpGet("includable-filterable-selectable-multiple")]
    public async Task<IActionResult> GetIncludeableFilterableMultipleAsync([FromQuery] BookFilterDto dto)
    {
        Func<IQueryable<Book>, IIncludableQueryable<Book, object>> include = a => a.Include(i => i.Author);
        var entities = await this._unitOfWork.Repository.GetMultipleAsync<Book, BookFilterDto, object>(
            false,
            dto,
            select => new
            {
                SelectName = select.Title,
                SelectDate = select.CreationDate,
                Author = select.Author.Name
            },
            include);

        return this.Ok(entities);
    }

    [HttpGet("multiple")]
    public async Task<IActionResult> GetMultipleAsync()
    {
        var entities = await this._unitOfWork.Repository.GetMultipleAsync<Book>(false);

        return this.Ok(entities);
    }

    [HttpGet("selectable-multiple")]
    public async Task<IActionResult> GetSelectableMultipleAsync()
    {
        var entities = await this._unitOfWork.Repository.GetMultipleAsync<Book, object>(
            false,
            select => new
            {
                SelectTitle = select.Title,
                SelectTotalPage = select.TotalPage
            });

        return this.Ok(entities);
    }

    [HttpDelete("{id}/hard")]
    public async Task<IActionResult> HardDeleteAsync([FromRoute] Guid id)
    {
        var entity = await this._unitOfWork.Repository.GetByIdAsync<Book>(true, id);
        await this._unitOfWork.Repository.HardDeleteAsync(entity);
        await this._unitOfWork.Repository.CompleteAsync();

        return this.NoContent();
    }

    [HttpDelete("{id}/soft")]
    public async Task<IActionResult> SoftDeleteAsync([FromRoute] Guid id)
    {
        var entity = await this._unitOfWork.Repository.GetByIdAsync<Book>(true, id);
        await this._unitOfWork.Repository.SoftDeleteAsync<Book, Guid>(entity);
        await this._unitOfWork.Repository.CompleteAsync();

        return this.NoContent();
    }

    [HttpPut("{id}/authors/{authorId}")]
    public async Task<IActionResult> UpdateAuthorAsync([FromRoute] Guid id, [FromRoute] Guid authorId, [FromBody] BookRequestDto dto)
    {
        var entity = await this._unitOfWork.Repository.GetByIdAsync<Book>(true, id);
        entity.Title = dto.Title;
        entity.TotalPage = dto.TotalPage;
        entity.AuthorId = authorId;
        var result = await this._unitOfWork.Repository.UpdateAsync<Book, Guid>(entity);
        await this._unitOfWork.Repository.CompleteAsync();

        return this.Ok(result);
    }
}