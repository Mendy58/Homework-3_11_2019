using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework_3_11_2019.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public Int64 PhoneNumber { get; set; }
        public bool IsUsers { get; set; }
    }
}