using System;
using System.Collections.Generic;
using System.Text;

namespace AppDWC.Models
{
    public class Quotes
    {

            public int id { get; set; }
            public string nome { get; set; }
            public string specialty { get; set; }
            public int request_id { get; set; }
            public DateTime start_date { get; set; }
            public string words { get; set; }
            public int turnaround { get; set; }
            public string from { get; set; }
            public string to { get; set; }
            public string type { get; set; }
            public int value { get; set; }
            public string state { get; set; }
    }


    }
