import { createBrowserRouter } from "react-router";
import App from "../layouts/App";
import ActivityForm from "../../features/activities/form/ActivityForm";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import HomePage from "../../features/activities/HomePage";
import ActivityDetail from "../../features/activities/dashboard/details/ActivityDetail";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            {path: '', element: <HomePage />},
            {path: 'activities', element: <ActivityDashboard />},
            {path: 'activities/:id', element: <ActivityDetail />},
            {path: 'createActivity', element: <ActivityForm  key='create'/>},
            {path: 'manage/:id', element: <ActivityForm />},
        ]
    }
])