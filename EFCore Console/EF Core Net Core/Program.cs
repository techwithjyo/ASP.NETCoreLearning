// See https://aka.ms/new-console-template for more information
using EF_Core_Net_Core;

Console.WriteLine("Hello, World!");


AddDataToDb();
FetchDataFromDb();
UpdateNameInDb();
DeleteNameInDb();

void DeleteNameInDb()
{
    using (var context = new SchoolContext())
    {
        var std = context.Students.First<Student>();
        context.Students.Remove(std);
        context.SaveChanges();
    }
}

void UpdateNameInDb()
{
    using (var context = new SchoolContext())
    {
        var std = context.Students.ToList();
        foreach (var student in std)
        {
            student.Name = "Changed Name";
        }
        context.SaveChanges();
    }
}

void FetchDataFromDb()
{
    var context = new SchoolContext();
    var studentsWithSameName = context.Students.Select(x => x.Name).ToList();
    foreach ( var student in studentsWithSameName )
    {
        Console.WriteLine(student);
    }
}

void AddDataToDb()
{
    using (var context = new SchoolContext())
    {
        for (int i = 0; i < 10; i++)
        {
            var std = new Student()
            {
                Name = "Bill " + Guid.NewGuid(),
                SomeRandomStudentString = "Random" + Guid.NewGuid()
            };

            context.Students.Add(std);
        }

        context.SaveChanges();
    }
}