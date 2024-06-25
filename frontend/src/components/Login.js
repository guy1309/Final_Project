import React, { useState } from "react";
import { apiUrl } from "./Url";
import { Link } from "react-router-dom";
import axios from "axios";
import './Login.css';

export default function Login()
{
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = (e) => {
    let error = '';
    if(email === '')
    error = error + 'Email ,';

    if(password === '')
    error = error + 'Password ';
    
    if(error.length > 0)
    {
      error = error + ' can not be blank';
      alert(error);
      return;
    }

    e.preventDefault();
    const data = {
      Email: email,
      Password: password,
    };
    const url = `http://localhost:54842/api/Users/login`;
    axios
      .post(url, data)
      .then((result) => {
        const dt = result.data;
        if (dt.statusCode === 200) {
          if (email === "admin" && password === "admin") {
            localStorage.setItem("username", email);
            window.location.href = "/admindashboard";
          } else {
            localStorage.setItem("username", email);
         
            window.location.href = "/dashboard";
          }
        }
        else
        {
          alert(dt.statusMessage);
        }
      })
      .catch((error) => {
        console.log(error);
      });
  };

    return(
      <section className="h-100 gradient-form" styl="background-color: #eee;">
  <div className="container py-5 h-100">
    <div className="row d-flex justify-content-center align-items-center h-100">
      <div className="col-xl-10">
        <div className="card rounded-3 text-black">
          <div className="row g-0">
            <div className="col-lg-6">
              <div className="card-body p-md-5 mx-md-4">

                <div className="text-center">
                 
                  <h4 className="mt-1 mb-5 pb-1">We are The Lotus Team</h4>
                </div>

                <form>
                  <p>Please login to your account</p>

                  <div className="form-outline mb-4">
                    <input   type="email"
                      id="form1Example13"
                      className="form-control form-control-lg"
                      onChange={(e)=> setEmail(e.target.value)}
                      value={email}
                      placeholder="Enter Email"/>
                    <label className="form-label" htmlFor="form2Example11">User Email</label>
                  </div>

                  <div className="form-outline mb-4">
                    <input    type="password"
                      id="form1Example23"
                      className="form-control form-control-lg"
                      onChange={(e)=> setPassword(e.target.value)}
                       value={password}
                       placeholder="Enter Password" />
                    <label className="form-label" htmlFor="form2Example22">Password</label>
                  </div>

                  <div className="text-center pt-1 mb-5 pb-1">
                    <button  type="button"
                    className="btn btn-primary btn-block fa-lg gradient-custom-2 mb-3"
                    onClick={(e)=> handleLogin(e)}></button>
                    
                  </div>

                  <div className="d-flex align-items-center justify-content-center pb-4">
                    <p className="mb-0 me-2">Don't have an account?</p>
                    <button>
                     <Link to="/Registration" className="btn btn-info btn-lg btn-block">                   
                      Registration
                  </Link></button>
                  </div>

                </form>

              </div>
            </div>
            <div className="col-lg-6 d-flex align-items-center gradient-custom-2">
              <div className="text-white px-3 py-4 p-md-5 mx-md-4">
                <h4 className="mb-4">We are more than just a company</h4>
                <p className="small mb-0"></p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</section>
    )    
}



