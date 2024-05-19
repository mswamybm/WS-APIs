using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walkerscott.Article.Domain.Entities;

namespace Wlkerscott.Article.Application.Features.Articles.QueryById
{
    public record GetArticlesQueryById(int id): IRequest<NewsArticle>;
}
