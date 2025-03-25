// src/pages/Cart.tsx
import { useAppDispatch } from '@/hooks/useAppDispatch';
import { useAppSelector } from '@/hooks/useAppSelector';
import { removeFromCart, updateQuantity } from '@/store/slice/cartSlice';
import { useNavigate } from 'react-router-dom';

const Cart = () => {
  const { items } = useAppSelector((state) => state.cart);
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleRemove = (productId: string) => {
    dispatch(removeFromCart(productId));
  };

  const handleQuantityChange = (productId: string, quantity: number) => {
    dispatch(updateQuantity({ productId, quantity }));
  };

  const total = items.reduce((acc, item) => acc + item.price * item.quantity, 0);

  return (
    <div className="container my-5">
      <h2 className="mb-4">🛒 Carrinho de Compras</h2>
      {items.length === 0 ? (
        <p>Seu carrinho está vazio.</p>
      ) : (
        <>
          <table className="table">
            <thead>
              <tr>
                <th>Produto</th>
                <th>Preço</th>
                <th>Quantidade</th>
                <th>Subtotal</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              {items.map((item) => (
                <tr key={item.productId}>
                  <td>
                    <div className="d-flex align-items-center">
                      <img
                        src={item.imageUrl}
                        alt={item.name}
                        style={{ width: '60px', height: '60px', objectFit: 'cover', marginRight: '10px' }}
                      />
                      {item.name}
                    </div>
                  </td>
                  <td>€ {item.price.toFixed(2)}</td>
                  <td>
                    <input
                      type="number"
                      value={item.quantity}
                      onChange={(e) => handleQuantityChange(item.productId, parseInt(e.target.value))}
                      min={1}
                      className="form-control"
                      style={{ width: '80px' }}
                    />
                  </td>
                  <td>€ {(item.price * item.quantity).toFixed(2)}</td>
                  <td>
                    <button className="btn btn-danger btn-sm" onClick={() => handleRemove(item.productId)}>
                      Remover
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>

          <h4 className="text-end">Total: € {total.toFixed(2)}</h4>

          <div className="text-end mt-4">
            <button className="btn btn-success" onClick={() => navigate('/checkout')}>
              Finalizar Pedido
            </button>
          </div>
        </>
      )}
    </div>
  );
};

export default Cart;
