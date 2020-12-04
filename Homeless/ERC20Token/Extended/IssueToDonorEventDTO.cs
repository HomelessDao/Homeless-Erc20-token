using Homeless.Contracts.ERC20Token.ContractDefinition;
using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homeless.Contracts.ERC20Token.ContractDefinition
{
    [Event("IssueToDonor")]
    public partial class IssueToDonorEventDTO : IssueToDonorEventDTOBase
    {
        [Parameter("bytes", "txnHashIndexed", 2, true)]
        public new string TxnHashIndexed { get; set; }
    }
}
