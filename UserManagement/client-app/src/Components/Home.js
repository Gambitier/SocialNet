import React from 'react'
import { Container, Header } from 'semantic-ui-react'
import { getUserInfo } from '../API/users'
import jwt_decode from 'jwt-decode'

class Home extends React.Component {
  state = {
    userName : '',
    firstName : '',
    lastName : '',
    email : '',
  };

  getUser = (userid) => {
    getUserInfo(userid).then((response) => {
      this.setState({...response.data})
    }).catch((err) => {
      console.log(err);
    });
  }
  
  componentDidMount(){
    const LOCAL_STORAGE_KEY = 'BearerToken'
    var token = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY))
    var decoded = jwt_decode(token)
    var userid = decoded.UserId;
    this.getUser(userid)
  }

  render() {
    return (
      <Container>
        <Header as='h2'>User information</Header>
        <Container>
          <header as='h5'>User Name: {`${this.state.firstName} ${this.state.lastName}`}</header>
        </Container>
      </Container>
    )
  }
}

export default Home
