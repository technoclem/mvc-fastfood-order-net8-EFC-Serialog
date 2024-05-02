using FastFood.Service.Interface;
using FastFoodEFC.Data;
using FastFoodEFC.Dto;
using FastFoodEFC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FastFood.Service
{
    public class CustomerService : ICustomerService
    {
        private IWebHostEnvironment _environment;
        private readonly IConfiguration _config;
        private readonly ILogger<CustomerService> _logger;
        private IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAESService _AESService;
        private readonly FastFoodDbContext _context;

        public CustomerService(FastFoodDbContext dbContext,ILogger<CustomerService> logger, IWebHostEnvironment environment,
            IConfiguration config, IEmailService emailService, IHttpContextAccessor httpContextAccessor,
            IAESService AESService)
        {
            _context = dbContext;
            _environment = environment;
            _config = config;
            _logger = logger;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            _AESService = AESService;
        }

        public async Task<Header?> GetHeader()
        {
            try
            {
                
                    Header? header = new Header();
                    int CustId = await GetCustIdFromHttpContext();
                    if (CustId > 0)
                    {
                        header.CartList = await _context.Carts
                .Where(ct => ct.CustId == CustId)
                .Join(_context.FoodItems,
                      ct => ct.FoodId,
                      fit => fit.FoodId,
                      (ct, fit) => new CartList
                      {
                          FoodId = ct.FoodId,
                          FoodName = fit.FoodName,
                          Price = ct.Price,
                          Quantity = ct.Quantity
                      })
                .OrderBy(cl => cl.FoodName)
                .ToListAsync();
                    header.CustProfileName = await _context.Customers
                 .Where(c => c.CustId == CustId)
                 .Select(c => new CustProfileName { CustId = c.CustId, CustName = c.CustName ?? "" })
                 .FirstOrDefaultAsync();
                }
                    header.CategoryList = await _context.Categories
                .OrderBy(c => c.CatName)
                .Select(c => new CategoryList { CatId = c.CatId, CatName = c.CatName })
                .ToListAsync();

                return header;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting header");
                return null;
            }
        }
        public async Task<IEnumerable<CartList>?> GetCartList(int CustId)
        {
            try
            {
                var cartList = await _context.Carts
                .Where(ct => ct.CustId == CustId)
                .Join(_context.FoodItems,
                      ct => ct.FoodId,
                      fit => fit.FoodId,
                      (ct, fit) => new CartList
                      {
                          FoodId = ct.FoodId,
                          FoodName = fit.FoodName,
                          Price = ct.Price,
                          Quantity = ct.Quantity
                      })
                .OrderBy(cl => cl.FoodName)
                .ToListAsync();

                return cartList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting customer Cart List ID: {CustId}");
                return null;
            }
        }
        public async Task<bool> RemoveCart(int CustId,int FoodId)
        {
            try
            {
                var cartItem = await _context.Carts.FirstOrDefaultAsync(ct => ct.CustId == CustId && ct.FoodId == FoodId);
                if (cartItem != null)
                {
                    _context.Carts.Remove(cartItem);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while Removing customer Cart.  ID: {CustId}");
                return false;
            }
        }
        public async Task<IEnumerable<CartList>?> GetOrderList(int CustId)
        {
            try
            {
                var orderList = await (from ot in _context.Orders
                                       join fit in _context.FoodItems on ot.FoodId equals fit.FoodId
                                       join pt in _context.Payments on ot.OrderId equals pt.OrderId into ptGroup
                                       from pt in ptGroup.DefaultIfEmpty()
                                       where ot.CustId == CustId 
                                       orderby fit.FoodName
                                       select new CartList
                                       {
                                           FoodId = ot.FoodId,
                                           FoodName = fit.FoodName,
                                           Price = ot.Price,
                                           Quantity = ot.Quantity
                                       }).ToListAsync();

                return orderList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting customer Order List ID: {CustId}");
                return null;
            }
        }
       
        public async Task<CustomerUpdate?> GetCustomer2(int CustId)
        {
            try
            {
                var customerAccount = await _context.Customers
                .Where(c => c.CustId == CustId)
                .Select(c => new CustomerUpdate
                {
                    CustId = c.CustId,
                    CustName = c.CustName,
                    CustAddress = c.CustAddress,
                    CustPhone = c.CustPhone,
                    CustEmail = c.CustEmail,
                    CustPassword = c.CustPassword
                })
                .FirstOrDefaultAsync();

                return customerAccount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting customer by ID: {CustId}");
                return null;
            }
        }
        public async Task<int> DoesCustEmailExist(string email)
        {
            try
            {
                var count = await _context.Customers
                .Where(c => c.CustEmail == email)
                .CountAsync();

                return count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while checking the customer email: {email}");
                return -1;
            }
        }

        public async Task<int> SignUp(CustomerSignUp CustModel)
        {
            int CustId = -1;

            try
            {
                if (CustModel != null)
                {

                    // Generate a random 10-digit number as activated pin
                    Random random = new Random();
                    var activatedPin = random.Next(10000, 99999).ToString() + random.Next(10000, 99999).ToString();

                    // Start transaction
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            // Insert customer data into Customer
                            var newCustomer = new Customer
                            {
                                CustName = CustModel?.CustName?.Trim(),
                                CustAddress = CustModel?.CustAddress?.Trim(),
                                CustPhone = CustModel?.CustPhone?.Trim(),
                                CustEmail = CustModel?.CustEmail?.Trim(),
                                CustPassword = CustModel?.CustPassword,
                                RegDate = DateTime.Now.ToString(),
                                ActivatedPin = activatedPin
                            };

                            _context.Customers.Add(newCustomer);
                            await _context.SaveChangesAsync();

                            // Commit transaction
                            await transaction.CommitAsync();

                            // Retrieve the CustId after insertion
                            CustId = newCustomer.CustId;

                            if (CustId > 0)
                            {
                                if (CustModel?.ImageFile != null)
                                {

                                    bool response = await UpdateProfileImage(CustModel.ImageFile, CustId);
                                    if (response == false)
                                    {
                                        _logger.LogError($"An error occurred while Uploading Image. Customer ID: {CustId}");
                                    }

                                }


                                var CustEmail = CustModel?.CustEmail;
                                if (CustEmail != null)
                                {
                                    await _emailService.SendMail("FastFood New Account", "Your Account has just been created.<br/>" +
                                           "<a href='https://localhost:7210/Account/ConfirmCustEmail?CustId=" + $"{CustId}&&ActivatedPin={activatedPin}'> " +
                                           "CLICK HERE TO CONFIRM YOUR ACCOUNT</a>", CustEmail);
                                }


                            }

                        }
                        catch (Exception ex)
                        {
                            // Rollback transaction if an error occurs
                            await transaction.RollbackAsync();

                            // Log error
                            _logger.LogError(ex, "An error occurred during signup");

                            
                        }
                    }



                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Adding customer details");

            }

            return CustId;
        }

        public async Task<int> ConfirmAccount(int CustId, string ActivatedPin)
        {
            try
            {
                // Check if there are matching records
                var recordCount = await _context.Customers
                    .Where(c => c.CustId == CustId && c.ActivatedPin == ActivatedPin)
                    .CountAsync();

                // If there are matching records, update the activated status
                if (recordCount > 0)
                {
                    var customer = await _context.Customers
                        .FirstOrDefaultAsync(c => c.CustId == CustId && c.ActivatedPin == ActivatedPin);

                    if (customer != null)
                    {
                        customer.Activated = 1;
                        await _context.SaveChangesAsync();
                    }
                    else recordCount = 0;
                }

                return recordCount;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while confirming the customer account with ID: {CustId}");
                return -1;
            }
        }

        public async Task<int> Login(LoginCredential LoginModel)
        {
            int response = -3;
            try
            {
                var customer = await _context.Customers
                 .FirstOrDefaultAsync(c => c.CustEmail == LoginModel.Email);


                if (customer != null)
                {
                    if (customer.Activated == 1)
                    {
                        if (customer.CustPassword != null)
                        {
                            if (customer.CustPassword == LoginModel.Password)
                            {
                                response = customer.CustId;
                            }
                            else response = 0;
                        }                        
                    }
                    else
                    {
                        if (customer.ActivatedPin != null)
                        {
                            response = -2;
                            if ((LoginModel.Email != null) && (LoginModel.Password != null))
                            {
                                await _emailService.SendMail("FastFood Account Confirmation", "Please confirm your account using the link below.<br/>" +
                                              "<br/><br/> <a href='https://localhost:7210/Account/ConfirmCustEmail?CustId=" 
                                              + $"{customer.CustId}&&ActivatedPin={customer.ActivatedPin}'> " +
                                              "CLICK HERE TO CONFIRM YOUR ACCOUNT</a>", LoginModel.Email);
                            }
                        }
                    }

                }
                else response = -1;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while Login the customer account with email: {LoginModel.Email}");
                response = -3;
            }
            return response;
        }

        public async Task<int> RequestPassword(string CustEmail)
        {
            int response = -1;
            try
            {
                var customer = await _context.Customers
                 .FirstOrDefaultAsync(c => c.CustEmail.Trim() == CustEmail.Trim());

                if (customer != null)
                {
                    string email = CustEmail.Trim();
                    if (customer.CustPassword != null)
                    {
                        await _emailService.SendMail("FastFood Account Details", $"Email: {email} Password: {customer.CustPassword}", email);

                        response= 1; // Success
                    }
                   
                }
                else
                {
                    response= 0; // Customer not found
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting the customer password. Email: {CustEmail}");
                response= -1;
            }
            return response;
        }
        public async Task<int> GetCustIdFromHttpContext()
        {
            var CustIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("CustId");
            if (CustIdClaim != null)
            {
                string DecryptedCustId = await _AESService.Decrypt(CustIdClaim.Value);
                if (DecryptedCustId == null || !int.TryParse(DecryptedCustId, out int CustId))
                {
                    return -1;
                }
                else
                {

                    return CustId;
                }
            }

            return -1;
        }
        public async Task<CustomerDTO?> GetCustomerAccount(int CustId)
        {
            try
            {
                // Retrieve customer information
                var customer = await _context.Customers
                    .Where(c => c.CustId == CustId)
                    .Select(c => new CustomerDTO
                    {
                        CustId = c.CustId,
                        CustName = c.CustName,
                        CustAddress = c.CustAddress,
                        CustPhone = c.CustPhone,
                        CustEmail = c.CustEmail,
                        CustPassword = c.CustPassword,
                        Ewallet = c.EWallet
                    })
                    .FirstOrDefaultAsync();

                if (customer != null)
                {
                    // Retrieve customer orders
                    customer.CustOrder = await _context.Orders
                        .Where(o => o.CustId == CustId)
                        .Select(o => new CustOrder
                        {
                            OrderDate = o.OrderDate, 
                            FoodID = o.FoodId,
                            FoodName = o.FoodItem.FoodName, 
                            FoodPrice = o.Price, 
                            Quantity = o.Quantity
                        })
                        .ToListAsync();
                }

                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while Getting the customer account with ID: {CustId}");

            }
            return null;
        }

        public async Task<bool> SetLoginCookie(int CustId)
        {
            bool done = false;
            try
            {
                string? Authentication = _config["CookieAuth:Name"];
                if (Authentication != null)
                {
                    string EncryptedId = await _AESService.Encrypt(CustId.ToString());
                    var claims = new List<Claim>()
                    {
                        new Claim("CustId", EncryptedId),
                        new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(10).ToString()) // Set expiration for 10 days from now
                    };
                    var identity = new ClaimsIdentity(claims, Authentication);
                    ClaimsPrincipal ClaimsPrincipal = new ClaimsPrincipal(identity);
                    if (_httpContextAccessor.HttpContext != null)
                    {
                        await _httpContextAccessor.HttpContext.SignInAsync(Authentication, ClaimsPrincipal);
                        done = true;
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while setting Login Cookie ID: {CustId}");

            }

            return done;
        }

        public async Task<int> UpdateProfile(CustomerUpdate model)
        {
            try
            {
                var existingCustomer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.CustId == model.CustId);

                if (existingCustomer != null)
                {

                    // Check if the new email already exists for another customer
                    var emailExists = await _context.Customers
                        .AnyAsync(c => c.CustEmail == model.CustEmail && c.CustId != model.CustId);

                    if (emailExists)
                    {
                        // Email already exists for another customer
                        return 0;
                    }
                    else
                    {
                        // Update customer data
                        existingCustomer.CustName = model.CustName?.Trim();
                        existingCustomer.CustAddress = model.CustAddress?.Trim();
                        existingCustomer.CustPhone = model.CustPhone?.Trim();
                        existingCustomer.CustEmail = model.CustEmail?.Trim();
                        existingCustomer.CustPassword = model.CustPassword;

                        await _context.SaveChangesAsync();

                        // Return 1 to indicate successful update
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while Updating the customer account with ID: {model?.CustId}");

            }
            return -1;
        }

        public async Task<bool> UpdateProfileImage(IFormFile ImageFile, int CustId)
        {
            try
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "CustomerImage");
                string filePath = Path.Combine(uploadsFolder, $"{CustId}.jpg");
                if (File.Exists(filePath))
                {

                    File.Delete(filePath);

                }
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }
                return true;
            }
            catch (Exception ex) { _logger.LogError(ex, $"Unable to upload image of the customer with ID {CustId}"); }

            return false;
        }
        public async Task<bool> AddToCart(int CustId, CartItemViewModel model)
        {
            
            try
            {
                var cartItem = new Cart
                {
                    CustId = CustId,
                    FoodId = model.FoodId,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    CartDate = DateTime.Now.ToString()
                };

                _context.Carts.Add(cartItem);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while Adding to Cart with customer account ID: {CustId},{model}");

            }
            return false;
        }

        public async Task<CustomerCart?> CheckOut(int CustId)
        {
            try
            {
                var customer = await _context.Customers
                .Where(c => c.CustId == CustId)
                .Select(c => new CustomerCart
                {
                    CustId = c.CustId,
                    CustName = c.CustName,
                    CustAddress = c.CustAddress,
                    CustPhone = c.CustPhone,
                    CustEmail = c.CustEmail,
                    Ewallet = c.EWallet,
                    CustCart = _context.Carts
                        .Where(t => t.CustId == CustId)
                        .Select(t => new CCart
                        {
                            CartDate = t.CartDate.ToString(), 
                            FoodID = t.FoodId,
                            FoodName = t.FoodItem.FoodName, 
                            FoodPrice = t.Price, 
                            Quantity = t.Quantity
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while checking out.  ID: {CustId}");

            }
            return null;
        }
        public async Task<int> PlaceOrder(int CustId)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var totalPrice = _context.Carts
                        .Where(t => t.CustId == CustId)
                        .Sum(t => t.Price * t.Quantity);

                    if (totalPrice > 0)
                    {
                        var ewallet = _context.Customers
                            .Where(c => c.CustId == CustId)
                            .Select(c => c.EWallet)
                            .FirstOrDefault();

                        if (ewallet >= totalPrice)
                        {
                            var orderItems = _context.Carts
                                .Where(t => t.CustId == CustId)
                                .Select(t => new Order
                                {
                                    CustId = t.CustId,
                                    FoodId = t.FoodId,
                                    Quantity = t.Quantity,
                                    Price = t.Price,
                                    OrderDate = DateTime.Now.ToString()
                                })
                                .ToList();

                            _context.Orders.AddRange(orderItems);
                            await _context.SaveChangesAsync();

                            var orderId = orderItems.FirstOrDefault()?.OrderId;

                            _context.Carts.RemoveRange(_context.Carts.Where(t => t.CustId == CustId));
                            await _context.SaveChangesAsync();

                            var payment = new Payment
                            {
                                OrderId = orderId ?? 0,
                                TotalPrice = totalPrice,
                                PDate = DateTime.Now.ToString()
                            };

                            _context.Payments.Add(payment);
                            await _context.SaveChangesAsync();

                            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustId == CustId);
                            if (customer != null)
                            {
                                customer.EWallet -= totalPrice;
                                await _context.SaveChangesAsync();
                            }

                            transaction.Commit();

                            return 1; // Success
                        }
                        else
                        {

                            return 0; // Insufficient balance
                        }
                    }
                    else return -2;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while Placing Order with ID: {CustId}");
                    transaction.Rollback();
                    throw;
                }
            }
            
            
        }
    }
}
