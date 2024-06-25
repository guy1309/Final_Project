import React from 'react';
import {  BrowserRouter as  Router, Routes, Route } from 'react-router-dom'
import Login from'./Login';
import Registration from'./Registration';
import Dashboard from './users/Dashboard';
import Orders from './users/Orders';
import Profile from './users/Profile';
import Cart from './users/Cart';
import MedicineDisplay from'./users/MedicineDisplay';
import AdminDashboard from './admin/AdminDashboard';
import AdminOrders from './admin/AdminOrders';
import CustomerList from './admin/CustomerList';
import Medicine from './admin/Medicine';
import Receipt from './users/Receipt';
import SideMenu from './users/SideMenu';
import Footer from './users/Footer';


export default function RouterPage()
{
    return(
        <Router>
            <Routes>
                <Route path='/' element={ <Login /> } />
                <Route path='/Registration' element={ <Registration /> } />
                <Route path='/dashboard' element={ <Dashboard /> } />
                <Route path='/myorders' element={ <Orders /> } />
                <Route path='/profile' element={ <Profile /> } />
                <Route path='/cart' element={ <Cart /> } />              
           
                <Route path='/admindashboard' element={ <AdminDashboard /> } />
                <Route path='/adminorders' element={ <AdminOrders /> } />
                <Route path='/customers' element={ <CustomerList /> } />
                <Route path='/medicine' element={ <Medicine /> } />

                
                <Route path='/products' element={ <MedicineDisplay /> } />

                <Route path='/receipt/:id' element={ <Receipt /> } />
                <Route path='/sidemenu' element={ <SideMenu /> } />

            </Routes>
            <Footer />
        </Router>
    )    
}