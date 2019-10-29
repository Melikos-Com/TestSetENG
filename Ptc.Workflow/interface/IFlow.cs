using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Workflow
{
    public interface IFlow
    {
        IDictionary<int, IStep> _steps { get; set; }

        void Add(int sts, IStep step);

        void Remove(int sts);

        void Update(int sts, IStep step);
    }
}
