import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import axios from '../node_modules/axios';
import { AxiosResponse } from 'axios';
import { Header, Icon, List } from 'semantic-ui-react';

class App extends Component {
  state = {
    values: [],
  };

  componentDidMount() {
    axios
      .get('http://localhost:5000/weatherforecast')
      .then((response: AxiosResponse) => {
        this.setState({
          values: response.data,
        });
      });
  }

  render() {
    return (
      <div>
        <Header as="h2">
          <Icon name="plug" />
          <Header.Content>Uptime Guarantee</Header.Content>
        </Header>
        <List>
          {this.state.values.map((value: any) => (
            <List.Item key={value.id}>{value.name}</List.Item>
          ))}
        </List>
      </div>
    );
  }
}

export default App;
