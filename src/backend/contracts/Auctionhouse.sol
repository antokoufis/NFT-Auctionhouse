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

    // Add item struct
    struct Item {
        uint256 itemId;
        IERC721 nft;
        uint256 tokenId;
        address payable owner;
        uint256 status;
    }

    mapping(uint => Item) public items;

    constructor(uint256 _feePercent) {
        feeAccount = payable(msg.sender);
        feePercent = _feePercent;
    }
}
