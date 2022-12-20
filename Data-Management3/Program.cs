// Data Management Project
#nullable disable
using System.Text.Json;
Console.Clear();

// Read user-data file
string jsonString = File.ReadAllText("user-data.json");

// Convert Back -- > Data
List<User> users = JsonSerializer.Deserialize<List<User>>(jsonString);

// List of products (including ones not in shopping list)
List<Product> products = new List<Product>();

// All Products
products.Add(new Product("Laptop", "Acer", 800));
products.Add(new Product("Phone", "Samsung", 1000));
products.Add(new Product("Phone", "Apple", 1000));
products.Add(new Product("Monitor", "Asus", 300));
products.Add(new Product("Telivision", "Samsung", 700));
products.Add(new Product("Tablet", "Samsung", 100));
products.Add(new Product("Laptop", "Apple", 2000));

// Boolean based on signing up or logging in (log is true and sign is false)
bool signOrLog = true;

Console.WriteLine("1. Log in with existing account.");
Console.WriteLine("2. Sign up using new account.");
string enterInput = Console.ReadLine();
if (enterInput == "2")
{
    signOrLog = false;
}
else if (enterInput != "1")
{
    Console.WriteLine("Input invalid. Enter either 1 to log in or 2 to sign up and create a new account.");
    Environment.Exit(0);
}
//User Login
bool usernameMatch = false;
if (!signOrLog)
    Console.WriteLine("Please enter the username and password of the account you would like to register.");
Console.Write("Username: ");
string usernameInput = Console.ReadLine();
Console.Write("Password: ");
string passwordInput = Console.ReadLine();

// If signing up, check if username doesn't already exist. If so, kick out (CASE SENSITIVE)
if (!signOrLog)
{
    foreach (User user in users)
    {
        if (usernameInput == user.Username)
        {
            usernameMatch = true;
            Console.WriteLine("Username already exists.");
            Environment.Exit(0);
        }
    }
    if (!usernameMatch)
    {
        users.Add(new User(usernameInput, passwordInput));
    }
}

if (findUser(usernameInput, passwordInput))
{
    bool menuLoop = true;
    while (menuLoop)
    {
        // Get shopping list of current user
        var shopList = getShopList(usernameInput);
        // Main Menu Loop
        Console.WriteLine("\n Main Menu");
        Console.WriteLine("1. Display All Products");
        Console.WriteLine("2. Look for Product by Brand");
        Console.WriteLine("3. Sort & Show Lowest to Highest");
        Console.WriteLine("4. Add Product to Shopping Cart");
        Console.WriteLine("5. Remove Product from Shopping Cart");
        Console.WriteLine("6. Display Shopping Cart");
        Console.WriteLine("7. Register New Account");
        Console.WriteLine("8. Delete Account");
        Console.WriteLine("9. Display All Accounts");
        Console.WriteLine("10. Exit");
        string menuOption = Console.ReadLine().ToLower();
        Console.WriteLine();

        if (menuOption == "1")
        {
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{products[i].Type} {products[i].Brand} ${products[i].Price}");
            }
        }
        else if (menuOption == "2")
        {
            bool result = false;
            // Implement Search Program
            Console.Write("Search by Brand: ");
            string brandSearch = Console.ReadLine().ToLower();
            // First letters of the product brand match the letters type (left to right)
            for (int i = 0; i < products.Count; i++)
            {
                if (brandSearch == products[i].Brand.ToLower())
                {
                    result = true;
                    Console.WriteLine($"{products[i].Type} {products[i].Brand} ${products[i].Price}");
                }
            }
            if (!result)
            {
                Console.WriteLine("Brand not found.");
            }
        }
        else if (menuOption == "3")
        {
            // Sort from lowest to highest price 
            products.Sort((p1, p2) => p1.Price.CompareTo(p2.Price));
            // Display products
            foreach (Product product in products)
            {
                Console.WriteLine($"{product.Type} {product.Brand} ${product.Price}");
            }
        }
        else if (menuOption == "4")
        {
            // Result is used to check if the product name is valid or if it's already in the shopping list
            bool result = false;
            // Check if product is in the shopping list
            bool inShopList = false;
            // Add product to shopping list
            Console.WriteLine("Enter the type & brand of the product you want to ADD:");
            Console.Write("Type: ");
            string addType = Console.ReadLine().ToLower();
            Console.Write("Brand: ");
            string addBrand = Console.ReadLine().ToLower();
            // Find if in shopping list
            foreach (Product listProduct in shopList)
            {
                if (addType == listProduct.Type.ToLower() && addBrand == listProduct.Brand.ToLower())
                {
                    inShopList = true;
                }
            }
            // If not in shopping list, check if product exists, if product exists, add product
            if (!inShopList)
            {
                foreach (Product product in products)
                {
                    if (addType == product.Type.ToLower() && addBrand == product.Brand.ToLower())
                    {
                        result = true;
                        shopList.Add(product);
                        Console.WriteLine("Product added to your shopping list.");
                    }
                }
            }
            // If product is already in shopping list or does not exist, output message
            if (!result)
            {
                Console.WriteLine("Product does not exist or is already in your shopping cart.");
            }
        }
        else if (menuOption == "5")
        {
            // Remove product from shopping list
            if (shopList.Count > 0)
            {
                bool result = false;
                Console.WriteLine("Enter the type & brand of the product you want to REMOVE:");
                Console.Write("Type: ");
                string addType = Console.ReadLine().ToLower();
                Console.Write("Brand: ");
                string addBrand = Console.ReadLine().ToLower();
                for (int j = 0; j < shopList.Count; j++)
                {
                    if (addType == shopList[j].Type.ToLower() && addBrand == shopList[j].Brand.ToLower())
                    {
                        result = true;
                        shopList.Remove(shopList[j]);
                        Console.WriteLine("Item removed from shopping list.");
                    }
                }
                if (!result)
                {
                    Console.WriteLine("Item not found in shopping list.");
                }
            }
            else
            {
                Console.WriteLine("Shopping list is already empty.");
            }
        }
        else if (menuOption == "6")
        {
            // Display shopping list
            if (shopList.Count > 0)
            {
                for (int i = 0; i < shopList.Count; i++)
                {
                    Console.WriteLine($"{shopList[i].Type} {shopList[i].Brand} ${shopList[i].Price}");
                }
            }
            else
            {
                Console.WriteLine("Shopping list empty.");
            }
        }
        else if (menuOption == "7")
        {
            // Register account based on username and password
            usernameMatch = false;
            Console.WriteLine("Please enter the username & password of the account you want to register.");
            Console.WriteLine();
            Console.Write("Username: ");
            string newUsernameInput = Console.ReadLine();
            Console.Write("Password: ");
            string newPasswordInput = Console.ReadLine();
            foreach (User user in users)
            {
                if (user.Username == newUsernameInput)
                {
                    usernameMatch = true;
                    Console.WriteLine("Username is already in use.");
                }
            }
            if (!usernameMatch)
            {
                users.Add(new User(newUsernameInput, newPasswordInput));
                Console.WriteLine($"New account, {newUsernameInput} is now registered.");
            }
        }
        else if (menuOption == "8")
        {
            // Delete account based on username and password
            usernameMatch = false;
            Console.WriteLine("Please enter the username & password of the account you want to delete.");
            Console.WriteLine();
            Console.Write("Username: ");
            string dltUsernameInput = Console.ReadLine();
            Console.Write("Password: ");
            string dltPasswordInput = Console.ReadLine();
            // Check if valid valid username and password to output message
            foreach (User user in users)
            {
                if (user.Username == dltUsernameInput && user.Password == dltPasswordInput)
                {
                    usernameMatch = true;
                    Console.WriteLine($"Account of {dltUsernameInput} deleted.");
                }
            }
            // Remove all users with this username and password
            users.RemoveAll(users => users.Username == dltUsernameInput && users.Password == dltPasswordInput);
            // Output based on foreach loop result
            if (!usernameMatch)
            {
                Console.WriteLine("Username and/or password do not match/exist.");
            }
        }
        else if (menuOption == "9")
        {
            // Display all users
            foreach (User user in users)
            {
                Console.WriteLine(user.Username);
            }
        }
        else if (menuOption == "10")
        {
            break;
        }
    }
}
// Find if user exists
bool findUser(string username, string password)
{
    foreach (User user in users)
    {
        if (user.Username == username && user.Password == password)
        {
            Console.WriteLine($"Welcome, {username}");
            return true;
        }
    }
    Console.WriteLine("Username and/or password not found.");
    return false;
}
// Get shopping list of user
List<Product> getShopList(string username)
{
    foreach (User user in users)
    {
        if (user.Username == username)
        {
            return user.ShopList;
        }
    }
    return null;
}

// Convert --> JSON string
jsonString = JsonSerializer.Serialize(users);

// Store in user-data file
File.WriteAllText("user-data.json", jsonString);

// List of users
users.Add(new User("Mr. Veldkamp", "CS"));
users.Add(new User("aly", "hi"));


class Product
{
    public string Type { get; set; }
    public string Brand { get; set; }
    public int Price { get; set; }

    public Product(string type, string brand, int price)
    {
        Type = type;
        Brand = brand;
        Price = price;
    }
}

class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Product> ShopList { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        ShopList = new List<Product>();
    }

    public override string ToString()
    {
        return $"({Username},{Password}, {ShopList})";
    }
}