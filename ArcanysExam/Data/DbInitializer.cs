using ArcanysExam.Models;
using System.Linq;

namespace ArcanysExam.Data
{
    public class DbInitializer
    {
        public static void Initialize(ExamContext context)
        {
            if (context.Files.Any())
            {
                return; // DB has been seeded
            }

            var files = new File[]
            {
                new File { Name = "test1", Blob = new byte[0] },
                new File { Name = "test2", Blob = new byte[0] },
                new File { Name = "test3", Blob = new byte[0] }
            };

            foreach (var f in files)
            {
                context.Files.Add(f);
            }

            context.SaveChanges();
        }
    }
}
