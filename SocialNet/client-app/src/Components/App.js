import { Container } from 'semantic-ui-react'
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom'
import AppHeader from './AppHeader'
import Signup from './Signup'
import Login from './Login'
import Home from './Home'
import Landing from './Landing'

function App() {
  return (
    <Container>
      <Router>
        <AppHeader />
        <Switch>
          <Route path='/' exact render={(props) => <Landing {...props} />} />
          <Route path='/home' render={(props) => <Home {...props} />} />
          <Route
            path='/signup'
            render={(props) => ( <Signup {...props} />)}/>
          <Route
            path='/login'
            render={(props) => <Login {...props} />} />
        </Switch>
      </Router>
    </Container>
  )
}

export default App
