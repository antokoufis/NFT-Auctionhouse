// SPDX-License-Identifier: MIT

pragma solidity ^0.8.4;

import "@openzeppelin/contracts/token/ERC721/IERC721.sol";
import "@openzeppelin/contracts/security/ReentrancyGuard.sol";
import "hardhat/console.sol";


contract Auctionhouse is ReentrancyGuard {
    // the account that receives fees
    address payable public immutable feeAccount;
    // the fee percentage on sales
    uint256 public immutable feePercent;

    uint256 public itemCount;
    uint256 public auctionCount;
    uint256 public bidCount;

    // Add structs
    struct Item {
        uint256 itemId;
        IERC721 nft;
        uint256 tokenId;
        address payable owner;
        uint256 status; //1 = Available for Auction Item, 2 =  NOT available for Auction Item
    }

    struct Auction {
        uint256 auctionId;
        uint256 itemId;
        address payable auctioneer;
        uint256 startingPrice;
        uint256 endDateTime;
        uint256 winningBid;
        uint256 status; //1 = Available for bidding, 2 = Not Available for bidding
    }

    struct Bid {
        uint256 bidId;
        uint256 auctionId;
        uint256 bidPrice;
        uint256 timestamp;
        address payable bidder;
        uint256 status; //1 = Active, 2 = Returned
    }

    mapping(uint256 => Item) public items;
    mapping(uint256 => Auction) public auctions;
    mapping(uint256 => Bid) public bids;

    event ListedItems(
        uint256 itemId,
        address indexed nft,
        uint256 tokenId,
        address indexed owner
    );

    event ListedAuctions(
        uint256 indexed auctionId,
        uint256 indexed itemId,
        address indexed auctioneer,
        uint256 startingPrice,
        uint256 endDateTime,
        uint256 winningBid,
        uint256 status
    );

    event ListedBid(
        uint256 indexed bidId,
        uint256 indexed auctionId,
        uint256 bidPrice,
        uint256 timestamp,
        address payable bidder,
        uint256 status
    );

    constructor(uint256 _feePercent) {
        feeAccount = payable(msg.sender);
        feePercent = _feePercent;
    }

    function makeItem(IERC721 _nft, uint256 _tokenId) external nonReentrant {
        itemCount++;
        items[itemCount] = Item(
            itemCount,
            _nft,
            _tokenId,
            payable(msg.sender),
            1
        );

        // // IERC721 _nft = item.nft;
        // //_nft.transferFrom(_nft.ownerOf(item.tokenId), address(this), item.tokenId);
        // _nft.transferFrom(_nft.ownerOf(_tokenId), msg.sender, _tokenId);
        emit ListedItems(itemCount, address(_nft), _tokenId, msg.sender);
    }

    function makeAuction(uint256 _itemId, uint256 _startingPrice, uint256 _endDateTime ) external nonReentrant {
        auctionCount++;
        Item storage item = items[_itemId];
        require(item.status == 1);
        require(item.owner == msg.sender);
        require(_startingPrice>0);
        auctions[auctionCount] = Auction(
            auctionCount,
            _itemId,
            payable(msg.sender),
            _startingPrice,
            _endDateTime,
            0,
            1
        );

        item.status = 2;
        IERC721 _nft = item.nft;
        _nft.transferFrom(msg.sender, address(this), item.tokenId);


        emit ListedAuctions(
            auctionCount,
            _itemId,
            msg.sender,
            _startingPrice,
            _endDateTime,
            0,
            1
        );
    }

    function makeBid(uint256 _auctionId, uint256 _bidPrice) external payable nonReentrant {
        
        Auction storage auction = auctions[_auctionId];
        
        require(auction.endDateTime > block.timestamp);
        require(auction.status == 1);

        if (auction.winningBid == 0) {
            require(auction.startingPrice < _bidPrice);
            bidCount++;

            bids[bidCount] = Bid(
                bidCount,
                _auctionId,
                _bidPrice,
                block.timestamp,
                payable(msg.sender),
                1
            );
            
            payable(address(this)).transfer(msg.value);
            auction.winningBid = bidCount;
        } else {
            Bid storage bid = bids[auction.winningBid];
            require(bid.bidPrice < _bidPrice);
            bidCount++;

            bids[bidCount] = Bid(
                bidCount,
                _auctionId,
                _bidPrice,
                block.timestamp,
                payable(msg.sender),
                1
            );

            bid.bidder.transfer(bid.bidPrice);
            bid.status = 2;
            payable(address(this)).transfer(msg.value);
            auction.winningBid = bidCount;
        }

        emit ListedBid(
            bidCount,
            _auctionId,
            _bidPrice,
            block.timestamp,
            payable(msg.sender),
            1
        );
    }

    receive() external payable {
    }
    
}
