﻿#region addProducts

using EntityFramework.Data;
using EntityFramework.Models;

using ContosoPizzaContext context = new ContosoPizzaContext();

Product veggieSpecial = new Product()
{
    Name = "Veggie Special Pizza",
    Price = 9.99M
};
context.Products.Add(veggieSpecial);

Product deluxeMeat = new Product()
{
    Name = "Deluxe Meat Pizza",
    Price = 12.99M
};
context.Add(deluxeMeat);

Product veggiePizza = new Product()
{
    Name = "Veggie pizza",
    Price = 8.99M
};
context.Add(veggiePizza);

context.SaveChanges();

#endregion