using System;
using System.Collections.Generic;
using System.Text;

namespace WannaWhat.DTOs
{
    public abstract class DTOBase<ResponseType>
    {
        public bool IsValid { get; set; }
        public String Description { get; set; }
        public int Status { get; set; }
        public ResponseType Payload { get; set; }
        public List<string> Errors { get; set; }

    }
}
