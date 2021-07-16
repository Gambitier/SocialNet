import React from 'react'
import { Container, Header } from 'semantic-ui-react'

class Landing extends React.Component {
  render() {
    return (
      <Container>
        <Header as='h3'>Project information</Header>
        <Container>
          <header as='h3'>
            Project repo link:
            <a href='https://github.com/Gambitier/UserManagement'>GitHub</a>
          </header>
        </Container>
      </Container>
    )
  }
}

export default Landing
