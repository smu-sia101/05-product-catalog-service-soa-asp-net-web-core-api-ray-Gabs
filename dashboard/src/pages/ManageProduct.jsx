/* eslint-disable no-unused-vars */
import React, { useState, useEffect } from "react";
import axios from "axios";
import "./ManageProduct.css";
import Sidebar from "./Sidebar.jsx";

function ManageProduct() {
  // State for form data
  const [product, setProduct] = useState({
    id: "",
    name: "",
    price: "",
    description: "",
    category: "",
    stock: "",
    imageUrl: "",
  });

  // State for success message or error message
  const [message, setMessage] = useState("");
  const [messageType, setMessageType] = useState(""); // Added to differentiate message types (success or error)

  // Handle input changes for the form
  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setProduct({ ...product, [name]: value });
  };

  // Handle form submission to add a product
  const handleSubmit = async (e) => {
    e.preventDefault();

    // Validate form inputs (basic validation)
    if (!product.id || !product.name || !product.price || !product.description) {
      setMessage("Please fill in all required fields.");
      setMessageType("error");
      return;
    }

    try {
      // Send a POST request to add the product
      const response = await axios.post("https://localhost:7091/api/products", product);

      // If successful, clear the form and display success message
      setProduct({
        id: "",
        name: "",
        price: "",
        description: "",
        category: "",
        stock: "",
        imageUrl: "",
      });
      setMessage("Product added successfully!");
      setMessageType("success");

      // Automatically hide message after 3 seconds
      setTimeout(() => {
        setMessage("");
        setMessageType("");
      }, 3000);
    } catch (error) {
      // Handle error
      setMessage("Error adding product.");
      setMessageType("error");

      // Automatically hide message after 3 seconds
      setTimeout(() => {
        setMessage("");
        setMessageType("");
      }, 3000);
      console.error(error);
    }
  };

  return (
    <div className="ManageProduct-container">
      <Sidebar />
      <div className="ManageProduct-Content">
        <h1>Manage Product</h1>
        <p>Manage your products here.</p>

        <form onSubmit={handleSubmit}>
          <div>
            <label>ID:</label>
            <input
              type="text"
              name="id"
              value={product.id}
              onChange={handleInputChange}
              required
            />
          </div>

          <div>
            <label>Name:</label>
            <input
              type="text"
              name="name"
              value={product.name}
              onChange={handleInputChange}
              required
            />
          </div>

          <div>
            <label>Price:</label>
            <input
              type="number"
              name="price"
              value={product.price}
              onChange={handleInputChange}
              required
            />
          </div>

          <div>
            <label>Description:</label>
            <textarea
              name="description"
              value={product.description}
              onChange={handleInputChange}
              required
            />
          </div>

          <div>
            <label>Category:</label>
            <input
              type="text"
              name="category"
              value={product.category}
              onChange={handleInputChange}
            />
          </div>

          <div>
            <label>Stock:</label>
            <input
              type="number"
              name="stock"
              value={product.stock}
              onChange={handleInputChange}
            />
          </div>

          <div>
            <label>Image URL:</label>
            <input
              type="text"
              name="imageUrl"
              value={product.imageUrl}
              onChange={handleInputChange}
            />
          </div>

          <button type="submit">Add Product</button>
        </form>

        {/* Message pop-out */}
        {message && (
          <div className={`message ${messageType} ${message ? "show" : ""}`}>
            {message}
          </div>
        )}
      </div>
    </div>
  );
}

export default ManageProduct;
