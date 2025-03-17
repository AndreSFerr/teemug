const TShirt = () => {
  const products = Array.from({ length: 12 }).map((_, index) => ({
    id: index,
    name: `Product ${index + 1}`,
    description: `Description for product ${index + 1}`,
    price: (10 + index).toFixed(2),
    image: `https://picsum.photos/300/200?random=${index}&t=${Date.now()}`,
  }));

  return (
    <div className="row row-cols-1 row-cols-md-4 g-4">
      {products.map((product) => (
        <div className="col" key={product.id}>
          <div className="card h-100">
            <img
              src={product.image}
              className="card-img-top"
              alt={product.name}
              style={{ objectFit: 'cover', height: '200px' }}
            />
            <div className="card-body">
              <h5 className="card-title">{product.name}</h5>
              <p className="card-text">{product.description}</p>
              <p><strong>€ {product.price}</strong></p>
              <button className="btn btn-primary w-100">Add to Cart</button>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
};

export default TShirt;
