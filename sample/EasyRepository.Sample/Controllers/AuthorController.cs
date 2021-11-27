﻿using EasyRepository.EFCore.Abstractions;
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
    public class AuthorController : ControllerBase
    {
        private readonly IRepository repository;

        public AuthorController(IRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthorAsync([FromBody] AuthorRequestDto dto)
        {
            var entity = await repository.AddAsync<Author, Guid>(new Author
            {
                Name = dto.Name,
                Surname = dto.Surname
            }, default);

            return Ok(entity);
        }

        [HttpPost("range")]
        public async Task<IActionResult> AddAuthorAsync([FromBody] List<AuthorRequestDto> dto)
        {
            var entity = await repository.AddRangeAsync<Author, Guid>(dto.Select(s => new Author
            {
                Name = s.Name,
                Surname = s.Surname
            }).ToList(), default);

            return Ok(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthorAsync([FromRoute] Guid id, [FromBody] AuthorRequestDto dto)
        {
            var entity = await repository.GetByIdAsync<Author>(true, id);
            entity.Name = dto.Name;
            entity.Surname = dto.Surname;
            var result = await repository.UpdateAsync<Author, Guid>(entity);
            return Ok(result);
        }

        [HttpDelete("{id}/hard")]
        public async Task<IActionResult> HardDeleteAsync([FromRoute] Guid id)
        {
            var entity = await repository.GetByIdAsync<Author>(true, id);
            await repository.HardDeleteAsync<Author>(entity);
            return NoContent();
        }

        [HttpDelete("{id}/soft")]
        public async Task<IActionResult> SoftDeleteAsync([FromRoute] Guid id)
        {
            var entity = await repository.GetByIdAsync<Author>(true, id);
            await repository.SoftDeleteAsync<Author, Guid>(entity);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var entity = await repository.GetByIdAsync<Author>(true, id);
            return Ok(entity);
        }

        [HttpGet("multiple")]
        public async Task<IActionResult> GetMultipleAsync()
        {
            var entities = await repository.GetMultipleAsync<Author>(false);
            return Ok(entities);
        }

        [HttpGet("selectable-multiple")]
        public async Task<IActionResult> GetSelectableMultipleAsync()
        {
            var entities = await repository.GetMultipleAsync<Author, object>(false, select => new
            {
                SelectName = select.Name,
                SelectDate = select.CreationDate
            });
            return Ok(entities);
        }

        [HttpGet("filterable-multiple")]
        public async Task<IActionResult> GetFilterableMultipleAsync([FromQuery] AuthorFilterDto dto)
        {
            var entities = await repository.GetMultipleAsync<Author, AuthorFilterDto, object>(false, dto, select => new
            {
                SelectName = select.Name,
                SelectDate = select.CreationDate
            });
            return Ok(entities);
        }

        [HttpGet("includable-filterable-selectable-multiple")]
        public async Task<IActionResult> GetIncludeableFilterableMultipleAsync([FromQuery] AuthorFilterDto dto)
        {
            Func<IQueryable<Author>, IIncludableQueryable<Author, object>> include = a => a.Include(i => i.Books);
            var entities = await repository.GetMultipleAsync<Author, AuthorFilterDto, object>(false, dto, select => new
            {
                SelectName = select.Name,
                SelectDate = select.CreationDate
            }, include);
            return Ok(entities);
        }
    }
}