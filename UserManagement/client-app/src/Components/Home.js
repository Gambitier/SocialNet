import React from 'react'
import { Container, Header } from 'semantic-ui-react'

class Home extends React.Component {
  render() {
    return (
      <Container>
        <Header as='h2'>User information</Header>
        <Container>
            <header as='h5'>User Name: </header>
        </Container>
      </Container>
    )
  }
}

export default Home
