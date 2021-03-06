﻿using System.Collections.Generic;

namespace Whoops_EF_Database_Example
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual IList<Task> Tasks { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + LastName;
        }

        public override string ToString()
        {
            return "User [id=" + UserId + ", name=" + GetFullName() + "]";
        }
    }
}