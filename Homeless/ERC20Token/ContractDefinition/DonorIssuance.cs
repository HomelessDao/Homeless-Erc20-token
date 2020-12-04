using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Homeless.Contracts.ERC20Token.ContractDefinition
{
    public partial class DonorIssuance : DonorIssuanceBase { }

    public class DonorIssuanceBase 
    {
        [Parameter("address", "donor", 1)]
        public virtual string Donor { get; set; }
        [Parameter("uint256", "value", 2)]
        public virtual BigInteger Value { get; set; }
        [Parameter("bytes", "txnHashDonation", 3)]
        public virtual byte[] TxnHashDonation { get; set; }
    }
}
