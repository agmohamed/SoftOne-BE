using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOTaskManagement.Models;

public class PaginatedData<T>
{
    public List<T> Data { get; set; }
    public int TotalPages { get; set; }

    public int TotalCount { get; set; }

    public int CurrentPage { get; set; }

}
