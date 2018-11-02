using System;

namespace Exam
{
    public class Book
    {
        //Pk
        public int BookId {get; set;}
        public string Title {get; set;}
        public string Publisher {get; set;}
        public string PublishDate {get; set;}
        public int Pages {get; set;}

        //FK
        
        public int AuthorID {get; set;}


        public override string ToString()
        {
            return $"{Title} - {Publisher} - {PublishDate} - {Pages}";
        }

    }
    
}