using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Open.Core {
    public class PaginatedList<T> : List<T>, IPaginatedList<T> {
        public const int DefaultPageSize = 10;
        public int PageIndex { get; protected set; }
        public int TotalPages { get; protected set; }
        public PaginatedList() { }
        private PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize) {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            AddRange(items);
        }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int? pageIndex,
            int? pageSize) {
            var idx = pageIndex ?? 1;
            var size = pageSize ?? DefaultPageSize;
            var count = await source.CountAsync();
            var items = await source.Skip((idx - 1) * size).Take(size).ToListAsync();
            return new PaginatedList<T>(items, count, idx, size);
        }
    }
}


  
