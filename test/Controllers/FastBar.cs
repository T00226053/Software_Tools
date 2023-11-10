﻿using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using test.Models;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace test.Controllers
{
    public class FastBar : Controller
    {
        private readonly ILogger<FastBar> _logger;
        public FastBar(ILogger<FastBar> logger)
        {
            _logger = logger;
        }

        public IActionResult FastBarMenu()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult RetrieveItems()
        {
            var model = new CreateOrder();

            string connectionString = "Server=tcp:restaurantdatabaseserver.database.windows.net,1433;Initial Catalog=restaurantdb;Persist Security Info=False;User ID=adminBilly;Password=Password1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT ItemName, ItemPrice FROM MenuItems";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrderDetails orderDetails = new OrderDetails();

                                orderDetails.itemName = "" + reader.GetString(0);
                                orderDetails.itemPrice = "" + reader.GetString(1);

                                //model.ListOrderDetails.Add(orderDetails);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            // Pass the List of CreateOrder to the view with the menu items.
            return View(model);
        }

    }
}

public class OrderDetails
{
    public string itemName = "";
    public string itemPrice = "";
}

