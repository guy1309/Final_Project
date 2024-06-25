import React from "react";
import AdminHeader from "./Adminheader";
import "./modal.css";
export default function AdminDashboard() {
  return (
    <>
      <AdminHeader />
      <div>
        <body className="body1">
        <h1>welcome admin</h1>
        <div className="text">
        Hello dear admin, on this panel you can follow every order that made and every new user that signed in our website.
        All the information you will able to see in our database too.
        In addition, you can upload additional products from here that will be displayed to our users on the site.

        </div>
        </body>
       
      </div>
    </>
  );
}
