using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.Models.Category.Outs
{
    public class GetCategoryForView
    {
        public List<CategoryList> Categories = new List<CategoryList>();

        public int Total { get; set; }
    }

    public class CategoryList
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
