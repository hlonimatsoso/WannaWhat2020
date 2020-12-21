using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WannaWhat.Core.Models
{
    public class UserInventory
    {
        
        [ForeignKey("UserId")]
        public WannaWhatUser User { get; set; }

        [Key]
        public string UserId { get; set; }

        public int XP { get; set; }

        public int HeartCount { get; set; }

        public int ReceivedHeartsCount { get; set; }

        public int WincCount { get; set; }

        public int ReceivedWincCount { get; set; }

        public int FakeRabitsCount { get; set; }

        public int RealRabitsCount { get; set; }



    }
}
