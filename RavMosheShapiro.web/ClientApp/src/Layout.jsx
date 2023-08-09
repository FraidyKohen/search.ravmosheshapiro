import React from 'react';
import { Link } from 'react-router-dom';

const Layout = ({ children }) => {
    return (
        <div>
            <header>
                <nav className="navbar navbar-dark static-top navbar-expand-lg" style={{ backgroundColor: '#080464', maxHeight: 40  }}>
                    <button className="navbar-toggler border-0" style={{ color: '#080464' }} type="button" data-toggle="collapse" data-target="#navbarSupportedContent"> <span className="navbar-toggler-icon "></span> </button>
                    <div className="collapse navbar-collapse" id="navbarSupportedContent">
                        <ul className="navbar-nav mx-auto">
                            <li className="nav-item mx-4"> <a className="nav-link active" href="https://ravmosheshapiro.com/index.html">Books | ספרים</a> </li>
                            <li className="nav-item mx-4"> <a className="nav-link" href="https://ravmosheshapiro.com/subscribe.html">Subscribe | הירשם</a> </li>
                            <li className="nav-item mx-4"> <a className="nav-link" href="https://ravmosheshapiro.com/parshah19.html">Parshah | פרשה</a> </li>
                            <li className="nav-item mx-4"> <a className="nav-link" href="https://ravmosheshapiro.com/mishlei.html">Mishlei | משלי</a> </li>
                            <li className="nav-item mx-4"> <a className="nav-link" href="https://ravmosheshapiro.com/about.html">About | הסכמות</a> </li>
                            <li className="nav-item mx-4"> <a className="nav-link" href="https://www.zyapublications.com/">Siddur | סידור</a> </li>
                        </ul>
                    </div>
                    </nav>
            </header>
            <div className="container" style={{ marginTop: 5 }}>
                {children}
            </div>
        </div >

    )
}
export default Layout;



