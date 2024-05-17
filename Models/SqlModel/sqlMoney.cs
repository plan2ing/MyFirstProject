using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csproject.Models
{
    public class z_sqlMoney : DapperSql<Money>
    {
        public z_sqlMoney()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Money.UserNo";
            DefaultOrderByDirection = "ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }
    }
}