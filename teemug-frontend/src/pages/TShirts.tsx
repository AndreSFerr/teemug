import { useGetProductsQuery } from "@/store/api/productsApi";
import { useAppDispatch } from "@/hooks/useAppDispatch";
import { addToCart } from "@/store/slice/cartSlice";

const TShirt = () => {
  const { data: tshirts, isLoading } = useGetProductsQuery({ category: 1 });
  const dispatch = useAppDispatch();

  const handleAddToCart = (product: any) => {
    dispatch(addToCart({
      id: product.id,
      name: product.name,
      price: product.price,
      imageUrl: product.imageUrl,
      quantity: 1
    }));
  };

  return (
    <div className="row row-cols-1 row-cols-md-4 g-4">
      {isLoading ? (
        <p>Carregando...</p>
      ) : (
        tshirts?.map((product: any) => (
          <div className="col" key={product.id}>
            <div className="card h-100">
              <img
                src={product.imageUrl}
                className="card-img-top"
                alt={product.name}
                style={{ objectFit: 'cover', height: '200px' }}
              />
              <div className="card-body">
                <h5 className="card-title">{product.name}</h5>
                <p className="card-text">{product.description}</p>
                <p><strong>€ {product.price}</strong></p>
                <button
                  className="btn btn-primary w-100"
                  onClick={() => handleAddToCart(product)}
                >
                  Add to Cart
                </button>
              </div>
            </div>
          </div>
        ))
      )}
    </div>
  );
};

export default TShirt;
