# FastFood Order Service using .NET Core MVC, Entity Framework Core, and Serilog

This repository contains the source code for the FastFood service, powered by .NET Core MVC and Entity Framework Core. It includes functionality for managing customer accounts, shopping carts, and orders in a fast food delivery system.

## Demo
Check out the demo at this link: https://fastfoodentityframework.runasp.net/

## Table of Contents
- [Features](#features)
- [Setup](#setup)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Authentication and Authorization:** Utilizes cookie authentication for customer login with authorization policies.
- **Customer Account Management:** Allows customers to view and update their profile information.
- **Shopping Cart Management:** Enables customers to add items to their shopping carts, view the cart contents, and remove items.
- **Order Placement:** Facilitates the process of placing orders securely.
- **Validation:** Utilizes Data Annotations for input validation to ensure proper data integrity.
- **Configuration:** Email sender credentials can be configured via the appsettings.json file.
- **Logging:** Integrated Serilog functionality to capture errors and information.
- **Dependency Injection:** Utilizes dependency injection to manage and inject services.
- **Entity Framework Core for Data Access:** Implements Entity Framework Core for efficient data access.
- **Serilog for Logging:** Implements logging functionalities using Serilog.

## Setup

1. Clone the repository:

```bash
git clone https://github.com/yourusername/FastFoodService.git
```

2. Navigate to the project directory:

```bash
cd FastFoodService
```

3. Install dependencies:

```bash
dotnet restore
```

## Usage

### Customer Controller

The `CustomerController` provides various actions related to customer management, including:

- **MyAccount:** View customer account details.
- **UpdateProfile:** Update customer profile information.
- **UploadImage:** Upload a profile image for the customer.
- **AddToCart:** Add items to the shopping cart.
- **ViewCart:** View the contents of the shopping cart.
- **RemoveCart:** Remove items from the shopping cart.
- **CheckOut:** Proceed to checkout and place an order.

### Account Controller

The `AccountController` manages user authentication and account-related actions, such as sign up, login, and password recovery.

### Home Controller

The `HomeController` handles home page rendering, about page, contact us page, and product details page.

## Contributing

Contributions are welcome! If you'd like to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Commit your changes (`git commit -am 'Add some feature'`).
4. Push to the branch (`git push origin feature/your-feature`).
5. Create a new Pull Request.

## License

This project is licensed under the MIT License.
