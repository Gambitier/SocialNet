import Signup from './Signup'
import Login from './Login'
import Home from './Home'
import AppHeader from './AppHeader'
import { Container } from "semantic-ui-react";

function App() {
  const signupUserHandler = (user) => {
    console.log('user signed up: ', user)
  };

  const loginUserHandler = (userCreds) => {
    console.log('user logged in: ', userCreds)
  };

  return (
    <Container>
      <AppHeader />
      <Signup signupUserHandler={signupUserHandler} />
      <Login loginUserHandler={loginUserHandler} />
      <Home />
    </Container>
  )
}

export default App;
