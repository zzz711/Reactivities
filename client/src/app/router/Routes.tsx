import { createBrowserRouter, Navigate } from 'react-router';
import App from '../layouts/App';
import ActivityForm from '../../features/activities/form/ActivityForm';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import HomePage from '../../features/activities/HomePage';
import ActivityDetail from '../../features/activities/dashboard/details/ActivityDetail';
import Counter from '../../features/counter/Counter';
import TestErrors from '../../features/errors/TestErrors';
import NotFound from '../../features/errors/NotFound';
import ServerError from '../../features/errors/ServerError';
import LoginForm from '../../features/account/LoginForm';
import RequireAuth from './RequireAuth';
import RegisterForm from '../../features/account/RegisterForm';
import ProfilePage from '../../features/profiles/ProfilePage';
import ChangePasswordForm from '../../features/account/ChangePasswordForm';

export const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    children: [
      {
        element: <RequireAuth />,
        children: [
          { path: 'activities', element: <ActivityDashboard /> },
          { path: 'activities/:id', element: <ActivityDetail /> },
          { path: 'createActivity', element: <ActivityForm key="create" /> },
          { path: 'profiles/:id', element: <ProfilePage /> },
          { path: 'change-password', element: <ChangePasswordForm /> },
        ],
      },
      { path: '', element: <HomePage /> },
      { path: 'counter', element: <Counter /> },
      { path: 'errors', element: <TestErrors /> },
      { path: 'not-found', element: <NotFound /> },
      { path: 'server-error', element: <ServerError /> },
      { path: 'login', element: <LoginForm /> },
      { path: 'register', element: <RegisterForm /> },
      { path: '*', element: <Navigate replace to="/not-found" /> },
    ],
  },
]);
