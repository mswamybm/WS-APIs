using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Walkerscott.Article.Domain.Entities;
using Wlkerscott.Article.Application.Features.Articles.Commands;
using Wlkerscott.Article.Application.Features.Articles.Delete;
using Wlkerscott.Article.Application.Features.Articles.Query;
using Wlkerscott.Article.Application.Features.Articles.QueryById;
using Wlkerscott.Article.Application.Features.Articles.Search;
using Wlkerscott.Article.Application.Features.Articles.Update;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Walkerscott.Article.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateArticleCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaginatedResponseDTO<NewsArticle>>> GetArticles([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            var query = new GetArticlesQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("search")]
        public async Task<ActionResult<PaginatedResponseDTO<NewsArticle>>> GetArticleSearch(
           [FromQuery] int pageNumber = 1,
           [FromQuery] int pageSize = 10,
           [FromQuery] string searchTerm = "")
        {
            Expression<Func<NewsArticle, bool>> predicate = o => string.IsNullOrEmpty(searchTerm) || o.Title.Contains(searchTerm) || o.Description.Contains(searchTerm) ;
            var query = new GetArticlesSearchQuery { PageNumber = pageNumber, PageSize = pageSize, Predicate = predicate };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<NewsArticle>> GetArticle(int id)
        {
            var query = new GetArticlesQueryById(id );
            var article = await _mediator.Send(query);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateArticle(int id, UpdateArticleCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var article = await _mediator.Send(command);
            return Ok(article);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            await _mediator.Send(new DeleteArticleCommand(id));
            return NoContent();
        }
    }
}