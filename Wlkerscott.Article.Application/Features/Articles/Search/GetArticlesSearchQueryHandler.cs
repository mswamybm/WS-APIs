using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walkerscott.Article.Domain.Entities;
using Walkerscott.Article.Domain.Interfaces;
using Wlkerscott.Article.Application.Features.Articles.Query;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Wlkerscott.Article.Application.Features.Articles.Search
{
    public class GetArticlesSearchQueryHandler : IRequestHandler<GetArticlesSearchQuery, PaginatedResponseDTO<NewsArticle>>
    {
        private readonly IRepository<NewsArticle> _Repository;

        public GetArticlesSearchQueryHandler(IRepository<NewsArticle> Repository)
        {
            _Repository = Repository;
        }

        public async Task<PaginatedResponseDTO<NewsArticle>> Handle(GetArticlesSearchQuery request, CancellationToken cancellationToken)
        {
            var(data, count) = await _Repository.GetArticlesAsync(request.Predicate, request.PageNumber, request.PageSize, true);
            return new PaginatedResponseDTO<NewsArticle>()
            {
                Data = data,
                TotalCount = count

            };
        }
    }
}
