import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import ManageProduct from "./pages/ManageProduct.jsx";  
import ProductList from "./pages/ProductList.jsx";  
import Dashboard from "./pages/Sidebar.jsx";
import "./App.css";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<ProductList />} /> 
        <Route path="/manageProduct" element={<ManageProduct />} /> 
        <Route path="/dashboard" element={<Dashboard />} /> 
      </Routes>
    </BrowserRouter>
  );
}

export default App;
