import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import type { LoginSchema } from '../schemas/loginSchema';
import agent from '../api/agent';
import { useNavigate } from 'react-router';
import type { RegisterSchema } from '../schemas/registerSchema';
import { toast } from 'react-toastify';
import type { ChangePasswordSchema } from '../schemas/changePasswordSchema';

export const useAccount = () => {
  const queryClient = useQueryClient();
  const navigate = useNavigate();

  const { data: currentUser, isLoading: loadingUserInfo } = useQuery({
    queryKey: ['user'],
    queryFn: async () => {
      const response = await agent.get<User>('/account/user-info');
      return response.data;
    },
    enabled: !queryClient.getQueryData(['user']),
  });

  const loginUser = useMutation({
    mutationFn: async (creds: LoginSchema) => {
      await agent.post('/login?useCookies=true', creds);
    },
    onSuccess: async () => {
      await queryClient.invalidateQueries({
        queryKey: ['user'],
      });
    },
  });

  const registerUser = useMutation({
    mutationFn: async (creds: RegisterSchema) => {
      await agent.post('/account/register', creds);
    },
    onSuccess: () => {
      toast.success('register successful');
      navigate('/login');
    },
  });

  const logOutUser = useMutation({
    mutationFn: async () => {
      await agent.post('/account/logout');
    },
    onSuccess: () => {
      queryClient.removeQueries({ queryKey: ['user'] });
      queryClient.removeQueries({ queryKey: ['activities'] });
      navigate('/');
    },
  });

  const changePassword = useMutation({
    mutationFn: async (data: ChangePasswordSchema) => {
        await agent.post('/account/change-password', data);
    }
  })

  return {
    loginUser,
    currentUser,
    logOutUser,
    loadingUserInfo,
    registerUser,
    changePassword
  };
};
