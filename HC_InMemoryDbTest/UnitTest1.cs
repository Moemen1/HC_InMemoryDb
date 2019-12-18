using HC_InMemoryDb.Controllers;
using HC_InMemoryDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HC_InMemoryDbTest
{   
    public class UnitTest1
    {
        string databaseNaam;
        private HC_InMemoryDbContext GetInMemoryDatabase(bool nieuweDb)
        {
            if(nieuweDb) databaseNaam = Guid.NewGuid().ToString();
            
            var options = new DbContextOptionsBuilder<HC_InMemoryDbContext>()
                .UseInMemoryDatabase(databaseNaam)
                .Options;

            return new HC_InMemoryDbContext(options);
        }

        private HC_InMemoryDbContext GetInMemoryDatabaseMetData()
        {
            var c = GetInMemoryDatabase(false);

            c.Add(new Student { Id = 1, Naam = "Jan", Leeftijd = 20 });
            c.Add(new Student { Id = 2, Naam = "Imane", Leeftijd = 19 });
            c.Add(new Student { Id = 3, Naam = "John", Leeftijd = 23 });

            c.SaveChanges();

            return GetInMemoryDatabase(false);
        }
        [Fact]
        public async Task Test1()
        {
            var c = GetInMemoryDatabaseMetData();          


            StudentsController s = new StudentsController(c);

            var result = s.Index();

            var viewResult = Assert.IsType<ViewResult>(await result);

            var model = Assert.IsAssignableFrom<List<Student>>(viewResult.ViewData.Model);

            Assert.Equal(3, model.Count);
        }
    }
}
