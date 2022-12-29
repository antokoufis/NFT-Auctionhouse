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

namespace NFTAuctionhouse.Contracts.Auctionhouse.ContractDefinition
{


    public partial class AuctionhouseDeployment : AuctionhouseDeploymentBase
    {
        public AuctionhouseDeployment() : base(BYTECODE) { }
        public AuctionhouseDeployment(string byteCode) : base(byteCode) { }
    }

    public class AuctionhouseDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60c060405234801561001057600080fd5b506040516102d23803806102d283398101604081905261002f91610040565b60016000553360805260a052610059565b60006020828403121561005257600080fd5b5051919050565b60805160a05161025661007c600039600060d101526000607601526102566000f3fe608060405234801561001057600080fd5b50600436106100575760003560e01c80631a1beae21461005c57806365e17c9d146100715780636bfb0d01146100b55780637fd6f15c146100cc578063bfb231d2146100f3575b600080fd5b61006f61006a3660046101cf565b610170565b005b6100987f000000000000000000000000000000000000000000000000000000000000000081565b6040516001600160a01b0390911681526020015b60405180910390f35b6100be60015481565b6040519081526020016100ac565b6100be7f000000000000000000000000000000000000000000000000000000000000000081565b61013e610101366004610207565b60026020819052600091825260409091208054600182015492820154600383015460049093015491936001600160a01b0390811693919291169085565b604080519586526001600160a01b0394851660208701528501929092529091166060830152608082015260a0016100ac565b6002600054036101c65760405162461bcd60e51b815260206004820152601f60248201527f5265656e7472616e637947756172643a207265656e7472616e742063616c6c00604482015260640160405180910390fd5b50506001600055565b600080604083850312156101e257600080fd5b82356001600160a01b03811681146101f957600080fd5b946020939093013593505050565b60006020828403121561021957600080fd5b503591905056fea264697066735822122046747036f939271bf908f5bbe1b4f00417d2250b423ad3d38a930dd1e44d68b964736f6c63430008110033";
        public AuctionhouseDeploymentBase() : base(BYTECODE) { }
        public AuctionhouseDeploymentBase(string byteCode) : base(byteCode) { }
        [Parameter("uint256", "_feePercent", 1)]
        public virtual BigInteger FeePercent { get; set; }
    }

    public partial class FeeAccountFunction : FeeAccountFunctionBase { }

    [Function("feeAccount", "address")]
    public class FeeAccountFunctionBase : FunctionMessage
    {

    }

    public partial class FeePercentFunction : FeePercentFunctionBase { }

    [Function("feePercent", "uint256")]
    public class FeePercentFunctionBase : FunctionMessage
    {

    }

    public partial class ItemCountFunction : ItemCountFunctionBase { }

    [Function("itemCount", "uint256")]
    public class ItemCountFunctionBase : FunctionMessage
    {

    }

    public partial class ItemsFunction : ItemsFunctionBase { }

    [Function("items", typeof(ItemsOutputDTO))]
    public class ItemsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class MakeItemFunction : MakeItemFunctionBase { }

    [Function("makeItem")]
    public class MakeItemFunctionBase : FunctionMessage
    {
        [Parameter("address", "_nft", 1)]
        public virtual string Nft { get; set; }
        [Parameter("uint256", "_tokenId", 2)]
        public virtual BigInteger TokenId { get; set; }
    }

    public partial class FeeAccountOutputDTO : FeeAccountOutputDTOBase { }

    [FunctionOutput]
    public class FeeAccountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class FeePercentOutputDTO : FeePercentOutputDTOBase { }

    [FunctionOutput]
    public class FeePercentOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class ItemCountOutputDTO : ItemCountOutputDTOBase { }

    [FunctionOutput]
    public class ItemCountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class ItemsOutputDTO : ItemsOutputDTOBase { }

    [FunctionOutput]
    public class ItemsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "itemId", 1)]
        public virtual BigInteger ItemId { get; set; }
        [Parameter("address", "nft", 2)]
        public virtual string Nft { get; set; }
        [Parameter("uint256", "tokenId", 3)]
        public virtual BigInteger TokenId { get; set; }
        [Parameter("address", "owner", 4)]
        public virtual string Owner { get; set; }
        [Parameter("uint256", "status", 5)]
        public virtual BigInteger Status { get; set; }
    }


}
