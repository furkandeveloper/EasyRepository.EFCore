using EasyRepository.EFCore.Abstractions;
using EasyRepository.Sample.Dtos.Request;
using EasyRepository.Sample.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRepository.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IRepository repository;
        public BookController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost("authors/{authorId}")]
        public async Task<IActionResult> AddBookAsync([FromRoute] Guid authorId,[FromBody] BookRequestDto dto)
        {
            var entity = await repository.AddAsync<Book, Guid>(new Book
            {
                Title = dto.Title,
                TotalPage = dto.TotalPage,
                AuthorId = authorId
            }, default);

            return Ok(entity);
        }

        [HttpPost("authors/{authorId}/range")]
        public async Task<IActionResult> AddBookAsync([FromRoute] Guid authorId, [FromBody] List<BookRequestDto> dto)
        {
            var entity = await repository.AddRangeAsync<Book, Guid>(dto.Select(s => new Book
            {
                Title = s.Title,
                TotalPage = s.TotalPage,
                AuthorId = authorId
            }).ToList(), default);

            return Ok(entity);
        }

        [HttpPut("{id}/authors/{authorId}")]
        public async Task<IActionResult> UpdateAuthorAsync([FromRoute] Guid id,[FromRoute] Guid authorId, [FromBody] BookRequestDto dto)
        {
            var entity = await repository.GetByIdAsync<Book>(true, id);
            entity.Title = dto.Title;
            entity.TotalPage = dto.TotalPage;
            entity.AuthorId = authorId;
            var result = await repository.UpdateAsync<Book, Guid>(entity);
            return Ok(result);
        }

        [HttpDelete("{id}/hard")]
        public async Task<IActionResult> HardDeleteAsync([FromRoute] Guid id)
        {
            var entity = await repository.GetByIdAsync<Book>(true, id);
            await repository.HardDeleteAsync<Book>(entity);
            return NoContent();
        }

        [HttpDelete("{id}/soft")]
        public async Task<IActionResult> SoftDeleteAsync([FromRoute] Guid id)
        {
            var entity = await repository.GetByIdAsync<Book>(true, id);
            await repository.SoftDeleteAsync<Book, Guid>(entity);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var entity = await repository.GetByIdAsync<Book>(true, id);
            return Ok(entity);
        }

        [HttpGet("multiple")]
        public async Task<IActionResult> GetMultipleAsync()
        {
            var entities = await repository.GetMultipleAsync<Book>(false);
            return Ok(entities);
        }

        [HttpGet("selectable-multiple")]
        public async Task<IActionResult> GetSelectableMultipleAsync()
        {
            var entities = await repository.GetMultipleAsync<Book, object>(false, select => new
            {
                SelectTitle = select.Title,
                SelectTotalPage = select.TotalPage
            });
            return Ok(entities);
        }

        [HttpGet("filterable-multiple")]
        public async Task<IActionResult> GetFilterableMultipleAsync([FromQuery] BookFilterDto dto)
        {
            var entities = await repository.GetMultipleAsync<Book, BookFilterDto, object>(false, dto, select => new
            {
                SelectTitle = select.Title,
                SelectTotalPage = select.TotalPage
            });
            return Ok(entities);
        }

        [HttpGet("includable-filterable-selectable-multiple")]
        public async Task<IActionResult> GetIncludeableFilterableMultipleAsync([FromQuery] BookFilterDto dto)
        {
            Func<IQueryable<Book>, IIncludableQueryable<Book, object>> include = a => a.Include(i => i.Author);
            var entities = await repository.GetMultipleAsync<Book, BookFilterDto, object>(false, dto, select => new
            {
                SelectName = select.Title,
                SelectDate = select.CreationDate,
                Author = select.Author.Name
            }, include);
            return Ok(entities);
        }
    }
}
