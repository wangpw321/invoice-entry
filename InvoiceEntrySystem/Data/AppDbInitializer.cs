﻿using InvoiceEntrySystem.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceEntrySystem.Data
{
    public class AppDbInitializer
    {
        public  static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                if (!context.Invoices.Any())
                {
                    context.Invoices.AddRange(new List<Invoice>()
                    {
                        new Invoice()
                        {
                            SupplierName="MacDonald",
                            InvoiceNumber="123456",
                            DateOrdered=DateTime.Now.AddDays(-10),
                            DateReceived=DateTime.Now.AddDays(10),
                            Paid=true,
                            DeleteMistake=false,
                            LineItemsNumber=1,
                            Total=59.9,
                        },

                        new Invoice()
                        {
                            SupplierName="Boston Pizza",
                            InvoiceNumber="1234567",
                            DateOrdered=DateTime.Now.AddDays(-11),
                            DateReceived=DateTime.Now.AddDays(11),
                            Paid=false,
                            DeleteMistake=false,
                            LineItemsNumber=2,
                            Total=131.72,
                        },
                        new Invoice()
                        {
                            SupplierName="Dunder Mifflin",
                            InvoiceNumber="12345678",
                            DateOrdered=DateTime.Now.AddDays(-12),
                            DateReceived=DateTime.Now.AddDays(12),
                            Paid=false,
                            DeleteMistake=true,
                            LineItemsNumber=3,
                            Total=1966.91,
                        },
                    });
                    
                    context.SaveChanges();
                }

                if (!context.LineItems.Any())
                {
                    context.LineItems.AddRange(new List<LineItem>()
                    {
                        //MacDonald
                        new LineItem()
                        {
                            ItemDescription="Burger",
                            Quantity=10,
                            UnitPrice=5.99,
                            InvoiceId=1,
                            LineTotal=59.0,
                        },

                        //Boston Pizza
                        new LineItem()
                        {
                            ItemDescription="Pizza",
                            Quantity=3,
                            UnitPrice=14.59,
                            InvoiceId=2,
                            LineTotal=43.77,
                        },

                        new LineItem()
                        {
                            ItemDescription="Spaghetti",
                            Quantity=5,
                            UnitPrice=17.59,
                            InvoiceId=2,
                            LineTotal=87.95,
                        },

                        //Dunder Mifflin
                        new LineItem()
                        {
                            ItemDescription="Paper",
                            Quantity=500,
                            UnitPrice=0.79,
                            InvoiceId=3,
                            LineTotal=395,
                        },

                        new LineItem()
                        {
                            ItemDescription="Paper Package",
                            Quantity=7,
                            UnitPrice=35.99,
                            InvoiceId=3,
                            LineTotal=251.93,
                        },

                        new LineItem()
                        {
                            ItemDescription="Copier",
                            Quantity=2,
                            UnitPrice=659.99,
                            InvoiceId=3,
                            LineTotal=1319.98,
                        },
                    });
                    context.SaveChanges();

                }
            }
        }
    }
}
