﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppRelation.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string BookCode { get; set; }
        public double Price { get; set; }
        public bool Availability { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Brand? Brand { get; set; }
        public int? BrandId { get; set; }
        public Category? Category { get; set; }
        public List<BookImages>? BookImages { get; set; }
        public List<Tag>? Tag { get; set; }
    }
}
