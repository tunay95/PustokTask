﻿using Microsoft.AspNetCore.Identity;

namespace WebAppRelation.Models
{
    public class AppUser : IdentityUser 
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsRemained { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public List<Order> OrderItems { get; set; }
    }
}
