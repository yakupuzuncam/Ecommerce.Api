﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetwood.Infrastructure.Commands.Models.Product;
using Streetwood.Infrastructure.Queries.Models.Product;

namespace Streetwood.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await mediator.Send(new GetProductsQueryModel()));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
            => Ok(await mediator.Send(new GetProductByIdQueryModel(id)));

        // For admin
        [HttpGet("category/{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(Guid categoryId)
            => Ok(await mediator.Send(new GetProductsByCategoryIdQueryModel(categoryId)));

        // For client
        [HttpGet("list/{categoryId}")]
        public async Task<IActionResult> GetList(Guid categoryId)
            => Ok(await mediator.Send(new GetProductsWithDiscountByCategoryIdQueryModel(categoryId)));

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] AddProductCommandModel model)
            => Ok(await mediator.Send(model));

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateProductCommandModel model)
        {
            await mediator.Send(model.SetId(id));
            return Accepted();
        }

        [HttpDelete("{id}/{categoryId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
            => Accepted(await mediator.Send(new DeleteProductCommandModel(id)));
    }
}