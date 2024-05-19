using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walkerscott.Article.Domain.Entities;
using Walkerscott.Article.Domain.Interfaces;

namespace Wlkerscott.Article.Application.Features.Articles.Query
{
    public class GetArticlessQueryHandler : IRequestHandler<GetArticlesQuery, PaginatedResponseDTO<NewsArticle>>
    {
        private readonly IRepository<NewsArticle> _Repository;

        public GetArticlessQueryHandler(IRepository<NewsArticle> Repository)
        {
            _Repository = Repository;
        }

        public async Task<PaginatedResponseDTO<NewsArticle>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {

            var(data,count) = await _Repository.GetArticlesAsync(request.PageNumber, request.PageSize);
            return new PaginatedResponseDTO<NewsArticle>()
            {
                Data = data,
                TotalCount = count

            };
        }
    }
}
