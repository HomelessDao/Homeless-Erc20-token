using System.Numerics;
using Nethereum.Signer;
using Nethereum.Util;
using Nethereum.Web3.Accounts;
using Nethereum.XUnitEthereumClients;
using Xunit;
using Homeless.Contracts.ERC20Token;
using Nethereum.Web3;
using Homeless.Contracts.ERC20Token.ContractDefinition;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Nethereum.BlockchainProcessing.ProgressRepositories;
using Nethereum.Hex.HexConvertors.Extensions;
using System;
using Nethereum.JsonRpc.Client;

namespace Homeless.Testing
{
    [Collection(EthereumClientIntegrationFixture.ETHEREUM_CLIENT_COLLECTION_DEFAULT)]
    public class Erc20TokenTests
    {
        private readonly EthereumClientIntegrationFixture _ethereumClientIntegrationFixture;

        public Erc20TokenTests(EthereumClientIntegrationFixture ethereumClientIntegrationFixture)
        {
            _ethereumClientIntegrationFixture = ethereumClientIntegrationFixture;
        }
        

        [Fact]
        public async void ShouldIssueTokens()
        {

            var donor = "0x6C547791C3573c2093d81b919350DB1094707011";
            //Using ropsten infura if wanted for only a tests
            //var web3 = _ethereumClientIntegrationFixture.GetInfuraWeb3(InfuraNetwork.Ropsten);
            var web3 = _ethereumClientIntegrationFixture.GetWeb3();

            var erc20TokenDeployment = new ERC20TokenDeployment() { DecimalUnits = 18, TokenName = "Homeless", TokenSymbol = "Homeless" };

            //Deploy our custom token
            var tokenDeploymentReceipt = await ERC20TokenService.DeployContractAndWaitForReceiptAsync(web3, erc20TokenDeployment);
            
            //Creating a new service
            var tokenService = new ERC20TokenService(web3, tokenDeploymentReceipt.ContractAddress);

            var donorIssuance = new DonorIssuance()
            {
                Donor = donor,
                Value = Web3.Convert.ToWei(10),
                TxnHashDonation = "0x30912b7fa14ccf619f8cfbff4ada42aabbccaa0d6373ffdbe0f0b509ac8883d9".HexToByteArray()
            };

            var issueTokensReceipt = await tokenService.IssueTokenToDonorRequestAndWaitForReceiptAsync(new List<DonorIssuance> { donorIssuance });

            //validate the current balance
            var balance = await tokenService.BalanceOfQueryAsync(donor);
            Assert.Equal(10, Web3.Convert.FromWei(balance));

            //retrieving the event from the receipt
            var eventTransfer = issueTokensReceipt.DecodeAllEvents<IssueToDonorEventDTO>()[0];

            Assert.Equal(10, Web3.Convert.FromWei(eventTransfer.Event.Value));
            Assert.True(donor.IsTheSameAddress(eventTransfer.Event.Donor));
            Assert.True(donorIssuance.TxnHashDonation.ToHex().IsTheSameHex(eventTransfer.Event.TxnHash.ToHex()));

        }


        [Fact]
        public async void ShouldIssueMultipleTokens()
        {

            var donor = "0x6C547791C3573c2093d81b919350DB1094707011";
            //Using ropsten infura if wanted for only a tests
            //var web3 = _ethereumClientIntegrationFixture.GetInfuraWeb3(InfuraNetwork.Ropsten);
            var web3 = _ethereumClientIntegrationFixture.GetWeb3();

            var erc20TokenDeployment = new ERC20TokenDeployment() { DecimalUnits = 18, TokenName = "Homeless", TokenSymbol = "Homeless" };

            //Deploy our custom token
            var tokenDeploymentReceipt = await ERC20TokenService.DeployContractAndWaitForReceiptAsync(web3, erc20TokenDeployment);

            //Creating a new service
            var tokenService = new ERC20TokenService(web3, tokenDeploymentReceipt.ContractAddress);

            var donorIssuance1 = new DonorIssuance()
            {
                Donor = donor,
                Value = Web3.Convert.ToWei(10),
                TxnHashDonation = "0x30912b7fa14ccf619f8cfbff4ada42aabbccaa0d6373ffdbe0f0b509ac8883d9".HexToByteArray()
            };

            var donorIssuance2 = new DonorIssuance()
            {
                Donor = donor,
                Value = Web3.Convert.ToWei(10),
                TxnHashDonation = "0x30912b7fa14ccf619f8cfbff4ada42aabbccaa0d6373ffdbe0f0b509ac8883d0".HexToByteArray()
            };

            var donorIssuance3 = new DonorIssuance()
            {
                Donor = donor,
                Value = Web3.Convert.ToWei(10),
                TxnHashDonation = "0x30912b7fa14ccf619f8cfbff4ada42aabbccaa0d6373ffdbe0f0b509ac8883d1".HexToByteArray()
            };

            var issueTokensReceipt = await tokenService.IssueTokenToDonorRequestAndWaitForReceiptAsync(new List<DonorIssuance> { donorIssuance1, donorIssuance2, donorIssuance3 });

            //validate the current balance
            var balance = await tokenService.BalanceOfQueryAsync(donor);
            Assert.Equal(30, Web3.Convert.FromWei(balance));

            //retrieving the event from the receipt
            var eventTransfer = issueTokensReceipt.DecodeAllEvents<IssueToDonorEventDTO>()[0];

            Assert.Equal(10, Web3.Convert.FromWei(eventTransfer.Event.Value));
            Assert.True(donor.IsTheSameAddress(eventTransfer.Event.Donor));
            Assert.True(donorIssuance1.TxnHashDonation.ToHex().IsTheSameHex(eventTransfer.Event.TxnHash.ToHex()));


            var eventTransfer2 = issueTokensReceipt.DecodeAllEvents<IssueToDonorEventDTO>()[1];

            Assert.Equal(10, Web3.Convert.FromWei(eventTransfer2.Event.Value));
            Assert.True(donor.IsTheSameAddress(eventTransfer2.Event.Donor));
            Assert.True(donorIssuance2.TxnHashDonation.ToHex().IsTheSameHex(eventTransfer2.Event.TxnHash.ToHex()));

        }


        [Fact]
        public async void ShoulAllowOnlyIssuerToChangeIssuer()
        {

            var newIssuer = "0x6C547791C3573c2093d81b919350DB1094707011";
            //Using ropsten infura if wanted for only a tests
            //var web3 = _ethereumClientIntegrationFixture.GetInfuraWeb3(InfuraNetwork.Ropsten);
            var web3 = _ethereumClientIntegrationFixture.GetWeb3();

            var erc20TokenDeployment = new ERC20TokenDeployment() { DecimalUnits = 18, TokenName = "Homeless", TokenSymbol = "Homeless" };

            //Deploy our custom token
            var tokenDeploymentReceipt = await ERC20TokenService.DeployContractAndWaitForReceiptAsync(web3, erc20TokenDeployment);

            //Creating a new service
            var tokenService = new ERC20TokenService(web3, tokenDeploymentReceipt.ContractAddress);

            var receipt = await tokenService.ChangeIssuerRequestAndWaitForReceiptAsync(newIssuer);
            var newIssuerInChain = await tokenService.IssuerQueryAsync();
            Assert.True(newIssuerInChain.IsTheSameAddress(newIssuer));

            //change again using our account
            try
            {
                var receipt2 = await tokenService.ChangeIssuerRequestAndWaitForReceiptAsync(newIssuer);
            }
            catch(Exception ex)
            {
                //different clients have different messages so just catching the exception
                Assert.True(ex.GetType() == typeof(RpcResponseException));
            }
           

        }



        [Fact]
        public async void ShouldTransferAsUsual()
        {

            var donor = EthereumClientIntegrationFixture.AccountAddress; // we are the donors
            //Using ropsten infura if wanted for only a tests
            //var web3 = _ethereumClientIntegrationFixture.GetInfuraWeb3(InfuraNetwork.Ropsten);
            var web3 = _ethereumClientIntegrationFixture.GetWeb3();

            var erc20TokenDeployment = new ERC20TokenDeployment() { DecimalUnits = 18, TokenName = "Homeless", TokenSymbol = "Homeless" };

            //Deploy our custom token
            var tokenDeploymentReceipt = await ERC20TokenService.DeployContractAndWaitForReceiptAsync(web3, erc20TokenDeployment);

            //Creating a new service
            var tokenService = new ERC20TokenService(web3, tokenDeploymentReceipt.ContractAddress);

            var donorIssuance = new DonorIssuance()
            {
                Donor = donor,
                Value = Web3.Convert.ToWei(10),
                TxnHashDonation = "0x30912b7fa14ccf619f8cfbff4ada42aabbccaa0d6373ffdbe0f0b509ac8883d9".HexToByteArray()
            };

            var issueTokensReceipt = await tokenService.IssueTokenToDonorRequestAndWaitForReceiptAsync(new List<DonorIssuance> { donorIssuance });

            //validate the current balance
            var balance = await tokenService.BalanceOfQueryAsync(donor);
            Assert.Equal(10, Web3.Convert.FromWei(balance));


            var transferToken = await tokenService.TransferRequestAndWaitForReceiptAsync("0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BAe", Web3.Convert.ToWei(1));

            balance = await tokenService.BalanceOfQueryAsync(donor);
            Assert.Equal(9, Web3.Convert.FromWei(balance));

        }


        //[Fact]
        //public async void ShouldGetTransferEventLogs()
        //{

        //    var destinationAddress = "0x6C547791C3573c2093d81b919350DB1094707011";
        //    //Using ropsten infura if wanted for only a tests
        //    //var web3 = _ethereumClientIntegrationFixture.GetInfuraWeb3(InfuraNetwork.Ropsten);
        //    var web3 = _ethereumClientIntegrationFixture.GetWeb3();

        //    var erc20TokenDeployment = new ERC20TokenDeployment() { DecimalUnits = 18, TokenName = "TST", TokenSymbol = "TST" };

        //    //Deploy our custom token
        //    var tokenDeploymentReceipt = await ERC20TokenService.DeployContractAndWaitForReceiptAsync(web3, erc20TokenDeployment);

        //    //Creating a new service
        //    var tokenService = new ERC20TokenService(web3, tokenDeploymentReceipt.ContractAddress);

        //    //using Web3.Convert.ToWei as it has 18 decimal places (default)
        //    var transferReceipt1 = await tokenService.TransferRequestAndWaitForReceiptAsync(destinationAddress, Web3.Convert.ToWei(10, 18));
        //    var transferReceipt2 = await tokenService.TransferRequestAndWaitForReceiptAsync(destinationAddress, Web3.Convert.ToWei(10, 18));

        //    var transferEvent = web3.Eth.GetEvent<TransferEventDTO>();
        //    var transferFilter = transferEvent.GetFilterBuilder().AddTopic(x => x.To, destinationAddress).Build(tokenService.ContractHandler.ContractAddress,
        //        new BlockRange(transferReceipt1.BlockNumber, transferReceipt2.BlockNumber));

        //    var transferEvents = await transferEvent.GetAllChanges(transferFilter);

        //    Assert.Equal(2, transferEvents.Count); 

        //}


        //[Fact]
        //public async void ShouldGetTransferEventLogsUsingProcessorAndStoreThem()
        //{

        //    var destinationAddress = "0x6C547791C3573c2093d81b919350DB1094707011";
        //    //Using ropsten infura if wanted for only a tests
        //    //var web3 = _ethereumClientIntegrationFixture.GetInfuraWeb3(InfuraNetwork.Ropsten);
        //    var web3 = _ethereumClientIntegrationFixture.GetWeb3();

        //    var erc20TokenDeployment = new ERC20TokenDeployment() { DecimalUnits = 18, TokenName = "TST", TokenSymbol = "TST" };

        //    //Deploy our custom token
        //    var tokenDeploymentReceipt = await ERC20TokenService.DeployContractAndWaitForReceiptAsync(web3, erc20TokenDeployment);

        //    //Creating a new service
        //    var tokenService = new ERC20TokenService(web3, tokenDeploymentReceipt.ContractAddress);

        //    //using Web3.Convert.ToWei as it has 18 decimal places (default)
        //    var transferReceipt1 = await tokenService.TransferRequestAndWaitForReceiptAsync(destinationAddress, Web3.Convert.ToWei(10, 18));
        //    var transferReceipt2 = await tokenService.TransferRequestAndWaitForReceiptAsync(destinationAddress, Web3.Convert.ToWei(10, 18));


        //    //We are storing in a database the logs
        //    var storedMockedEvents = new List<EventLog<TransferEventDTO>>();
        //    //storage action mock
        //    Task StoreLogAsync(EventLog<TransferEventDTO> eventLog)
        //    {
        //        storedMockedEvents.Add(eventLog);
        //        return Task.CompletedTask;
        //    }

        //    //progress repository to restart processing (simple in memory one, use the other adapters for other storage possibilities)
        //    var blockProgressRepository = new InMemoryBlockchainProgressRepository(transferReceipt1.BlockNumber.Value - 1);

        //    //create our processor to retrieve transfers
        //    //restrict the processor to Transfers for a specific contract address
        //    var processor = web3.Processing.Logs.CreateProcessorForContract<TransferEventDTO>(
        //        tokenService.ContractHandler.ContractAddress, //the contract to monitor
        //        StoreLogAsync, //action to perform when a log is found
        //        minimumBlockConfirmations: 0,  // number of block confirmations to wait
        //        blockProgressRepository: blockProgressRepository //repository to track the progress
        //        );

        //    //if we need to stop the processor mid execution - call cancel on the token
        //    var cancellationToken = new CancellationToken();

        //    //crawl the required block range
        //    await processor.ExecuteAsync(
        //        cancellationToken: cancellationToken,
        //        toBlockNumber: transferReceipt2.BlockNumber.Value,
        //        startAtBlockNumberIfNotProcessed: transferReceipt1.BlockNumber.Value);

        //    Assert.Equal(2, storedMockedEvents.Count);

        //}

    }
}