import React from "react";
import Header from "./Header";
import "./About.css";
import { Card } from "react-bootstrap";
import Button from 'react-bootstrap/Button';
import para from "./Card-images/paracetamol.jpg";
import all from "./Card-images/all-in.jpg";
import cip from "./Card-images/cipralex.png";
import strep from "./Card-images/strepsils.webp";


export default function Dashboard() {
 
  return (
    <>
      <Header />
      <div>
       
        <body className="body2">
        <h1 className="h1">Welcome to our store</h1>
        <hr></hr>
        
        <div className="about3">
          On our website you can buy products that you need, for example soaps,
          ointments, medicines, protein powders and more. In the system you can
          create a user and connect at any time you want to buy from our
          products. In addition, you will be able to see all your orders and
          blockades in the system. The data you retrieve will be stored in our
          database. For any other question you may want to know, you can see at
          the end of the page the ways to contact with us. In the bottom there are some of are products....
        </div>

        <div className="div">
        <Card style={{ width: '18rem' }}>
        <img src={para} alt="para" />
      <Card.Body>
        <Card.Title>Paracetamol</Card.Title>
        <Card.Text>
        Paracetamol is a non-opioid analgesic and antipyretic agent used to treat fever and mild to moderate pain.
        Common brand names include Tylenol and Panadol.
        </Card.Text>
        <Button href="/products" variant="primary">buy now!</Button>
      </Card.Body>
    </Card>

    <Card style={{ width: '18rem' }}>
        <img src={cip} alt="cip" />
      <Card.Body>
        <Card.Title>Cipralex</Card.Title>
        <Card.Text>
        Escitalopram, sold under the brand names Lexapro and Cipralex, among others, 
        is an antidepressant of the selective serotonin reuptake inhibitor class,it is taken by mouth.
        </Card.Text>
        <Button href="/products" variant="primary">buy now!
       </Button>
      </Card.Body>
    </Card>

    <Card style={{ width: '18rem' }}>
        <img src={all} alt="all" />
      <Card.Body>
        <Card.Title>All-in</Card.Title>
        <Card.Text>
        Protein bars are a convenience food that contains a high proportion of protein relative to carbohydrates and fats. 
        protein bars contain more added sugar than some desserts like cookies.
        </Card.Text>
        <Button href="/products" variant="primary">buy now!</Button>
      </Card.Body>
    </Card>
    
    <Card style={{ width: '18rem' }}>
        <img src={strep} alt="strep" />
      <Card.Body>
        <Card.Title>Strepsils</Card.Title>
        <Card.Text>
        trepsils is a brand of throat lozenges manufactured by British-Dutch company Reckitt Benckiser. 
        Strepsils throat lozenges are used to relieve discomfort caused by mouth and throat infections.
        </Card.Text>
        <Button href="/products" variant="primary">buy now!</Button>
      </Card.Body>
    </Card>
    </div>
      </body>
      </div>
    </>
  );
}
