using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Fall2015.Models;

namespace Fall2015.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<Fall2015.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Fall2015.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //the first parameter (Email) is used to check if data exists already: http://blogs.msdn.com/b/rickandy/archive/2013/02/12/seeding-and-debugging-entity-framework-ef-dbs.aspx
            context.Students.AddOrUpdate(s => s.Email, new Student[]
                                         {
                                             new Student
                                             {
                                                 Firstname = "Christian",
                                                 Lastname =  "Kirschberg",
                                                 Email = "ckirschberg@gmail.com",
                                                 MobilePhone = "61690509",
                                                 ProfileImagePath = "/ProfileImages/01a8ce50-58e7-407f-b0dd-c8d68c38d51e.jpg", //requires the image to be located here
                                                 EducationId = 1
                                             },
                                             new Student
                                             {
                                                 Firstname = "Hans",
                                                 Lastname = "Hansen",
                                                 Email = "hans@hans.dk",
                                                 MobilePhone = "12345678",
                                                 ProfileImagePath = "/ProfileImages/07adfb50-6675-4076-83e5-c1a1ca903a7c.jpg",
                                                 EducationId = 2
                                             },
                                             new Student
                                             {
                                                 Firstname = "Jens",
                                                 Lastname = "Jensen",
                                                 Email = "jens@jens.dk",
                                                 MobilePhone = "12345638",
                                                 ProfileImagePath = "/ProfileImages/0db3826f-e88a-46cb-8954-fb2e25ffedee.jpg",
                                                 EducationId = 3
                                             },
                                             new Student
                                             {
                                                 Firstname = "Helle",
                                                 Lastname = "Hellesen",
                                                 Email = "helle@helle.dk",
                                                 MobilePhone = "12345632",
                                                 ProfileImagePath = "/ProfileImages/1565d2ad-55ec-4a42-87a0-8ac9c15b3b02.jpg",
                                                 EducationId = 1
                                             },
                                             new Student
                                             {
                                                 Firstname = "Berit",
                                                 Lastname = "Beritsen",
                                                 Email = "berit@berit.dk",
                                                 MobilePhone = "12345631",
                                                 ProfileImagePath = "/ProfileImages/1707c461-a8d8-4c65-a0dc-f07cf5454fba.jpg",
                                                 EducationId = 2
                                             },
                                             new Student
                                             {
                                                 Firstname = "Allan",
                                                 Lastname = "Allansen",
                                                 Email = "allan@allan.dk",
                                                 MobilePhone = "12345632",
                                                 ProfileImagePath = "/ProfileImages/19f62588-a61f-4601-8065-c913695451cd.jpg",
                                                 EducationId = 3
                                             },
                                             new Student
                                             {
                                                 Firstname = "Jesper",
                                                 Lastname = "Jespersen",
                                                 Email = "jesper@jesper.dk",
                                                 MobilePhone = "12315631",
                                                 ProfileImagePath = "/ProfileImages/20956b8c-0d93-43e3-94e7-88a046bd09b7.jpg",
                                                 EducationId = 1
                                             }
                                         });



            context.CompetencyHeaders.AddOrUpdate(h => h.CompetencyHeaderId, new CompetencyHeader[]
                                        {
                                            new CompetencyHeader
                                            {
                                                CompetencyHeaderId = 1,
                                                Name="DESIGN"
                                            },
                                            new CompetencyHeader{
                                                CompetencyHeaderId = 2,
                                                Name="BUSINESS"
                                            },
                                            new CompetencyHeader
                                            {
                                                CompetencyHeaderId = 3,
                                                Name="EDUCATION"
                                            }
                                        });



            context.Competencies.AddOrUpdate(c => c.CompetencyId, new Competency[]
                                        {
                                            new Competency
                                            {
                                                CompetencyId = 1,
                                                CompetencyHeaderId = 1,
                                                Name = "Visual Communication"
                                            },
                                            new Competency
                                            {
                                                CompetencyId = 2,
                                                CompetencyHeaderId = 1,
                                                Name = "App Design"
                                            },
                                            new Competency
                                            {
                                                CompetencyId = 3,
                                                CompetencyHeaderId = 1,
                                                Name = "Web Design"
                                            },
                                            new Competency
                                            {
                                                CompetencyId = 4,
                                                CompetencyHeaderId = 2,
                                                Name = "Marketing"
                                            },
                                            new Competency
                                            {
                                                CompetencyId = 5,
                                                CompetencyHeaderId = 2,
                                                Name = "Business Orginasation"
                                            },
                                            new Competency
                                            {
                                                CompetencyId = 6,
                                                CompetencyHeaderId = 2,
                                                Name = "Trade"
                                            },
                                            new Competency
                                            {
                                                CompetencyId = 7,
                                                CompetencyHeaderId = 3,
                                                Name = "Educational Development"
                                            },
                                            new Competency
                                            {
                                                CompetencyId = 8,
                                                CompetencyHeaderId = 3,
                                                Name = "Learning Styles"
                                            },
                                            new Competency
                                            {
                                                CompetencyId = 9,
                                                CompetencyHeaderId = 3,
                                                Name = "ICT"
                                            }
                                         });



            context.Educations.AddOrUpdate(e => e.EducationId, new Education[]
                                        {
                                            new Education
                                            {
                                                EducationId = 1,
                                                Address = "Lygten 37",
                                                City = "Copenhagen",
                                                Name = "Datamatiker",
                                                ZipCode = 2200
                                            },
                                            new Education
                                            {
                                                EducationId = 2,
                                                Address = "Guldbergsgade 29N",
                                                City = "Copenhagen",
                                                Name = "E-designer",
                                                ZipCode = 2200
                                            },
                                            new Education
                                            {
                                                EducationId = 3,
                                                Address = "Lygten 16",
                                                City = "Copenhagen",
                                                Name = "Multimediedesigner",
                                                ZipCode = 2200
                                            }

                                        });


        }
    }
}
