import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'
import { Auth0Provider } from '@auth0/auth0-react'
import App from './App.jsx'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
      <Auth0Provider
        domain="dev-xx61bkk427qjlvct.us.auth0.com"
        clientId="YYYMVIu33rnFFCliVceWBDoBS0gwWyZY"
        authorizationParams={{
          redirect_uri: "http://localhost:5173/todos"
        }}
      >
        <App />
      </Auth0Provider>
    </BrowserRouter>
  </StrictMode>
)
