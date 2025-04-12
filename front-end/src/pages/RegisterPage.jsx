import { useNavigate, Link } from "react-router-dom";
import { useState } from "react";

function RegisterPage (){
    const [fullName, setFullName] = useState("");
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const handleRegister = async (e) => {
        e.preventDefault();
        try{
            await axios.post("placeholdingapi", {
                fullName,
                username,
                email,
                password
            });
            alert("Registration successful! Please login.");
            navigate("/login");
        } catch(error){
            console.error("Registration error:", error);
            alert("Registration failed" + error.response?.data?.message || "Couldn't connect to server.");
        }
    };

    return(
        <div className="auth-container">
            <h2>Create Account</h2>
            <form className="auth-form" onSubmit={handleRegister}>
                <input type="text" placeholder="Full Name" value={fullName} onChange={(e) => setFullName(e.target.value)} required/>
                <input type="text" placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} required/>
                <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} required/>
                <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} required/>
                <button type="submit">Register</button>
            </form>
            <div className="auth-links">
                <p>Already have an account? <Link to={"/login"}>Log In</Link></p>
            </div>
        </div>
    )
}

export default RegisterPage;