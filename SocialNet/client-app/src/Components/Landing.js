import React from 'react'
import { Container, Header } from 'semantic-ui-react'
import { Link } from 'react-router-dom'

class Landing extends React.Component {
  render() {
    return (
      <Container>
        <Header as='h3'>Project information</Header>
        <Container>
          <header as='h3'>
            Project repo link:
            <a href='https://github.com/Gambitier/SocialNet'>GitHub</a>
          </header>
          <br />
          <header>
            <Link to='/signup'>signup here</Link>
            <br />
            <br />
            <Link to='/login'>login here</Link>
          </header>
        </Container>
      </Container>
    )
  }
}

export default Landing
