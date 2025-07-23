using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOTaskManagement.Models;

public class PaginatedQuery
{
    public int Page { get; set; } = 1;

    public int PerPage { get; set; } = 10;

    public string Keyword { get; set; } = string.Empty;


    public string SortDirection { get; set; } = "asc";

    public int Status { get; set; }

    public int Priority { get; set; }



}
