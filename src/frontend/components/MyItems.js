import { useState, useEffect } from 'react'
import { Row, Col, Card, Button } from 'react-bootstrap'

const MyItems = ({ auctionhouse, nft }) => {
  const [loading, setLoading] = useState(true)
  const [items, setItems] = useState([])
  const loadAuctionhouseItems = async () => {
    const itemCount = await auctionhouse.itemCount()
    let items = []
    for (let i = 1; i <= itemCount; i++) {
      const item = await auctionhouse.items(i)
        // get uri url from nft contract
        const uri = await nft.tokenURI(item.tokenId)

        // use fixedUri to fetch the nft metadata stored on ipfs 
        const fixedUri = "https://" + uri
        const response = await fetch(fixedUri)

        const metadata = await response.json()
        // Add item to items array
        items.push({
          itemId: item.itemId,
          name: metadata.name,
          description: metadata.description,
          image: metadata.image
        })
      
    }
    setLoading(false)
    setItems(items)
  }

  useEffect(() => {
    loadAuctionhouseItems()
  }, [])
  if (loading) return (
    <main style={{ padding: "1rem 0" }}>
      <h2>Loading...</h2>
    </main>
  )
  return (
    <div className="flex justify-center">
      {items.length > 0 ?
        <div className="px-5 container">
          <Row xs={1} md={2} lg={4} className="g-4 py-5">
            {items.map((item, idx) => (
              <Col key={idx} className="overflow-hidden">
                <Card>
                  <Card.Img variant="top" src={"https://" + item.image} />
                  <Card.Body color="secondary">
                    <Card.Title>{item.name}</Card.Title>
                  </Card.Body>
                </Card>
              </Col>
            ))}
          </Row>
        </div>
        : (
          <main style={{ padding: "1rem 0" }}>
            <h2>No listed assets</h2>
          </main>
        )}
    </div>
  );
}
export default MyItems