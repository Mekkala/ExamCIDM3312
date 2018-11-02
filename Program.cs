using System;
using System.Linq;
using System.Collections.Generic;

namespace Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            SeedDatabase();
        }
         public static void SeedDatabase()
        {
            using(var db = new AppDbContext())
            {
                try
                {
  
                    db.Database.EnsureCreated();

                    if(!db.Books.Any() && !db.Authors.Any())
                
                    {
                        

                        IList<Book> bList = new List<Book>() 
                        { 
                            
                            new Book() {BookId = 1, Title = "Pro ASP.NET Core MVC 2 7th ed. Edition", Publisher = "Apress", PublishDate="October 25, 2017",  Pages =  1017, AuthorID = 101 },
                            new Book() {BookId = 2, Title = "Pro Angular 6 3rd Edition", Publisher = "Apress", PublishDate="October 10, 2018",  Pages =  776, AuthorID = 101  },
                            new Book() {BookId = 3, Title = "Programming Microsoft Azure Service Fabric (Developer Reference) 2nd Edition", Publisher = "Microsoft Press", PublishDate="May 25, 2018",  Pages =  528, AuthorID = 152},
                         
                    
                        };  

                        db.Books.AddRange(bList);

                        IList<Author> aList = new List<Author>()
                        {
                           new Author() {AuthorID = 101, FirstName ="Adam", LastName = "Freeman"},
                            new Author() {AuthorID = 152, FirstName ="Haishi", LastName = "Bai"},
                            
                        };

                        db.Authors.AddRange(aList);

                        db.SaveChanges();
                       
                    }
                    else
                    {
                        // Where method
                        // 1. Show book table
                        var bookRecord = db.Books.ToList();
                        Console.WriteLine("*********************Records in the Books table***********************");
                        foreach(Book b in bookRecord)
                        {
                            Console.WriteLine(b);
                        }

                        // 2. Books Published by "APress"

                        var bookByApress = db.Books.Where(b => b.Publisher == "Apress");
                        Console.WriteLine("*********************Books Published by APress***************************");
                        foreach(Book b in bookByApress)
                        {
                            Console.WriteLine(b);
                        }

                        // 3. Books whose author's first name is the shortest

                        /* var bookAuthor = db.Authors.Where(a => a.FirstName != "Adam");
                        Console.WriteLine("*****************Books whose author's first name is shortest**********************");
                        foreach(Author a in bookAuthor)
                        {
                            Console.WriteLine(a);
                        }
                        */

                        

                        // Find Method
                        //1. author named "Adam" 
                        var authorAdam = db.Authors.Where(a => a.FirstName == "Adam"); 
                        Console.WriteLine("*************************Author Named Adam******************************");
                        foreach(Author a in authorAdam)
                        {
                            Console.WriteLine(a);
                        }
                        
                        // 2. book whose page count is greater than 1000
                        Console.WriteLine("******************************Book whose page count is greater than 1000************************");
                        var bookPage = from b in db.Books
                                        where b.Pages >= 1000 
                                        select b;
                        foreach(Book b in bookPage)
                        {
                            Console.WriteLine(b);
                        }

                        //OrderBy Method 

                        //2. Books sorted by book title descending
                        var orderBook = db.Books.OrderByDescending(b => b.Title);
                        Console.WriteLine("**********************************Book Sorted By Title Descending***************************");
                        foreach(Book b in orderBook)
                        {
                            Console.WriteLine(b);

                        }         

                        //GroupBy and OrderBy Methods
                        //1. Books Grouped by publisher
                        Console.WriteLine("***************************************Books Grouped by publisher******************************");
                        var groupPublisher = db.Books.GroupBy(b => b.Publisher);
                        foreach(var publishG in groupPublisher)
                        {
                            Console.WriteLine($"Publisher Group: {publishG.Key}");
                            foreach (Book b in publishG)
                            {
                                Console.WriteLine(b);
                            }
                        }

                        //2. Books Grouped by publisher and sorted by Author's last name
                        /*Console.WriteLine("***************Books Grouped by publisher and sorted by Author's last name****************");
                        var groupJoin = db.Books.Join(db.Authors,
                                                      b => b.AuthorID,
                                                      a => a.AuthorID,
                                                      (b, a) => new
                                                      {
                                                          Book = b.Title,
                                                          Author = a.LastName,

                                                      });
                        var bookAndALast = groupJoin.OrderBy(s => s.LastName);
                        foreach(var b in bookAndALast)
                        {
                            Console.WriteLine($"{b.Title} - {a.AuthorID}");
                        } 
                        */                             


                    }
                }
                catch(Exception exp)
                {
                    Console.Error.WriteLine(exp.Message);
                }
            }
        
        }   
    }
}    
