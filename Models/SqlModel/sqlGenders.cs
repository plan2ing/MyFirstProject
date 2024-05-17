using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace csproject.Models
{
    public class z_sqlGenders : DapperSql<Genders>
    {
        public z_sqlGenders()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Genders.GenderNo";
            DefaultOrderByDirection = "ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }
    }
}