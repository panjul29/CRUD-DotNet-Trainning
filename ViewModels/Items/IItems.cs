using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Northwind.EntityFrameworks;

namespace Northwind.ViewModels.Items
{
    interface IItems
    {
        string UnitOfMeasurement { get; set; }
        string CostRate { get; set; }

        Dictionary<string, object> fromItemsToDict();

        string convertItemsToString();

        decimal calculateProductUnitPrice();
    }
}