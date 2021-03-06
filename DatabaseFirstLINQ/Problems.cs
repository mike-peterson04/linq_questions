using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            Console.WriteLine(ProblemOne() + " problem one");
            Console.WriteLine("_______________________");
            ProblemTwo();
            Console.WriteLine("_______________________");
            ProblemThree();
            Console.WriteLine("_______________________");
            ProblemFour();
            Console.WriteLine("_______________________");
            ProblemFive();
            Console.WriteLine("_______________________");
            ProblemSix();
            Console.WriteLine("_______________________");
            ProblemSeven();
            Console.WriteLine("_______________________");
            ProblemEight();
            Console.WriteLine("_______________________");
            ProblemNine();
            Console.WriteLine("_______________________");
            ProblemTen();
            Console.WriteLine("_______________________");
            //ProblemEleven();
            //Console.WriteLine("_______________________");
            //ProblemTwelve();
            //Console.WriteLine("_______________________");
            //ProblemThirteen();
            //Console.WriteLine("_______________________");
            //ProblemFourteen();
            //Console.WriteLine("_______________________");
            //ProblemFifteen();
            //Console.WriteLine("_______________________");
            //ProblemSixteen();
            //Console.WriteLine("_______________________");
            //ProblemSeventeen();
            //Console.WriteLine("_______________________");
            //ProblemEighteen();
            //Console.WriteLine("_______________________");
            //ProblemNineteen();
            //Console.WriteLine("_______________________");
            //ProblemTwenty();
            //Console.WriteLine("_______________________");
            //BonusOne();
            //Console.WriteLine("_______________________");
            BonusTwo();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private int ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            var numberOfUsers = _context.Users;
            return numberOfUsers.ToList().Count;

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var expensiveProducts = _context.Products.Where(p => p.Price > 150);
            foreach (var product in expensiveProducts)
            {
                Console.WriteLine(product.Name + " costs " + product.Price);
            }

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var s_products = _context.Products.Where(s => s.Name.ToUpper().Contains("S"));
            foreach (var product in s_products)
            {
                Console.WriteLine(product.Name);
            }
        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.
            var legacyUsers = _context.Users.Where(s => s.RegistrationDate < new DateTime(2016,1,1));
            foreach (var user in legacyUsers)
            {
                Console.WriteLine(user.Email+" and "+user.RegistrationDate);
            }

        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            var legacyUsers = _context.Users.Where(s => s.RegistrationDate > new DateTime(2016, 1, 1) && s.RegistrationDate < new DateTime(2018, 1, 1));
            foreach (var user in legacyUsers)
            {
                Console.WriteLine(user.Email + " and " + user.RegistrationDate);
            }

        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.


            var cart = _context.ShoppingCarts.Include(c => c.Product).Include(c => c.User).Where(c => c.User.Email == "afton@gmail.com");
                foreach(var item in cart)
                {
                Console.WriteLine(item.Product.Name +" costs $"+ item.Product.Price+ " and you have "+ item.Quantity+" in your cart");
                }
           
            
            

        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.

            var cart = _context.ShoppingCarts.Include(c => c.Product).Include(c => c.User).Where(c => c.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum();
            // Solution works if user has single item. Used instructions above
            Console.WriteLine(cart);
        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.

            var cart = _context.ShoppingCarts.Include(c => c.Product).Include(c => c.User).Join(_context.UserRoles, c => c.UserId, r => r.UserId, (c, r) => new
            {
                c.User.Email,
                r.RoleId,
                c.Product.Price,
                c.Product.Name,
                c.Quantity
            }).Where(r=>r.RoleId == 2);

            foreach (var item in cart)
            {
                Console.WriteLine(item.Email+" has "+item.Quantity+" of "+item.Name+" in their cart for $"+item.Price+" each");
            }

        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Razor Blade Stealth Advanced",
                Description = "High performance gaming laptop",
                Price = 2800
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            var productId = _context.Products.Where(p => p.Price == 2800).Select(p => p.Id).SingleOrDefault();
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 50
            };
            _context.ShoppingCarts.Add(shoppingCart);
            _context.SaveChanges();
        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(p => p.Price == 2800).SingleOrDefault();
            product.Price = 3000;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();

        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var oda = _context.Users.Where(o => o.Email == "oda@gmail.com").SingleOrDefault();
            _context.Users.Remove(oda);
            _context.SaveChanges();
        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            while (true)
            {

                string emailAddress;
                string pw;
                
                Console.WriteLine("please enter your email address:");
                emailAddress = Console.ReadLine();
                Console.WriteLine("Please enter your Password:");
                pw = Console.ReadLine();
                try
                {
                    var user = _context.Users.Where(u => u.Email == emailAddress && u.Password == pw).SingleOrDefault();
                    if (user == null)
                    {
                        Console.WriteLine("invalid email or password please try again");
                    }
                    else
                    {
                        Console.WriteLine("Logged in");
                        break;
                    }
                    
                    


                }
                catch(Exception e)
                {
                    Console.WriteLine("invalid email or password please try again");
                }

            }
            

        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.

  
            int grandTotal = 0;

            var cart = _context.ShoppingCarts.Include(c => c.Product)
                .Select(c => new {UserId = c.UserId, Product = c.Product, Quantity = c.Quantity })
                .GroupBy(c => c.UserId)
                .Select(c => new { user = c.Key, total = c.Sum(sc => (sc.Product.Price * sc.Quantity)) });
            foreach (var thing in cart)
            {
                Console.WriteLine("user# "+thing.user+" has $"+thing.total+"in their cart");
                grandTotal += (int)thing.total;
            }
            

            

            Console.WriteLine("all carts have a value of " + grandTotal);
        }

        

        

        // BIG ONE
    //    private void BonusThree()
    //    {
    //        // 1. Create functionality for a user to sign in via the console Check
    //        // 2. If the user succesfully signs in
    //        // a. Give them a menu where they perform the following actions within the console
    //        // View the products in their shopping cart
    //        // View all products in the Products table
    //        // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
    //        // Remove a product from their shopping cart
    //        // 3. If the user does not succesfully sing in Check
    //        // a. Display "Invalid Email or Password" Check
    //        // b. Re-prompt the user for credentials Check
    //        bool token = false;
    //        string emailAddress = "";
    //        string pw = "";

    //        while (!token)
    //        {

                

    //            Console.WriteLine("please enter your email address:");
    //            emailAddress = Console.ReadLine();
    //            Console.WriteLine("Please enter your Password:");
    //            pw = Console.ReadLine();
    //            try
    //            {
    //                var user = _context.Users.Where(u => u.Email == emailAddress && u.Password == pw).SingleOrDefault();
    //                if (user == null)
    //                {
    //                    Console.WriteLine("invalid email or password please try again");
    //                }
    //                else
    //                {
    //                    Console.WriteLine("Logged in");
    //                    token = true;
    //                    break;
    //                }




    //            }
    //            catch (Exception e)
    //            {
    //                Console.WriteLine("invalid email or password please try again");
    //            }

    //        }
    //        var authUser = _context.Users.Where(u => u.Email == emailAddress && u.Password == pw).SingleOrDefault();
    //        // user signed in
    //        userInterface(authUser.Id);





    //    }

    //    private void userInterface(int userId)
    //    {
    //        while (true)
    //        {
    //            try
    //            {
    //                Console.WriteLine("Please select from the following menu options:");
    //                Console.WriteLine("1. View your shopping cart");
    //                Console.WriteLine("2. View products for sale");
    //                Console.WriteLine("3. Log Out");

    //                int key = int.Parse(Console.ReadLine());
    //                if(key <1 || key > 3)
    //                {
    //                    Console.WriteLine("Oops we need a number between 1 and 3");
    //                }
    //                else
    //                {
    //                    if (key == 1)
    //                    {
    //                        shoppingMenu(userId);
    //                        key = 0;
    //                    }
    //                    else if (key == 2)
    //                    {
    //                        productMenu(userId);
    //                        key = 0;
    //                    }
    //                    else
    //                    {
    //                        break;
    //                    }
    //                }
    //            }
    //            catch
    //            {
    //                Console.WriteLine("Oops we need a number between 1 and 3");
    //            }
                

    //        }

    //    }

    //    private void productMenu(int userId)
    //    {
    //        int count = 0;
    //        int removeCheck = 0;
    //        int escapeCheck = -1;
    //        var cart = _context.ShoppingCarts.Include(c => c.Product).Where(c => c.UserId == userId);
            
    //        while (true)
    //        {
    //            foreach (var item in cart)
    //            {
    //                count++;
    //                if (count == removeCheck)
    //                {

    //                    if (item.Quantity-1 == 0) {
    //                        _context.ShoppingCarts.Remove(item);
    //                        _context
    //                            }
    //                    count = 0;
    //                }
    //                else if (escapeCheck == 0)
    //                {
    //                    break;
    //                }
    //                else
    //                {
    //                    Console.WriteLine("item Number "+count+"|"+item.quant + ": " + item.product + " in cart for $" + item.amount);
    //                }
    //            }
    //            Console.WriteLine("______________________________");
    //            Console.WriteLine("if you wish to remove an object from your cart please enter the item Number");
    //            Console.WriteLine("or press 0 to return to the previous menu");
    //            try
    //            {
    //                removeCheck = int.Parse(Console.ReadLine());
    //                if(removeCheck == 0)
    //                {
    //                    escapeCheck = 0;
    //                    break;
    //                }
    //                else if (removeCheck > count || removeCheck <0)
    //                {
    //                    Console.WriteLine("Whoops we need a number between 0 and "+count);
    //                }
                  
    //            }
    //            catch 
    //            {
    //                Console.WriteLine("Whoops we need a number between 0 and " + count);
    //            }
    //        }

    //    }
    //    private void shoppingMenu(int userId)
    //    {

    //    }

    }
}
