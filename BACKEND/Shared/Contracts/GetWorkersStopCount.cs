using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts
{
    public record GetWorkersStopCountRequest
    {
        public int StopId { get; set; }
    }

    public record GetWorkersStopCountResponse
    {
        //public int StopId { get; set; }
        public int Count { get; set; }
    }
}
