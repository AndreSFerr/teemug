type Product = {
  name: string;
  description: string;
  price: string;
  image: string;
};

type Props = {
  title: string;
  products: Product[];
  carouselId: string;
};

const ProductCarousel = ({ title, products, carouselId }: Props) => {
 
  const groupProducts = products.reduce<Product[][]>((acc, curr, index) => {
    const groupIndex = Math.floor(index / 3);
    if (!acc[groupIndex]) acc[groupIndex] = [];
    acc[groupIndex].push(curr);
    return acc;
  }, []);

  return (
    <section className="container my-5">
      <h2 className="text-center mb-4">{title}</h2>
      <div id={carouselId} className="carousel slide" data-bs-ride="carousel">
        <div className="carousel-inner">
          {groupProducts.map((group, i) => (
            <div className={`carousel-item ${i === 0 ? 'active' : ''}`} key={i}>
              <div className="row">
                {group.map((product, j) => (
                  <div className="col-md-4" key={j}>
                    <div className="card h-100 shadow-sm border-0 mx-1">
                      <img
                        src={product.image}
                        className="card-img-top"
                        alt={product.name}
                        style={{ objectFit: 'cover', height: '250px' }}
                      />
                      <div className="card-body">
                        <h5 className="card-title">{product.name}</h5>
                        <p className="card-text text-muted">{product.description}</p>
                      </div>
                      <div className="card-footer bg-white border-0 d-flex justify-content-between">
                        <span className="fw-bold">{product.price}</span>
                        <button className="btn btn-sm btn-outline-primary">Add to Cart</button>
                      </div>
                    </div>
                  </div>
                ))}
              </div>
            </div>
          ))}
        </div>

       
        <button className="carousel-control-prev" type="button" data-bs-target={`#${carouselId}`} data-bs-slide="prev">
          <span className="carousel-control-prev-icon"></span>
        </button>
        <button className="carousel-control-next" type="button" data-bs-target={`#${carouselId}`} data-bs-slide="next">
          <span className="carousel-control-next-icon"></span>
        </button>
      </div>
    </section>
  );
};

export default ProductCarousel;
