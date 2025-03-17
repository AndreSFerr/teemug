import ProductCarousel from "@/components/products/ProductCarousel";

const Home = () => {

  const camisetas = Array.from({ length: 12 }).map((_, index) => ({
    name: `Camiseta ${index + 1}`,
    description: `Descrição para camiseta ${index + 1}`,
    price: `${(19.90 + index * 1.5).toFixed(2)} €`,
    image: `https://picsum.photos/seed/camiseta${index}/300/200`,
  }));

  const canecas = Array.from({ length: 12 }).map((_, index) => ({
    name: `Caneca ${index + 1}`,
    description: `Descrição para caneca ${index + 1}`,
    price: `${(9.90 + index * 1.2).toFixed(2)} €`,
    image: `https://picsum.photos/seed/caneca${index}/300/200`,
  }));

  const meiosPagamento = ["Visa", "Mastercard", "PayPal", "MBWay"].map((nome) => ({
    name: nome,
    image: `https://placehold.co/150x100?text=${encodeURIComponent(nome)}`,
  }));

  return (
    <div className="d-flex flex-column min-vh-100">
      <ProductCarousel title="Camisetas" products={camisetas} carouselId="carouselCamisetas" />
      <ProductCarousel title="Canecas" products={canecas} carouselId="carouselCanecas" />
    
      <section className="container py-5">
        <h2 className="text-center mb-4">Meios de Pagamento</h2>
        <div className="row justify-content-center text-center g-4">
          {meiosPagamento.map((pagamento, index) => (
            <div className="col-6 col-md-3" key={index}>
              <img
                src={pagamento.image}
                alt={pagamento.name}
                className="img-fluid"
              />
            </div>
          ))}
        </div>
      </section>  
    </div>
  );
};

export default Home;
