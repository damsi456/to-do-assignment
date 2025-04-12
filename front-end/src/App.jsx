import './App.css'
import { Routes, Route } from 'react-router-dom';
import HomePage from './pages/HomePage.jsx';
import RegisterPage from './pages/RegisterPage.jsx';
import LoginPage from './pages/LoginPage.jsx';
import TodoPage from './pages/TodoPage.jsx'

function App() {
  return (
    <Routes>
      <Route path='/' element={<HomePage />}/>
      <Route path='/todos' element={<TodoPage />}/>
      <Route path='/register' element={<RegisterPage />} />
      <Route path='/login' element={<LoginPage />} />
    </Routes>
  )
}

export default App
