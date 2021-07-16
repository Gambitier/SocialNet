import React from 'react'
import { Button, Container, Form, Header } from 'semantic-ui-react'

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
    this.props.loginUserHandler(this.state)
    this.setState({
      userName: '',
      password: '',
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
