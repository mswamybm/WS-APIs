using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walkerscott.Article.Domain.Entities;

namespace Wlkerscott.Article.Application.Features.Articles.Update
{

    public class UpdateArticleCommand : IRequest<UpdateArticleCommand>
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Category Type { get; set; }


    }
}
