import React from "react";
import { useNavigate } from "react-router-dom"; // Import useNavigate hook
import FormatListBulletedIcon from '@mui/icons-material/FormatListBulleted';// Example icon, you can replace it
import Inventory2Icon from '@mui/icons-material/Inventory2';// Example icon, replace it as needed

function Dashboard() {
    const navigate = useNavigate(); 
  return (
    <div className="dashboard-container">
      <aside className="sidebar">
        <div className="tab" onClick={() => navigate("/")}>    {/* product  list */}
            <FormatListBulletedIcon /> 
        </div>
        <div className="tab" onClick={() => navigate("/manageProduct")}> 
            {/* Manage Product */}
          <Inventory2Icon /> 
        </div>
      </aside>
    </div>
  );
}

export default Dashboard;
