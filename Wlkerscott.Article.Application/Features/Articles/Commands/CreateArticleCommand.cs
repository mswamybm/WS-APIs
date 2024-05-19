using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walkerscott.Article.Domain.Interfaces;
using Walkerscott.Article.Domain.Entities;
using MediatR;

namespace Wlkerscott.Article.Application.Features.Articles.Commands
{
    public class CreateArticleCommand : IRequest<int>
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Category Type { get; set; }


    }
}

namespace Wlkerscott.Article.Application.Features.Articles.Commands
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateArticleCommand, int>
    {
        private readonly IRepository<NewsArticle> _authorRepository;

        public CreateAuthorCommandHandler(IRepository<NewsArticle> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<int> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var totalCount = await _authorRepository.ListAllAsync();
            var article = new NewsArticle {
                Id = totalCount.Count()+1,
                Description = request.Description,
                Type = request.Type,
                Title = request.Title,
                CreatedDate = DateTime.Now,
            };
            await _authorRepository.AddAsync(article);
            return article.Id;
        }
    }
}
