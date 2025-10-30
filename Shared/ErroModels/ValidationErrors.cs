using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErroModels
{
    public class ValidationErrors
    {
        public string Filed { get; set; } = null!;
        public IEnumerable<string> Errors { get; set; } = [];
    }
}
