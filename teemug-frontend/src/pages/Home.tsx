import Sidebar from "@/components/layout/Sidebar";
import Navbar from "@/components/layout/Navbar";
import ProductGrid from "@/components/products/ProductGrid";

const Home = () => {
  return (
    <div className="d-flex">
      <div className="d-none d-md-block">
        <Sidebar />
      </div>
      <div className="flex-grow-1">
        <Navbar />
        <div className="container mt-3">
          <ProductGrid />
        </div>
      </div>
    </div>
  );
};

export default Home;
