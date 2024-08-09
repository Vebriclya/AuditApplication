using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            using var db = new AuditContext();
            Console.WriteLine($"Database path: {db.DbPath}");

            db.Database.EnsureCreated();
            Console.WriteLine("Database created successfully.");

            // Create
            Console.WriteLine("Inserting a new audit");
            var newAudit = new Audit { Name = "Demonstration Building Audit", CreatedDate = DateTime.Now };
            db.Add(newAudit);
            db.SaveChanges();

            // Read
            Console.WriteLine("Querying for an audit");
            var audit = db.Audits
                .OrderBy(b => b.AuditId)
                .First();

            // Update
            Console.WriteLine("Updating the audit and adding a section");
            audit.Name = "Updated Demonstration Building Audit";
            audit.Sections.Add(
                new Section { Name = "General First Impression - External" });
            db.SaveChanges();

            // Delete
            Console.WriteLine("Delete the audit");
            db.Audits.Remove(audit);
            db.SaveChanges();

            Console.WriteLine("Operations completed successfully.");
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            if(ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}