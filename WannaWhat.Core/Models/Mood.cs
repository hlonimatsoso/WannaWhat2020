using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WannaWhat.Core.Models
{
    public class Mood
    {
        [Key]
        public string MoodId { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Description { get; set; }

        public List<UserMoods> UserMoods { get; set; }
    }
}
