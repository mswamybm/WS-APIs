using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wlkerscott.Article.Application.Features.Articles.Delete
{
    public record DeleteArticleCommand(int id) :IRequest<Unit>;
}
