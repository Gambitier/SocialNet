import React from "react";
import { Button, Container, Form, Header } from 'semantic-ui-react'
import { signupUserHandler } from "../API/users"

class Signup extends React.Component {
  state = {
    userName: '',
    firstName: '',
    lastName: '',
    email: '',
    password: '',
  }

  signUp = (e) => {
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
      firstName: '',
      lastName: '',
      email: '',
      password: '',
    })
    signupUserHandler(this.state).then(() => {
      this.props.history.push('/login')
    }).catch((err) => {
      console.log(err);
    })
  }

  render() {
    return (
      <Container>
        <Header as='h2'>Signup Form</Header>
        <Form onSubmit={this.signUp}>
          <Form.Field>
            <label>Username</label>
            <input
              type='text'
              name='username'
              placeholder='Username'
              value={this.state.userName}
              onChange={(e) => this.setState({ userName: e.target.value })}
            />
          </Form.Field>
          <Form.Field>
            <label>First Name</label>
            <input
              type='text'
              name='firstName'
              placeholder='First Name'
              value={this.state.firstName}
              onChange={(e) => this.setState({ firstName: e.target.value })}
            />
          </Form.Field>
          <Form.Field>
            <label>Last Name</label>
            <input
              type='text'
              name='lastName'
              placeholder='Last Name'
              value={this.state.lastName}
              onChange={(e) => this.setState({ lastName: e.target.value })}
            />
          </Form.Field>
          <Form.Field>
            <label>Email</label>
            <input
              type='email'
              name='email'
              placeholder='Email'
              value={this.state.email}
              onChange={(e) => this.setState({ email: e.target.value })}
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

export default Signup;
