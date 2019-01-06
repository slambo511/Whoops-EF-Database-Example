using System.Collections.Generic;

namespace EFS_DatabaseExample
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual IList<Task> Tasks { get; set; }

        public string GetFullName()
        {
            return this.FirstName + " " + LastName;
        }

        public override string ToString()
        {
            return "User [id=" + this.UserId + ", name=" + this.GetFullName() + "]";
        }
    }
}