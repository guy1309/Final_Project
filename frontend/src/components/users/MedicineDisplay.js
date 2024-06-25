import React, { Fragment, useEffect, useState } from "react";
import axios from "axios";
import { apiUrl } from "../Url";
import Header from "./Header";

export default function MedicineDisplay() {
  const [data, setData] = useState([]);
  const [quantity, setOrderQuantity] = useState(1);

  useEffect(() => {
    getData();
  }, []);

  const getData = () => {
    const data = {
      Email: "Admin",
    };
    const url = `${apiUrl}Admin/cartList`;
    axios
      .post(url, data)
      .then((result) => {
        const data = result.data;
        if (data.statusCode === 200) {
          setData(data.listCart);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };

  const handleAddToCart = (e, id) => {
    e.preventDefault();
    const data = {
      ID: id,
      Quantity: quantity,
      Email: localStorage.getItem("username"),
    };
    const url = `${apiUrl}Medicines/addToCart`;
    axios
      .post(url, data)
      .then((result) => {
        const dt = result.data;
        if (dt.statusCode === 200) {
          getData();
          alert(dt.statusMessage);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
    <Fragment>
      <Header />
      <br></br>
      <div
        style={{
          backgroundColor: "white",
          width: "80%",
          margin: "0 auto",
          borderRadius: "11px",
        }}
      >
        <div className="card-deck">
          {data && data.length > 0
            ? data.map((val, index) => {
                return (
                  <div
                    key={index}
                    className="col-md-3"
                    style={{ marginBottom: "21px" }}
                  >
                    <div className="card">
                      <img
                        className="card-img-top"
                        src={`public/assests/Images/${val.image}`}
                        alt={val.Name}
                      />
                      <div className="card-body">
                        <h4 className="card-title"> {val.medicineName}</h4>
                        <h4 className="card-title">
                          <select
                            id="medicineQuantity"
                            className="form-control"
                            onChange={(e) => setOrderQuantity(e.target.value)}
                          >
                            <option value="-1">Select Quantity</option>
                            <option value="1">1</option>
                            <option value="2">2</option>
                            <option value="3">3</option>
                            <option value="4">4</option>
                          </select>
                        </h4>
                        <button
                          className="btn btn-primary"
                          onClick={(e) => handleAddToCart(e, val.id)}
                        >
                          Add to cart
                        </button>
                      </div>
                    </div>
                  </div>
                );
              })
            : "Loading products..."}
        </div>
      </div>
    </Fragment>
  );
}
