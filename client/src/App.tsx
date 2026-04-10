import {Typography, List, ListItem, ListItemText} from "@mui/material";
import axios from "axios";
import { useEffect, useState } from 'react';

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
    axios.get<Activity[]>('http://localhost:5000/api/activities') //todo: get https working later      
      .then((response) => setActivities(response.data));

      return () => {}
  }, []);

  return (
    <>
      <Typography  variant='h3'>Reactivities</Typography>
      <List>
        {activities.map((activity) => (
          <ListItem key={activity.id}>
            <ListItemText>{activity.title}</ListItemText>
          </ListItem>
        ))}
      </List>
    </>
  );
}

export default App;
