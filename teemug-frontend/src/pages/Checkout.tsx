import { useAppSelector } from "@/hooks/useAppSelector";
import { useAppDispatch } from "@/hooks/useAppDispatch";
import { clearCart } from "@/store/slice/cartSlice";
import { useCreateOrderMutation } from "@/store/api/ordersApi";
import { useNavigate } from "react-router-dom";
import { useState } from "react";

const Checkout = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const cartItems = useAppSelector((state) => state.cart.items);
  const totalAmount = cartItems.reduce((acc, item) => acc + item.price * item.quantity, 0);

  const [createOrder, { isLoading, isSuccess, isError }] = useCreateOrderMutation();
  const [cardNumber, setCardNumber] = useState("");
  const [cardHolder, setCardHolder] = useState("");
  const [expiration, setExpiration] = useState("");
  const [cvv, setCvv] = useState("");

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const orderPayload = {
      items: cartItems.map((item) => ({
        productId: item.id,
        quantity: item.quantity,
        unitPrice: item.price,
      })),
      totalAmount,
    };

    try {
      await createOrder(orderPayload).unwrap();
      dispatch(clearCart());
      navigate("/order-confirmation");
    } catch (err) {
      console.error("Erro ao criar pedido:", err);
    }
  };

  return (
    <div className="container mt-5" style={{ maxWidth: "600px" }}>
      <h2 className="text-center mb-4">Finalizar Pagamento</h2>

      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label className="form-label">Número do Cartão</label>
          <input
            type="text"
            className="form-control"
            value={cardNumber}
            onChange={(e) => setCardNumber(e.target.value)}
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">Nome do Titular</label>
          <input
            type="text"
            className="form-control"
            value={cardHolder}
            onChange={(e) => setCardHolder(e.target.value)}
            required
          />
        </div>

        <div className="row">
          <div className="col-md-6 mb-3">
            <label className="form-label">Validade</label>
            <input
              type="text"
              className="form-control"
              placeholder="MM/AA"
              value={expiration}
              onChange={(e) => setExpiration(e.target.value)}
              required
            />
          </div>

          <div className="col-md-6 mb-3">
            <label className="form-label">CVV</label>
            <input
              type="text"
              className="form-control"
              value={cvv}
              onChange={(e) => setCvv(e.target.value)}
              required
            />
          </div>
        </div>

        <div className="d-flex justify-content-between align-items-center mt-4">
          <h5>Total: <strong>€ {totalAmount.toFixed(2)}</strong></h5>
          <button type="submit" className="btn btn-success" disabled={isLoading}>
            {isLoading ? "Processando..." : "Confirmar Pagamento"}
          </button>
        </div>

        {isError && (
          <div className="alert alert-danger mt-3">
            Ocorreu um erro ao processar o pagamento.
          </div>
        )}
      </form>
    </div>
  );
};

export default Checkout;
