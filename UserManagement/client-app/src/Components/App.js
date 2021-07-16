import { Container } from 'semantic-ui-react'
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom'
import AppHeader from './AppHeader'
import Signup from './Signup'
import Login from './Login'
import Home from './Home'
import Landing from './Landing'
import API from '../API/users'


function App() {
  const signupUserHandler = async (user) => {
    const response = await API.post('/users/signup', user)
    console.log('user signed up: ', user)
    console.log('server response: ', response.data)
  }

  const loginUserHandler = async (userCreds) => {
    const response = await API.post('/users/login', userCreds)
    console.log('user logged in: ', userCreds)
    console.log('server response: ', response.data)
  }

  return (
    <Container>
      <Router>
        <AppHeader />
        <Switch>
          <Route path='/' exact render={(props) => <Landing {...props} />} />
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

export default App
