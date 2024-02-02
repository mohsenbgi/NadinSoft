using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NadinSoft.Api.Models;
using NadinSoft.Api.Models.Product;
using NadinSoft.Application.Product.Commands.CreateProduct;
using NadinSoft.Application.Product.Commands.DeleteProduct;
using NadinSoft.Application.Product.Commands.UpdateProduct;
using NadinSoft.Application.Product.Queries.GetAllProducts;
using NadinSoft.Application.Product.Queries.GetProductById;
using NadinSoft.Application.Tools;

namespace NadinSoft.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ApiController
    {
        public ProductController(ISender sender) : base(sender)
        {
        }

        [HttpGet("{id:int}")]
        public async Task<ResponseResult> Index(int id, CancellationToken cancellationToken)
        {
            var query = new GetProductByIdQuery(id);

            return await Sender.Send(query, cancellationToken);
        }

        [HttpGet]
        public async Task<ResponseResult> List([FromQuery] FilterProductModel request, CancellationToken cancellationToken)
        {
            var query = new GetAllProductsQuery(request.UserId, request.Page, request.Take);

            return await Sender.Send(query, cancellationToken);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductModel request, CancellationToken cancellationToken)
        {
            var command = new CreateProductCommand(
                    User.GetUserId(),
                    request.Name,
                    request.ProduceDate,
                    request.ManufacturePhone,
                    request.ManufactureEmail,
                    request.IsAvailable
                );

            var result = await Sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(ResponseResult.Failure(result));
            }

            return CreatedAtAction(
                nameof(Index),
                new { id = result.Value },
                result.Value);
        }

        [Authorize]
        [HttpPut]
        public async Task<ResponseResult> Update(UpdateProductModel request, CancellationToken cancellationToken)
        {
            var command = new UpdateProductCommand(
                    request.Id,
                    User.GetUserId(),
                    request.Name,
                    request.ProduceDate,
                    request.ManufacturePhone,
                    request.ManufactureEmail,
                    request.IsAvailable
                );

            return await Sender.Send(command, cancellationToken);
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<ResponseResult> Delete(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand(
                    id,
                    User.GetUserId()
                );

            return await Sender.Send(command, cancellationToken);
        }
    }
}
