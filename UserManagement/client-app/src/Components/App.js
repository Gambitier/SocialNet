import Signup from './Signup'
import Login from './Login'
import Home from './Home'
import AppHeader from './AppHeader'
import { Container } from "semantic-ui-react";

function App() {
  return (
    <Container>
      <AppHeader />
      <Signup />
      <Login />
      <Home />
    </Container>
  )
}

export default App;
