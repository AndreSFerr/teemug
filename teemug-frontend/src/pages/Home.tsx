import { useGetProductsQuery } from "@/store/api/productsApi";
import ProductCarousel from "@/components/products/ProductCarousel";

const Home = () => {
  const { data: tshirts, isLoading: loadingTShirts } = useGetProductsQuery({ category: 1 });
  const { data: mugs, isLoading: loadingMugs } = useGetProductsQuery({ category: 2 });

  const meiosPagamento = ["Visa", "Mastercard", "PayPal", "MBWay"].map((nome) => ({
    name: nome,
    image: `https://placehold.co/150x100?text=${encodeURIComponent(nome)}`,
  }));

  const formatProductsForCarousel = (products: any[] | undefined) => {
    if (!products) return [];
    return products.map((p) => ({
      name: p.name,
      description: p.description,
      price: `€ ${p.price.toFixed(2)}`,
      image: p.imageUrl,
    }));
  };

  return (
    <div className="d-flex flex-column min-vh-100">
      <ProductCarousel
        title="Camisetas"
        products={formatProductsForCarousel(loadingTShirts ? [] : tshirts)}
        carouselId="carouselCamisetas"
      />

      <ProductCarousel
        title="Canecas"
        products={formatProductsForCarousel(loadingMugs ? [] : mugs)}
        carouselId="carouselCanecas"
      />

      <section className="container py-5">
        <h2 className="text-center mb-4">Meios de Pagamento</h2>
        <div className="row justify-content-center text-center g-4">
          {meiosPagamento.map((pagamento, index) => (
            <div className="col-6 col-md-3" key={index}>
              <img src={pagamento.image} alt={pagamento.name} className="img-fluid" />
            </div>
          ))}
        </div>
      </section>
    </div>
  );
};

export default Home;
