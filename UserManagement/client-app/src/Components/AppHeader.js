import React from 'react';
import { Header, Image } from 'semantic-ui-react'

class AppHeader extends React.Component{

    render(){
        return (
          <Header as='h2'>
            <Image
              circular
              src='https://react.semantic-ui.com/images/avatar/large/patrick.png'
            />{' '}
            User Management Project
          </Header>
        )
    }
}

export default AppHeader