// SPDX-License-Identifier: MIT

pragma solidity ^0.8.4;

import "@openzeppelin/contracts/token/ERC721/IERC721.sol";
import "@openzeppelin/contracts/security/ReentrancyGuard.sol";

contract Auctionhouse is ReentrancyGuard {
    // the account that receives fees
    address payable public immutable feeAccount;
    // the fee percentage on sales
    uint256 public immutable feePercent;
    uint256 public itemCount;
    uint256 public auctionCount;

    // Add structs
    struct Item {
        uint256 itemId;
        IERC721 nft;
        uint256 tokenId;
        address payable owner;
        uint256 status;
    }

    struct Auction {
        uint256 auctionId;
        uint256 itemId;
        address payable auctioneer;
        uint256 startingPrice;
        uint256 endDateTime;
        uint256 status;
    }

    struct Bid {
        uint256 bidId;
        uint256 auctionId;
        uint256 bidPrice;
        uint256 timestamp;
        address payable bidder;
        uint256 status;
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

        emit ListedItems(itemCount, address(_nft), _tokenId, msg.sender);
    }

    function makeAuction(
        uint256 _itemId,
        uint256 _startingPrice,
        uint256 _endDateTime
    ) external nonReentrant {
        auctionCount++;
        Item storage item = items[_itemId];

        auctions[auctionCount] = Auction(
            auctionCount,
            _itemId,
            payable(msg.sender),
            _startingPrice,
            _endDateTime,
            1
        );

        item.status = 2;
        item.nft.transferFrom(msg.sender, address(this), item.tokenId);

        emit ListedAuctions(
            auctionCount,
            _itemId,
            msg.sender,
            _startingPrice,
            _endDateTime,
            1
        );
    }
}
