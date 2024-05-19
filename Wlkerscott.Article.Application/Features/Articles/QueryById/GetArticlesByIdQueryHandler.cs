using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walkerscott.Article.Domain.Entities;
using Walkerscott.Article.Domain.Interfaces;

namespace Wlkerscott.Article.Application.Features.Articles.QueryById
{
    public class GetArticlesByIdQueryHandler : IRequestHandler<GetArticlesQueryById,NewsArticle>
    {

        private readonly IRepository<NewsArticle> _Repository;

        public GetArticlesByIdQueryHandler(IRepository<NewsArticle> Repository)
        {
            _Repository = Repository;
        }

        public async Task<NewsArticle> Handle(GetArticlesQueryById request, CancellationToken cancellationToken)
        {
            var article = await _Repository.GetByIdAsync(request.id);
            if (article == null)
            {
                return null;
            }            

            return article;
        }
    }
}
