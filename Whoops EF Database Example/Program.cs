using System;
using System.Data.SqlClient;
using System.Linq;

// Install-Package EntityFramework via Nuget Package Manager console.

namespace Whoops_EF_Database_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Build connection string
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = @"DESKTOP-T74VM4I\SERVER17PASS",
                    ConnectTimeout = 30,
                    Encrypt = true,
                    TrustServerCertificate = false,
                    IntegratedSecurity = true,
                    ApplicationIntent = ApplicationIntent.ReadWrite,
                    MultiSubnetFailover = false,
                    InitialCatalog = "EFSampleDB"
                };

                using (var context = new EFSampleContext(builder.ConnectionString))
                {
                    Console.WriteLine("Created database schema from C# Classes.");

                    // Create demo: Create a User instance and saving it to the database
                    var newUser = new User { FirstName = "Anna", LastName = "Shrestinian" };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                    Console.WriteLine("\nCreated User: " + newUser);

                    // Create demo: Create a Task instance and save it to the database
                    var newTask = new Task()
                    { Title = "Ship Helsinki", IsComplete = false, DueDate = DateTime.Parse("04-01-2019") };
                    context.Tasks.Add(newTask);
                    context.SaveChanges();
                    Console.WriteLine("\nCreated Task: " + newTask);

                    // Association demo: Assign task to user
                    newTask.AssignedTo = newUser;
                    context.SaveChanges();
                    Console.WriteLine(
                        "\nAssigned Task: '" + newTask.Title + "' to user '" + newUser.GetFullName() + "'");

                    // Read demo: find incomplete tasks assigned to user 'Anna'
                    Console.WriteLine("\nIncomplete tasks assigned to 'Anna':");
                    var query = from t in context.Tasks
                                where t.IsComplete == false && t.AssignedTo.FirstName.Equals("Anna")
                                select t;
                    foreach (var t in query)
                    {
                        Console.WriteLine(t.ToString());
                    }

                    // Update demo: change the 'dueDate' of a task
                    var taskToUpdate = context.Tasks.First();
                    Console.WriteLine("\nUpdating task: " + taskToUpdate);
                    taskToUpdate.DueDate = DateTime.Parse("30-03-2019");
                    context.SaveChanges();
                    Console.WriteLine("dueDate changed: " + taskToUpdate);

                    // Delete demo: delete all tasks with a dueDate in 2019
                    Console.WriteLine("\nDeleting all tasks with a dueDate in 2019");
                    var dueDate2019 = DateTime.Parse("31-12-2019");
                    query = from t in context.Tasks
                            where t.DueDate < dueDate2019
                            select t;
                    foreach (var t in query)
                    {
                        Console.WriteLine("Deleting task: " + t);
                        context.Tasks.Remove(t);
                    }

                    context.SaveChanges();

                    // Show tasks after the 'Delete' operation - there should be 0 tasks
                    Console.WriteLine("\nTasks after delete:");
                    var tasksAfterDelete = (from t in context.Tasks select t).ToList();
                    if (tasksAfterDelete.Count == 0)
                    {
                        Console.WriteLine("[None]");
                    }
                    else
                    {
                        foreach (var t in query)
                        {
                            Console.WriteLine(t.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
        }
    }
}

