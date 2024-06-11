using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_Internal
{
    static partial class Helper
    {
#nullable enable
        internal static StartOfRound? StartOfRound => StartOfRound.Instance.Unfake();

#nullable restore
    }
}
