import '../styles/HomePage.css'
import { useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";

function HomePage () {
    const navigate = useNavigate();
    const { loginWithRedirect } = useAuth0();
    
    return(
    <div className="welcome-container">
        <h1>Let's add your todos!</h1>
        <p>Organize your tasks, boost your productivity, and never miss a deadline.</p>
        <div className="welcome-links">
            <button 
            className="login-btn" 
            onClick={() => loginWithRedirect()}>
                Log In
            </button>
            <button
            className="register-btn"
            onClick={() => loginWithRedirect({ screen_hint: "signup" })}
            >
                Sign Up
            </button>
        </div>
    </div>
    )
}

export default HomePage;