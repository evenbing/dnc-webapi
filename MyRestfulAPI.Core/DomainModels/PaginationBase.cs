using MyRestfulAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyRestfulAPI.Core.DomainModels
{
    public class PaginationBase
    {
        private int _pagesize = 10;
        public int PageIndex { get; set; } = 0;
        public int PageSize
        {
            get => _pagesize;
            set => _pagesize = value > MaxPageSzie ? MaxPageSzie : value;
        }
        public string OrderBy { get; set; } = nameof(IEntity.Id);
        public string Fields { get; set; }
        protected int MaxPageSzie { get; set; } = 100;
    }
}
