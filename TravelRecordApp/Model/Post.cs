using System;
using SQLite;

namespace TravelRecordApp.Model
{
    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string ExperienceTitle { get; set; }


        [MaxLength(250)]
        public string Experience { get; set; }

        public Post()
        {
            
        }
    }
}

