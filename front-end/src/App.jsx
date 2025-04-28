import './App.css'
import { Routes, Route } from 'react-router-dom';
import HomePage from './pages/HomePage.jsx';
import TodoPage from './pages/TodoPage.jsx'

function App() {
  return (
    <Routes>
      <Route path='/' element={<HomePage />}/>
      <Route path='/todos' element={<TodoPage />}/>
    </Routes>
  )
}

export default App
