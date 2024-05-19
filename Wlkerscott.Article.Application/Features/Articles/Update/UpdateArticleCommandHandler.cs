using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walkerscott.Article.Domain.Entities;
using Walkerscott.Article.Domain.Interfaces;

namespace Wlkerscott.Article.Application.Features.Articles.Update
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, UpdateArticleCommand>
    {
        private readonly IRepository<NewsArticle> _Repository;

        public UpdateArticleCommandHandler(IRepository<NewsArticle> Repository)
        {
            _Repository = Repository;
        }

        public async Task<UpdateArticleCommand> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _Repository.GetByIdAsync(request.Id);
            if (article == null)
            {
                throw new KeyNotFoundException($"Order with Id {request.Id} not found.");
            }

            article.Id = request.Id;
            article.Title = request.Title;
            article.Description = request.Description;
            article.Type = request.Type;

            await _Repository.UpdateAsync(article);

            return request;
        }
      
    }
}
