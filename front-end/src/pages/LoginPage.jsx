import '../styles/Auth.css'
import axios from "axios";
import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";

function LoginPage (){
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.prevetDefault();
        try{
            await axios.post("placeholdingapi", {
                username,
                password
            });
            alert("Login Successful!");
            navigate("/todos")
        } catch(error) {
            // optional chaining(?.) 
            console.error("Login error:", error);
            alert("Login Failed: " + (error.response?.data?.message || "Could not connect to server"));
        }
    }

    return(
        <div className="auth-container">
            <h2>Welcome Back!</h2>
            <form className="auth-form" onSubmit={handleLogin}>
                <input type="text" placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} required/>
                <input type="text" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} required/>
                <button type="submit">Log In</button>
            </form>
            <div className="auth-links">
                <p>Don't have an account? <Link to="/register">Register</Link></p>
            </div>
        </div>
    )
}

export default LoginPage;