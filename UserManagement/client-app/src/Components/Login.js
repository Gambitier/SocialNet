import React from 'react'
import { Button, Container, Form, Header } from 'semantic-ui-react'
import { loginUserHandler } from '../API/users'

class Login extends React.Component {
  state = {
    userName: '',
    password: '',
  }

  login = (e) => {
    e.preventDefault()
    var stateIsValid = Object.values(this.state).every((value) =>
      Boolean(String(value).trim())
    )
    if (!stateIsValid) {
      alert('all the fields are mandatory')
      return
    }
    this.setState({
      userName: '',
      password: '',
    })
    loginUserHandler(this.state)
      .then((response) => {
        const LOCAL_STORAGE_KEY = 'BearerToken'
        localStorage.setItem(
          LOCAL_STORAGE_KEY,
          JSON.stringify(response.data.token)
        )
        this.props.history.push('/home')
      })
      .catch((error) => {
        console.log(error)
      })
  }

  render() {
    return (
      <Container>
        <Header as='h2'>Login Form</Header>
        <Form onSubmit={this.login}>
          <Form.Field>
            <label>Username</label>
            <input
              type='text'
              name='userName'
              placeholder='Username'
              value={this.state.userName}
              onChange={(e) => this.setState({ userName: e.target.value })}
            />
          </Form.Field>
          <Form.Field>
            <label>Password</label>
            <input
              type='password'
              name='password'
              placeholder='Password'
              value={this.state.password}
              onChange={(e) => this.setState({ password: e.target.value })}
            />
          </Form.Field>
          <Button type='submit'>Submit</Button>
        </Form>
      </Container>
    )
  }
}

export default Login
