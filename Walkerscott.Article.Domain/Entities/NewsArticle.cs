using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walkerscott.Article.Domain.Entities
{
    public class NewsArticle
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Category Type { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
