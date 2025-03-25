import { Link } from "react-router-dom";

const OrderConfirmation = () => {
  return (
    <div className="container mt-5 text-center" style={{ maxWidth: "600px" }}>
      <div className="card shadow-lg border-0">
        <div className="card-body p-5">
          <h2 className="mb-4 text-success">✅ Pedido Confirmado!</h2>
          <p className="fs-5">
            Obrigado pela sua compra. Seu pedido foi recebido com sucesso e será processado em breve.
          </p>

          <div className="my-4">
            <img
              src="https://cdn-icons-png.flaticon.com/512/148/148767.png"
              alt="Pedido Confirmado"
              style={{ width: "100px" }}
            />
          </div>

          <Link to="/" className="btn btn-primary mt-3">
            Voltar à Página Inicial
          </Link>
        </div>
      </div>
    </div>
  );
};

export default OrderConfirmation;
