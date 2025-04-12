import './App.css'
import { Routes, Route } from 'react-router-dom';
import TodoPage from './pages/TodoPage.jsx'

function App() {
  return (
    <Routes>
      <Route path='/todos' element={<TodoPage />}/>
    </Routes>
  )
}

export default App
