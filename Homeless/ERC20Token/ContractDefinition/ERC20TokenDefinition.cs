using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Homeless.Contracts.ERC20Token.ContractDefinition
{


    public partial class ERC20TokenDeployment : ERC20TokenDeploymentBase
    {
        public ERC20TokenDeployment() : base(BYTECODE) { }
        public ERC20TokenDeployment(string byteCode) : base(byteCode) { }
    }

    public class ERC20TokenDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60806040523480156200001157600080fd5b5060405162000df238038062000df28339810160408190526200003491620001de565b6000600255600680546001600160a01b031916331790558251620000609060039060208601906200008f565b506004805460ff191660ff84161790558051620000859060059060208401906200008f565b505050506200025e565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282620000c7576000855562000112565b82601f10620000e257805160ff191683800117855562000112565b8280016001018555821562000112579182015b8281111562000112578251825591602001919060010190620000f5565b506200012092915062000124565b5090565b5b8082111562000120576000815560010162000125565b600082601f8301126200014c578081fd5b81516001600160401b03808211156200016157fe5b6040516020601f8401601f19168201810183811183821017156200018157fe5b806040525081945083825286818588010111156200019e57600080fd5b600092505b83831015620001c25785830181015182840182015291820191620001a3565b83831115620001d45760008185840101525b5050505092915050565b600080600060608486031215620001f3578283fd5b83516001600160401b03808211156200020a578485fd5b62000218878388016200013b565b94506020860151915060ff8216821462000230578384fd5b60408601519193508082111562000245578283fd5b5062000254868287016200013b565b9150509250925092565b610b84806200026e6000396000f3fe608060405234801561001057600080fd5b50600436106100ea5760003560e01c80635c6581651161008c57806395d89b411161006657806395d89b41146101cd578063a9059cbb146101d5578063c1357c91146101e8578063dd62ed3e146101fb576100ea565b80635c6581651461019257806370a08231146101a55780637e893159146101b8576100ea565b80631d143848116100c85780631d1438481461014257806323b872dd1461015757806327e235e31461016a578063313ce5671461017d576100ea565b806306fdde03146100ef578063095ea7b31461010d57806318160ddd1461012d575b600080fd5b6100f761020e565b60405161010491906109c9565b60405180910390f35b61012061011b3660046107f7565b61029c565b60405161010491906109be565b610135610306565b6040516101049190610aad565b61014a61030c565b60405161010491906109aa565b6101206101653660046107b7565b61031b565b61013561017836600461075c565b610438565b61018561044a565b6040516101049190610ad7565b6101356101a036600461077f565b610453565b6101356101b336600461075c565b610470565b6101cb6101c636600461075c565b61048b565b005b6100f761051c565b6101206101e33660046107f7565b610577565b6101206101f6366004610822565b610605565b61013561020936600461077f565b610707565b6003805460408051602060026001851615610100026000190190941693909304601f810184900484028201840190925281815292918301828280156102945780601f1061026957610100808354040283529160200191610294565b820191906000526020600020905b81548152906001019060200180831161027757829003601f168201915b505050505081565b3360008181526001602090815260408083206001600160a01b038716808552925280832085905551919290917f8c5be1e5ebec7d5bd14f71427d1e84f3dd0314c0f7b2291e5b200ac8c7c3b925906102f5908690610aad565b60405180910390a350600192915050565b60025481565b6006546001600160a01b031681565b6001600160a01b038316600081815260016020908152604080832033845282528083205493835290829052812054909190831180159061035b5750828110155b6103805760405162461bcd60e51b815260040161037790610a50565b60405180910390fd5b6001600160a01b03808516600090815260208190526040808220805487019055918716815220805484900390556000198110156103e2576001600160a01b03851660009081526001602090815260408083203384529091529020805484900390555b836001600160a01b0316856001600160a01b03167fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef856040516104259190610aad565b60405180910390a3506001949350505050565b60006020819052908152604090205481565b60045460ff1681565b600160209081526000928352604080842090915290825290205481565b6001600160a01b031660009081526020819052604090205490565b6006546001600160a01b031633146104b55760405162461bcd60e51b815260040161037790610a2b565b6006546001600160a01b0382811691161461051957600680546001600160a01b038381166001600160a01b03198316811790935560405191169182917faf014c0f1e2d7c810cfe4f4ec7c675b9dbd7ad5fd0b058fe94776a447d6f333790600090a3505b50565b6005805460408051602060026001851615610100026000190190941693909304601f810184900484028201840190925281815292918301828280156102945780601f1061026957610100808354040283529160200191610294565b336000908152602081905260408120548211156105a65760405162461bcd60e51b8152600401610377906109dc565b33600081815260208190526040808220805486900390556001600160a01b03861680835291819020805486019055519091907fddf252ad1be2c89b69c2b068fc378daa952ba7f163c4a11628f55a4df523b3ef906102f5908690610aad565b6006546000906001600160a01b031633146106325760405162461bcd60e51b815260040161037790610a2b565b60005b82518110156106fe57610646610732565b83828151811061065257fe5b60209081029190910181015180820151600280548201905581516001600160a01b03166000908152928390526040928390208054909101905581810151915190925061069e919061098e565b604051809103902081600001516001600160a01b03167f04a44fabb56e915a0bc152617a45d942ab5f42977df4f574daef119fc7f7f408836020015184604001516040516106ed929190610ab6565b60405180910390a350600101610635565b50600192915050565b6001600160a01b03918216600090815260016020908152604080832093909416825291909152205490565b604051806060016040528060006001600160a01b0316815260200160008152602001606081525090565b60006020828403121561076d578081fd5b813561077881610b39565b9392505050565b60008060408385031215610791578081fd5b823561079c81610b39565b915060208301356107ac81610b39565b809150509250929050565b6000806000606084860312156107cb578081fd5b83356107d681610b39565b925060208401356107e681610b39565b929592945050506040919091013590565b60008060408385031215610809578182fd5b823561081481610b39565b946020939093013593505050565b60006020808385031215610834578182fd5b823567ffffffffffffffff8082111561084b578384fd5b818501915085601f83011261085e578384fd5b81358181111561086a57fe5b6108778485830201610ae5565b81815284810190848601875b848110156109535781358701601f196060828e03820112156108a3578a8bfd5b60408051606081018181108b821117156108b957fe5b8252838c01356108c881610b39565b8152838201358c82015260608401358a8111156108e3578d8efd5b8085019450508e603f8501126108f7578c8dfd5b8b8401358a81111561090557fe5b6109158d85601f84011601610ae5565b93508084528f8382870101111561092a578d8efd5b808386018e86013783018c018d9052908101919091528552509287019290870190600101610883565b50909998505050505050505050565b6000815180845261097a816020860160208601610b09565b601f01601f19169290920160200192915050565b600082516109a0818460208701610b09565b9190910192915050565b6001600160a01b0391909116815260200190565b901515815260200190565b6000602082526107786020830184610962565b6020808252602f908201527f746f6b656e2062616c616e6365206973206c6f776572207468616e207468652060408201526e1d985b1d59481c995c5d595cdd1959608a1b606082015260800190565b6020808252600b908201526a27b7363c9024b9b9bab2b960a91b604082015260600190565b60208082526039908201527f746f6b656e2062616c616e6365206f7220616c6c6f77616e6365206973206c6f60408201527f776572207468616e20616d6f756e742072657175657374656400000000000000606082015260800190565b90815260200190565b600083825260406020830152610acf6040830184610962565b949350505050565b60ff91909116815260200190565b60405181810167ffffffffffffffff81118282101715610b0157fe5b604052919050565b60005b83811015610b24578181015183820152602001610b0c565b83811115610b33576000848401525b50505050565b6001600160a01b038116811461051957600080fdfea2646970667358221220df128648e4703bcf0bd09051c736108c9c48fc1d58a9e4abd9c70b38822af01664736f6c63430007050033";
        public ERC20TokenDeploymentBase() : base(BYTECODE) { }
        public ERC20TokenDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("string", "_tokenName", 1)]
        public virtual string TokenName { get; set; }
        [Parameter("uint8", "_decimalUnits", 2)]
        public virtual byte DecimalUnits { get; set; }
        [Parameter("string", "_tokenSymbol", 3)]
        public virtual string TokenSymbol { get; set; }
    }

    public partial class AllowanceFunction : AllowanceFunctionBase { }

    [Function("allowance", "uint256")]
    public class AllowanceFunctionBase : FunctionMessage
    {
        [Parameter("address", "_owner", 1)]
        public virtual string Owner { get; set; }
        [Parameter("address", "_spender", 2)]
        public virtual string Spender { get; set; }
    }

    public partial class AllowedFunction : AllowedFunctionBase { }

    [Function("allowed", "uint256")]
    public class AllowedFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
        [Parameter("address", "", 2)]
        public virtual string ReturnValue2 { get; set; }
    }

    public partial class ApproveFunction : ApproveFunctionBase { }

    [Function("approve", "bool")]
    public class ApproveFunctionBase : FunctionMessage
    {
        [Parameter("address", "_spender", 1)]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "_value", 2)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class BalanceOfFunction : BalanceOfFunctionBase { }

    [Function("balanceOf", "uint256")]
    public class BalanceOfFunctionBase : FunctionMessage
    {
        [Parameter("address", "_owner", 1)]
        public virtual string Owner { get; set; }
    }

    public partial class BalancesFunction : BalancesFunctionBase { }

    [Function("balances", "uint256")]
    public class BalancesFunctionBase : FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class ChangeIssuerFunction : ChangeIssuerFunctionBase { }

    [Function("changeIssuer")]
    public class ChangeIssuerFunctionBase : FunctionMessage
    {
        [Parameter("address", "_newIssuer", 1)]
        public virtual string NewIssuer { get; set; }
    }

    public partial class DecimalsFunction : DecimalsFunctionBase { }

    [Function("decimals", "uint8")]
    public class DecimalsFunctionBase : FunctionMessage
    {

    }

    public partial class IssueTokenToDonorFunction : IssueTokenToDonorFunctionBase { }

    [Function("issueTokenToDonor", "bool")]
    public class IssueTokenToDonorFunctionBase : FunctionMessage
    {
        [Parameter("tuple[]", "_donorIssuance", 1)]
        public virtual List<DonorIssuance> DonorIssuance { get; set; }
    }

    public partial class IssuerFunction : IssuerFunctionBase { }

    [Function("issuer", "address")]
    public class IssuerFunctionBase : FunctionMessage
    {

    }

    public partial class NameFunction : NameFunctionBase { }

    [Function("name", "string")]
    public class NameFunctionBase : FunctionMessage
    {

    }

    public partial class SymbolFunction : SymbolFunctionBase { }

    [Function("symbol", "string")]
    public class SymbolFunctionBase : FunctionMessage
    {

    }

    public partial class TotalSupplyFunction : TotalSupplyFunctionBase { }

    [Function("totalSupply", "uint256")]
    public class TotalSupplyFunctionBase : FunctionMessage
    {

    }

    public partial class TransferFunction : TransferFunctionBase { }

    [Function("transfer", "bool")]
    public class TransferFunctionBase : FunctionMessage
    {
        [Parameter("address", "_to", 1)]
        public virtual string To { get; set; }
        [Parameter("uint256", "_value", 2)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class TransferFromFunction : TransferFromFunctionBase { }

    [Function("transferFrom", "bool")]
    public class TransferFromFunctionBase : FunctionMessage
    {
        [Parameter("address", "_from", 1)]
        public virtual string From { get; set; }
        [Parameter("address", "_to", 2)]
        public virtual string To { get; set; }
        [Parameter("uint256", "_value", 3)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class ApprovalEventDTO : ApprovalEventDTOBase { }

    [Event("Approval")]
    public class ApprovalEventDTOBase : IEventDTO
    {
        [Parameter("address", "_owner", 1, true )]
        public virtual string Owner { get; set; }
        [Parameter("address", "_spender", 2, true )]
        public virtual string Spender { get; set; }
        [Parameter("uint256", "_value", 3, false )]
        public virtual BigInteger Value { get; set; }
    }

    public partial class IssueToDonorEventDTO : IssueToDonorEventDTOBase { }

    [Event("IssueToDonor")]
    public class IssueToDonorEventDTOBase : IEventDTO
    {
        [Parameter("address", "_donor", 1, true )]
        public virtual string Donor { get; set; }
        
        [Parameter("bytes", "txnHashIndexed", 2, true )]
        public virtual byte[] TxnHashIndexed { get; set; }

        [Parameter("uint256", "_value", 3, false )]
        public virtual BigInteger Value { get; set; }
        [Parameter("bytes", "txnHash", 4, false )]
        public virtual byte[] TxnHash { get; set; }
    }

    public partial class IssuerChangedEventDTO : IssuerChangedEventDTOBase { }

    [Event("IssuerChanged")]
    public class IssuerChangedEventDTOBase : IEventDTO
    {
        [Parameter("address", "_newIssuer", 1, true )]
        public virtual string NewIssuer { get; set; }
        [Parameter("address", "_oldIssuer", 2, true )]
        public virtual string OldIssuer { get; set; }
    }

    public partial class TransferEventDTO : TransferEventDTOBase { }

    [Event("Transfer")]
    public class TransferEventDTOBase : IEventDTO
    {
        [Parameter("address", "_from", 1, true )]
        public virtual string From { get; set; }
        [Parameter("address", "_to", 2, true )]
        public virtual string To { get; set; }
        [Parameter("uint256", "_value", 3, false )]
        public virtual BigInteger Value { get; set; }
    }

    public partial class AllowanceOutputDTO : AllowanceOutputDTOBase { }

    [FunctionOutput]
    public class AllowanceOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "remaining", 1)]
        public virtual BigInteger Remaining { get; set; }
    }

    public partial class AllowedOutputDTO : AllowedOutputDTOBase { }

    [FunctionOutput]
    public class AllowedOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class BalanceOfOutputDTO : BalanceOfOutputDTOBase { }

    [FunctionOutput]
    public class BalanceOfOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "balance", 1)]
        public virtual BigInteger Balance { get; set; }
    }

    public partial class BalancesOutputDTO : BalancesOutputDTOBase { }

    [FunctionOutput]
    public class BalancesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }



    public partial class DecimalsOutputDTO : DecimalsOutputDTOBase { }

    [FunctionOutput]
    public class DecimalsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 { get; set; }
    }



    public partial class IssuerOutputDTO : IssuerOutputDTOBase { }

    [FunctionOutput]
    public class IssuerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class NameOutputDTO : NameOutputDTOBase { }

    [FunctionOutput]
    public class NameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class SymbolOutputDTO : SymbolOutputDTOBase { }

    [FunctionOutput]
    public class SymbolOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class TotalSupplyOutputDTO : TotalSupplyOutputDTOBase { }

    [FunctionOutput]
    public class TotalSupplyOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }




}
