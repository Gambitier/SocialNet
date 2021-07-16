import React from 'react'
import { Button, Container, Form, Header } from 'semantic-ui-react'

class Login extends React.Component {
  render() {
    return (
      <Container>
        <Header as='h2'>Login Form</Header>
        <Form>
          <Form.Field>
            <label>Username</label>
            <input placeholder='Username' />
          </Form.Field>
          <Form.Field>
            <label>Password</label>
            <input placeholder='Password' />
          </Form.Field>
          <Button type='submit'>Submit</Button>
        </Form>
      </Container>
    )
  }
}

export default Login
