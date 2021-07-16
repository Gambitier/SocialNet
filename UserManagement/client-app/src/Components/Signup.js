import React from "react";
import { Button, Container, Form, Header } from 'semantic-ui-react'

class Signup extends React.Component{

    render(){
        return (
          <Container>
            <Header as='h2'>Signup Form</Header>
            <Form>
              <Form.Field>
                <label>Username</label>
                <input placeholder='Username' />
              </Form.Field>
              <Form.Field>
                <label>First Name</label>
                <input placeholder='First Name' />
              </Form.Field>
              <Form.Field>
                <label>Last Name</label>
                <input placeholder='Last Name' />
              </Form.Field>
              <Form.Field>
                <label>Email</label>
                <input placeholder='Email' />
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

export default Signup;
