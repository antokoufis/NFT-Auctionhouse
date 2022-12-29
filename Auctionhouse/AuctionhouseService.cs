using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using NFTAuctionhouse.Contracts.Auctionhouse.ContractDefinition;

namespace NFTAuctionhouse.Contracts.Auctionhouse
{
    public partial class AuctionhouseService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, AuctionhouseDeployment auctionhouseDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<AuctionhouseDeployment>().SendRequestAndWaitForReceiptAsync(auctionhouseDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, AuctionhouseDeployment auctionhouseDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<AuctionhouseDeployment>().SendRequestAsync(auctionhouseDeployment);
        }

        public static async Task<AuctionhouseService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, AuctionhouseDeployment auctionhouseDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, auctionhouseDeployment, cancellationTokenSource);
            return new AuctionhouseService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public AuctionhouseService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> FeeAccountQueryAsync(FeeAccountFunction feeAccountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FeeAccountFunction, string>(feeAccountFunction, blockParameter);
        }

        
        public Task<string> FeeAccountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FeeAccountFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> FeePercentQueryAsync(FeePercentFunction feePercentFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FeePercentFunction, BigInteger>(feePercentFunction, blockParameter);
        }

        
        public Task<BigInteger> FeePercentQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<FeePercentFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> ItemCountQueryAsync(ItemCountFunction itemCountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ItemCountFunction, BigInteger>(itemCountFunction, blockParameter);
        }

        
        public Task<BigInteger> ItemCountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ItemCountFunction, BigInteger>(null, blockParameter);
        }

        public Task<ItemsOutputDTO> ItemsQueryAsync(ItemsFunction itemsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ItemsFunction, ItemsOutputDTO>(itemsFunction, blockParameter);
        }

        public Task<ItemsOutputDTO> ItemsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var itemsFunction = new ItemsFunction();
                itemsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<ItemsFunction, ItemsOutputDTO>(itemsFunction, blockParameter);
        }

        public Task<string> MakeItemRequestAsync(MakeItemFunction makeItemFunction)
        {
             return ContractHandler.SendRequestAsync(makeItemFunction);
        }

        public Task<TransactionReceipt> MakeItemRequestAndWaitForReceiptAsync(MakeItemFunction makeItemFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(makeItemFunction, cancellationToken);
        }

        public Task<string> MakeItemRequestAsync(string nft, BigInteger tokenId)
        {
            var makeItemFunction = new MakeItemFunction();
                makeItemFunction.Nft = nft;
                makeItemFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAsync(makeItemFunction);
        }

        public Task<TransactionReceipt> MakeItemRequestAndWaitForReceiptAsync(string nft, BigInteger tokenId, CancellationTokenSource cancellationToken = null)
        {
            var makeItemFunction = new MakeItemFunction();
                makeItemFunction.Nft = nft;
                makeItemFunction.TokenId = tokenId;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(makeItemFunction, cancellationToken);
        }
    }
}
