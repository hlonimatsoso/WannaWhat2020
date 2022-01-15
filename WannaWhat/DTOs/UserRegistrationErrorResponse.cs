using System;
using System.Collections.Generic;
using System.Text;

namespace WannaWhat.DTOs
{
    public class UserRegistrationErrorResponse: UserRegistrationResponse
    {

        public String type { get; set; }
        public String title { get; set; }
        public int status { get; set; }

        public String traceId { get; set; }

        public Errors errors { get; set; }
    }

    public class Errors
    {
        public List<string> Email { get; set; }
    }
}
