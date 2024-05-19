using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walkerscott.Article.Domain.Entities;

namespace Wlkerscott.Article.Application.Features.Articles.Query
{
    public class GetArticlesQuery : IRequest<PaginatedResponseDTO<NewsArticle>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
