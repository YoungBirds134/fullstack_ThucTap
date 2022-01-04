import React from 'react'
const Login = React.lazy(() => import('./views/pages/login/Login'))

const Dashboard = React.lazy(() => import('./views/dashboard/Dashboard'))

const routes = [
  { path: '/', exact: true, name: 'Login' , component: Login },
  { path: '/home', name: 'Dashboard', component: Dashboard },
]

export default routes
