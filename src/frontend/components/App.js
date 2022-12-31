import logo from './logo.png';
import './App.css';

import AuctionhouseAbi from '../contractsData/Auctionhouse.json'
import AuctionhouseAddress from '../contractsData/Auctionhouse-address.json'
import NFTAbi from '../contractsData/NFT.json'
import NFTAddress from '../contractsData/NFT-address.json'

import { ethers } from "ethers"
import { useState } from 'react'

function App() {
  const [loading, setLoading] = useState(true)
  const [account, setAccount] = useState(null)
  const [nft, setNFT] = useState({})
  const [auctionhouse, setAuctionhouse] = useState({})
  //Handles the connection between app and Metamask
  const web3Handler = async () => {
    const accounts = await window.ethereum.request({ method: 'eth_requestAccounts' });
    setAccount(accounts[0])
    // Get provider from Metamask
    const provider = new ethers.providers.Web3Provider(window.ethereum)
    // Set signer
    const signer = provider.getSigner()

    loadContracts(signer)
  }

  const loadContracts = async (signer) => {
    // Get deployed copies of contracts
    const auctionhouse = new ethers.Contract(AuctionhouseAddress.address, AuctionhouseAbi.abi, signer)
    setAuctionhouse(auctionhouse)
    const nft = new ethers.Contract(NFTAddress.address, NFTAbi.abi, signer)
    setNFT(nft)
    setLoading(false)
}

  return (
    <div>
      <nav className="navbar navbar-dark fixed-top bg-dark flex-md-nowrap p-0 shadow">
      </nav>
      <div className="container-fluid mt-5">
        <div className="row">
          <main role="main" className="col-lg-12 d-flex text-center">
            <div className="content mx-auto mt-5">
              <img src={logo} className="App-logo" alt="logo" />
              <h1 className="mt-5">Auctionhouse</h1>
              <p>
                Edit <code>src/frontend/components/App.js</code> and save to reload.
              </p>
            </div>
          </main>
        </div>
      </div>
    </div>
  );
}

export default App;
