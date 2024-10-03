﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSystem.Application.Models.Bases
{
    public class FilterProduct : Paging
    {
        public string? Keyword { get; set; }

        public Guid? CategoryId { get; set; }

        public bool IsSortDecrease { get; set; } = false;
    }
}
