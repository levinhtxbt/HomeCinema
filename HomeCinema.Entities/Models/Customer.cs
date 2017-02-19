using System;
using System.Collections;
using System.Collections.Generic;

namespace HomeCinema.Entities.Models
{
    public class Customer : IEntityBase
    {
        public Customer()
        {
            Rentals = new List<Rental>();
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdentityCard { get; set; }
        public Guid UniqueKey { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}