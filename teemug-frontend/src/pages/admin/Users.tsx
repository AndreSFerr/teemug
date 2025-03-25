import { useGetUsersQuery, useUpdateUserRoleMutation, useBlockUserMutation } from "@/store/api/adminApi";

const Users = () => {
  const { data: users, refetch } = useGetUsersQuery();
  const [updateUserRole] = useUpdateUserRoleMutation();
  const [blockUser] = useBlockUserMutation();

  const handleRoleChange = async (userId: string, newRole: string) => {
    await updateUserRole({ userId, newRole });
    refetch();
  };

  const handleBlockUser = async (userId: string) => {
    await blockUser(userId);
    refetch();
  };

  return (
    <div className="container py-4">
      <h2>Lista de Utilizadores</h2>
      <table className="table">
        <thead>
          <tr>
            <th>Nome</th>
            <th>Email</th>
            <th>Role</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {users?.map((user) => (
            <tr key={user.id}>
              <td>{user.name}</td>
              <td>{user.email}</td>
              <td>{user.role}</td>
              <td>
                <select value={user.role} onChange={(e) => handleRoleChange(user.id, e.target.value)}>
                  <option value="User">User</option>
                  <option value="Admin">Admin</option>
                </select>
                <button className="btn btn-sm btn-danger ms-2" onClick={() => handleBlockUser(user.id)}>Bloquear</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};
export default Users;