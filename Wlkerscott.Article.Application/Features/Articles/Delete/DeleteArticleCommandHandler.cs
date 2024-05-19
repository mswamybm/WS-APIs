using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walkerscott.Article.Domain.Entities;
using Walkerscott.Article.Domain.Interfaces;

namespace Wlkerscott.Article.Application.Features.Articles.Delete
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Unit>
    {
        private readonly IRepository<NewsArticle> _Repository;

        public DeleteArticleCommandHandler(IRepository<NewsArticle> Repository)
        {
            _Repository = Repository;
        }

        public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _Repository.GetByIdAsync(request.id);
            if (article == null)
            {
                throw new KeyNotFoundException($"Order with Id {request.id} not found.");
            }

            await _Repository.DeleteAsync(article);

            return Unit.Value;
        }
    }
}
