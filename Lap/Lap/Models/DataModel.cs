namespace Lap.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using System.Linq;

public static class DataModel
{
    public static void CreateDatabase()
    {
        using (LapContext TDB = new())
        {
            if (TDB.Database.EnsureDeleted())
            {
                Console.WriteLine("deleted");
                Console.WriteLine(TDB.Database.GenerateCreateScript());
            }
            if (TDB.Database.EnsureCreated())
            {
                Console.WriteLine("created");
            }
        }
    }

    public static void AddUser(Customer cust)
    {
        using (LapContext dbContext = new LapContext())
        {
            dbContext.Customers.Add(cust);
            dbContext.SaveChanges();
        }
    }

    public static bool UserLogin(Customer customer)
    {
        using (LapContext ctx = new LapContext())
        {
            IQueryable<Customer> query = from Cust in ctx.Customers
                                          where customer.Email == Cust.Email && customer.Password == Cust.Password
                                          select customer;
            if (query.Count<Customer>() > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }
    }





}
