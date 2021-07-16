import Signup from './Signup'
import Login from './Login'
import Landing from './Landing'
import Home from './Home'
import AppHeader from './AppHeader'
import { Container } from "semantic-ui-react";
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom'

function App() {
  const signupUserHandler = (user) => {
    console.log('user signed up: ', user)
  };

  const loginUserHandler = (userCreds) => {
    console.log('user logged in: ', userCreds)
  };

  return (
    <Container>
      <Router>
        <AppHeader />
        <Switch>
          <Route
            path='/'
            exact
            render={(props) => (
              <Landing {...props} />
            )}
          />
          <Route
            path='/signup'
            render={(props) => (
              <Signup {...props} signupUserHandler={signupUserHandler} />
            )}
          />
          <Route
            path='/login'
            render={(props) => (
              <Login {...props} loginUserHandler={loginUserHandler} />
            )}
          />
          <Route path='/home' render={(props) => <Home {...props} />} />
        </Switch>
      </Router>
    </Container>
  )
}

export default App;
