import React from 'react'
import './Footer.css'

const Footer = () => {
  return (
 <section className='footer'>
  <hr className='footer-seperator'/>
  <section className='footer-social-media'>
   <a  className='a1' href="/" target="_blank" rel='noopener noreferrer'>facebook</a>
   <a  className='a2' href="/" target="_blank" rel='noopener noreferrer'>instagram</a> 
   <a  className='a3' href="/" target="_blank" rel='noopener noreferrer'>linkedin</a>  
  </section>

 <section className='footer-info'>
<section className='footer-info-left'>
<section className='footer-info_name'>
software Engineer Haydin
</section>
<section className='footer-info_returnes'>
Returns policy
<br />
Delivary
</section>
</section>

<section className='footer-info-center'>
<section className='footer-info_email'>
myBudget@gmail.com
</section>
<section>
<section className='footer-info_terms'>
  Terms and Conditions
  <br />
  Copyright
</section>
</section>
</section>

<section className='footer-info-right'>
<section className='footer-info_number'>
 09-2067356
</section>
<section className='footer-info_contact'>
@2022
<br />
Contact us
    </section>
   </section>
  </section>
  <hr className='footer-seperator'/>
 </section>
  )
}

export default Footer