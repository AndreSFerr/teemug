import { useEffect } from 'react';
import { useAppDispatch } from '@/hooks/useAppDispatch';
import { logout } from '@/store/slice/authSlice';
import { useNavigate } from 'react-router-dom';

const Logout = () => {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  useEffect(() => {
    dispatch(logout());
    navigate('/');
  }, [dispatch, navigate]);

  return null; 
};

export default Logout;
