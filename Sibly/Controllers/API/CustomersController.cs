using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using Sibly.Models;
using Sibly.ViewModels;
using System.Data.Entity;
using Sibly.Dtos;
using AutoMapper;
//using System.Net.Http;

namespace Sibly.Controllers.API
{
    public class CustomersController : ApiController
    {
        public ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public IEnumerable<CustomerDto> GetCustomers()
        {
            //Get /API/CUSTOMERS
            return _context.Customers.ToList().Select(Mapper.Map<Customer,CustomerDto>);
          
        }


        //Get /API/CUSTOMER
  
        public CustomerDto GetCustomer(int id)
        {
            //Get /API/CUSTOMERS
            var customer = _context.Customers.SingleOrDefault(c => c.CustomerId==id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return Mapper.Map<Customer,CustomerDto>(customer);
        }


        //POST /API/CUSTOMER

        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
               throw new HttpResponseException(HttpStatusCode.BadRequest); 
            }
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            customerDto.CustomerId = customer.CustomerId;
            return customerDto;
        }


        //PUT /API/CUSTOMER/1
  
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var customerInDb = _context.Customers.SingleOrDefault(c => c.CustomerId == id);
            if (customerInDb==null)
            {
               throw new HttpResponseException(HttpStatusCode.NotFound); 
            }
            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
            _context.SaveChanges();

        }


        //DELETE /API/CUSTOMER

        public Customer CreateCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.CustomerId == id);
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return customer;
        }




    }
}
