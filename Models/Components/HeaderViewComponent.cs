using FastFood.Service.Interface;
using Microsoft.AspNetCore.Mvc;


namespace FastFoodEFC.Models.Components
{
    
    public class HeaderViewComponent : ViewComponent
    {

        
        private readonly ICustomerService _customerservice;

        public HeaderViewComponent(ICustomerService customerservice)
        {           
           _customerservice = customerservice;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {            
            var model = await _customerservice.GetHeader();
            return View("Header", model);

        }

    }


}
