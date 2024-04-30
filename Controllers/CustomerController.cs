using FastFood.Dto;
using FastFood.Models;
using FastFood.Service;
using FastFood.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Reflection;
using System.Security.Claims;

namespace FastFood.Controllers
{

    [Authorize(Policy = "Customer")]
    public class CustomerController : Controller
    {


        private readonly ILogger<HomeController> _logger;
        private IFoodService _foodService;
        private ICustomerService _customerService;
        public CustomerController(ILogger<HomeController> logger, IFoodService foodService, ICustomerService customerService)
        {
            _logger = logger;
            _foodService = foodService;
            _customerService = customerService;
        }

        public async Task<IActionResult> MyAccount()
        {
            int CustID = await _customerService.GetCustIDFromHttpContext();
            if (CustID > 0)
            {
                var model = await _customerService.GetCustomerAccount(CustID);
                return View(model);
            }
            else
            {
                TempData["error"] = "Unable to retrieve the customer details. Try Login Again";
                return RedirectToAction(nameof(AccountController.Login), nameof(AccountController));
            }
        }
        public async Task<IActionResult> UpdateProfile()
        {
            int CustID = await _customerService.GetCustIDFromHttpContext();
            if (CustID > 0)
            {
                var model = await _customerService.GetCustomer2(CustID);

                return View(model);
            }
            else
            {
                TempData["error"] = "Unable to retrieve the customer details. Try Login Again";
                return RedirectToAction(nameof(AccountController.Login), nameof(AccountController));
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(CustomerUpdate model)
        {
            if (ModelState.IsValid)
            {
                if (model.CustId > 0)
                {
                    int response = await _customerService.UpdateProfile(model);
                    if (response == -1) TempData["error"] = "Unable to update your data. Try Again";
                    else if (response == 0) TempData["error"] = $"This email {model.CustEmail} has been used by another customer ";
                    else if (response == 1)
                    {
                        TempData["success"] = $"Updated Successfully.";
                    }
                }
                else TempData["error"] = "Invalid Customer Account ID";
            }
            else TempData["error"] = "The Model is Invalid. Try Again";

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                int CustID = await _customerService.GetCustIDFromHttpContext();
                if (CustID > 0)
                {
                    bool response = await _customerService.UpdateProfileImage(imageFile, CustID);
                    if (response)
                    {
                        TempData["success"] = "Profile Image Updated Successfully";
                    }
                    else TempData["error"] = "Unable to update profile image. Try Again";
                }
                else
                {
                    TempData["error"] = "Unable to retrieve the customer details. Try Login Again";
                    return RedirectToAction("Login", "Account");

                }

            }
            else TempData["error"] = "The Model is Invalid. Try Again";

            return RedirectToAction(nameof(MyAccount));

        }


        public IActionResult AddToCart(int id)
        {
            return RedirectToAction("ProductDetails", "Home", new { id = id });
        }

       [HttpPost]
        public async Task<IActionResult> AddToCart(CartItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                int CustID = await _customerService.GetCustIDFromHttpContext();
                if (CustID > 0)
                {   
                    
                    bool response = await _customerService.AddToCart(CustID, model);
                    if (response)
                    {
                        TempData["success"] = "Item Added to the Cart Successfully";

                        return RedirectToAction("ViewCart");
                    }
                    else
                    {
                        TempData["error"] = "Unable to add item to Cart";
                    }
                }
                else
                {
                    TempData["error"] = "Unable to retrieve the customer details. Try Login Again";
                }

            }
            else TempData["error"] = "The Model is Invalid. Try Again";

            return RedirectToAction("ProductDetails", "Home", new { id = model.FoodId });

        }

        public async Task<IActionResult> ViewCart()
        {
            
            int CustId = await _customerService.GetCustIDFromHttpContext();
            if (CustId > 0)
            {
                var model = await _customerService.GetCartList(CustId);
                return View(model);
            }
            else
            {
                TempData["error"] = "Unable to retrieve the customer details. Try Login Again";
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RemoveCart(int id)
        {

            int CustId = await _customerService.GetCustIDFromHttpContext();
            if (CustId > 0)
            {
                var response = await _customerService.RemoveCart(CustId,id);
                if (response)
                {
                    TempData["success"] = "Cart Item Removed Successfully";                   
                }
                else
                {
                    TempData["error"] = "Unable To Remove Cart Item Removed";
                }
            }
            else
            {
                TempData["error"] = "Unable to retrieve the customer details. Try Login Again";
            }
            return RedirectToAction("ViewCart");
        }

        public async Task<IActionResult> CheckOut()
        {

            int CustId = await _customerService.GetCustIDFromHttpContext();
            if (CustId > 0)
            {
                var model = await _customerService.CheckOut(CustId);
                return View(model);
            }
            else
            {
                TempData["error"] = "Unable to retrieve the customer details. Try Login Again";
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {

            int CustId = await _customerService.GetCustIDFromHttpContext();
            if (CustId > 0)
            {  

                var response = await _customerService.PlaceOrder(CustId);
                if (response==-1) TempData["error"] = "Unable to Place Order. Try Login Again";
                else if (response == 0) TempData["error"] = "Low Ewallet Balance";
                else if (response == 1)
                {
                    TempData["success"] = "Order Placed Successfully";
                    return RedirectToAction("MyAccount");
                }
            }
            else
            {
                TempData["error"] = "Unable to retrieve the customer details. Try Login Again";
            }
            return RedirectToAction("CheckOut");
        }
    }
}
