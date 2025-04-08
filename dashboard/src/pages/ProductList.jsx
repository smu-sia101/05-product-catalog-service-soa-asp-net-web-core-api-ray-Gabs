import React, { useState, useEffect } from "react";
import axios from "axios"; // Import Axios
import "./ProductList.css";
import Sidebar from "./Sidebar.jsx";
import { styled } from "@mui/material/styles";
import Card from "@mui/material/Card";
import CardHeader from "@mui/material/CardHeader";
import CardMedia from "@mui/material/CardMedia";
import CardContent from "@mui/material/CardContent";
import CardActions from "@mui/material/CardActions";
import Collapse from "@mui/material/Collapse";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import DeleteIcon from "@mui/icons-material/Delete";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import Modal from "@mui/material/Modal";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";

// Modal style
const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "90%",  // Make the modal responsive
  maxWidth: "600px", // Limit the maximum width
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  pt: 2,
  px: 4,
  pb: 3,
  maxHeight: "80vh", // Restrict the modal height to 80% of the viewport height
  overflowY: "auto", // Enable scrolling if content exceeds the modal's max height
};

const ExpandMore = styled((props) => {
  const { expand, ...other } = props;
  return <IconButton {...other} />;
})(({ theme, expand }) => ({
  transform: !expand ? "rotate(0deg)" : "rotate(180deg)",
  transition: theme.transitions.create("transform", {
    duration: theme.transitions.duration.shortest,
  }),
}));

function ProductList() {
  const [products, setProducts] = useState([]);
  const [expanded, setExpanded] = useState({});
  const [openModal, setOpenModal] = useState(false);
  const [productToView, setProductToView] = useState(null);

  useEffect(() => {
    const apiUrl = "https://localhost:7091/api/products";
    axios
      .get(apiUrl)
      .then((response) => {
        setProducts(response.data);
      })
      .catch((error) => {
        console.error("There was an error fetching the products:", error);
      });
  }, []);

  const handleExpandClick = (id) => {
    setExpanded((prevState) => ({
      ...prevState,
      [id]: !prevState[id],
    }));
  };

  const handleCardClick = (product) => {
    setProductToView(product);
    setOpenModal(true);
  };

  const handleDelete = () => {
    const apiUrl = `https://localhost:7091/api/products/${productToView.id}`;
    axios
      .delete(apiUrl)
      .then(() => {
        setProducts((prevState) =>
          prevState.filter((product) => product.id !== productToView.id)
        );
        setOpenModal(false);
      })
      .catch((error) => {
        console.error("There was an error deleting the product:", error);
      });
  };

  const handleCloseModal = () => {
    setOpenModal(false);
    setProductToView(null);
  };

  const handleOutsideClick = (e) => {
    if (e.target.id === "modal-background") {
      handleCloseModal();
    }
  };

  return (
    <div className="ProductList-container">
      <Sidebar />
      <div className="product-list">
        <div className="product-header-container">
          <h1>Product List</h1>
        </div>
        <div className="product-cards">
          {products.map((product) => (
            <Card sx={{ maxWidth: 345 }} key={product.id} onClick={() => handleCardClick(product)}>
              <CardHeader
                action={
                  <IconButton onClick={() => handleCardClick(product)} aria-label="view">
                    <DeleteIcon />
                  </IconButton>
                }
                title={product.name}
                subheader={`Category: ${product.category}`}
              />
              <CardMedia
                component="img"
                image={product.imageUrl}
                alt={product.name}
                style={{
                  objectFit: "cover", // Ensures the image covers the container while maintaining its aspect ratio
                  width: "100%",      // Makes the image fill the width of the container
                  height: "200px",    // Adjust the height as needed
                }}
              />
              <CardContent>
                <Typography variant="body2" color="text.secondary">
                  {product.description}
                </Typography>
                <Typography variant="h6">Price: ${product.price}</Typography>
                <Typography variant="body2" color="text.secondary">
                  Stock: {product.stock}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Total Price: ${(product.price * product.stock).toFixed(2)}
                </Typography>
              </CardContent>
              <CardActions disableSpacing>
                <ExpandMore
                  expand={expanded[product.id] || false}
                  onClick={() => handleExpandClick(product.id)}
                  aria-expanded={expanded[product.id] || false}
                  aria-label="show more"
                >
                  <ExpandMoreIcon />
                </ExpandMore>
              </CardActions>
              <Collapse in={expanded[product.id] || false} timeout="auto" unmountOnExit>
                <CardContent>
                  <Typography sx={{ marginBottom: 2 }}>Additional Information:</Typography>
                  <Typography>{product.description}</Typography>
                </CardContent>
              </Collapse>
            </Card>
          ))}
        </div>
      </div>

      {/* Modal for Product Details and Delete Confirmation */}
      <Modal
        open={openModal}
        onClose={handleCloseModal}
        aria-labelledby="product-modal-title"
        aria-describedby="product-modal-description"
        id="modal-background"
        onClick={handleOutsideClick}
      >
        <Box sx={style}>
          <Typography variant="h6" id="product-modal-title">
            {productToView?.name}
          </Typography>
          <Typography variant="body2" sx={{ marginBottom: 2 }}>
            Category: {productToView?.category}
          </Typography>
          <Typography variant="body2" sx={{ marginBottom: 2 }}>
            Price: ${productToView?.price}
          </Typography>
          <Typography variant="body2" sx={{ marginBottom: 2 }}>
            Stock: {productToView?.stock}
          </Typography>
          <Typography sx={{ marginBottom: 2 }}>
            Additional Information: {productToView?.description}
          </Typography>
          <div>
            <Button onClick={handleDelete} variant="contained" color="error" sx={{ marginRight: 2 }}>
              Confirm Delete
            </Button>
            <Button onClick={handleCloseModal} variant="outlined">
              Close
            </Button>
          </div>
        </Box>
      </Modal>
    </div>
  );
}

export default ProductList;
