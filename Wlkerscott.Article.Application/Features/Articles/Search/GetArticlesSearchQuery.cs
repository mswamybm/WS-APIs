using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Walkerscott.Article.Domain.Entities;
using Wlkerscott.Article.Application.Features.Articles.Query;

namespace Wlkerscott.Article.Application.Features.Articles.Search
{
    public class GetArticlesSearchQuery : IRequest<PaginatedResponseDTO<NewsArticle>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public Expression<Func<NewsArticle, bool>> Predicate { get; set; }
    }
}
