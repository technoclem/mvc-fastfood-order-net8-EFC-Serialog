using Dapper;
using FastFood.Dto;
using FastFood.Service;
using FastFood.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FastFood.Models.Components
{
    
    public class ProfileViewComponent : ViewComponent
    {


        private readonly ICustomerService _customerservice;
       
        public ProfileViewComponent(ICustomerService customerservice)
        {
            _customerservice = customerservice;            
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            int CustId = await _customerservice.GetCustIDFromHttpContext();
            if (CustId > 0)
            {
                var model = await _customerservice.GetCustomer(CustId);
                return View("Profile", model);
            }
            else
            {
                return View("Profile", (object?)null);
            }

        }

    }


}
