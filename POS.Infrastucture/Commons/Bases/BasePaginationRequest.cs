using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastucture.Commons.Bases
{
    public class BasePaginationRequest
    {
        public int NumPage {get; set;} = 1 ;
        public int NumRecordPage { get; set; } = 10;
        public readonly int NumMaxRecordPage  = 50;
        public string Order { get; set; } = "asc";
        public string? Sort { get; set; } = null;

        public int Records
        {
            get => NumRecordPage;
            set
            {
                NumRecordPage = (value > NumMaxRecordPage) ?  NumMaxRecordPage : value ;
            }
        }

    }
}
