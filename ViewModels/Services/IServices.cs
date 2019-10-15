using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.ViewModels.Services
{
    interface IServices
    {
        Dictionary<string, object> fromServicesToDict();

        string convertServicesToString();

        decimal? calculateProductUnitPrice(string condition = "", int? userDemand = 0, decimal? duration = 0);
    }
}