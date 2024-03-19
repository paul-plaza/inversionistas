using Investors.Kernel.Shared.Operations.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investors.Kernel.Shared.Investors.Domain.Services
{
    public interface IInvestorManager
    {
        InvestorService Investors { get; }
    }
}
