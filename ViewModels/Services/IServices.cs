using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.ViewModels.Services
{
    interface IServices
    {
        char delimiter();
        Dictionary<string, object> fromServicesToDict();

        string convertServicesToString();

        decimal? calculateProductUnitPrice(string condition = null, int? userDemand = null, decimal? duration = null);
    }
}