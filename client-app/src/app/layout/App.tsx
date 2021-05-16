import React, { useEffect, useState } from 'react';
import { Container } from 'semantic-ui-react';
import { Activity } from '../models/activity';
import NavBar from './navbar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import {v4 as uuid} from 'uuid';
import agent from '../api/agent';
import LoadingComponnet from './LoadingComponnet';

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined);
  const [editMode, setEditMode] = useState(false);
  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);

  useEffect(() => {
    agent.Activities.list().then(response => {
      console.log(response);

      let activities: Activity[] = [];
      response.forEach(activity => {
        activity.date = activity.date.split('T')[0];
        activities.push(activity)
      });

      setActivities(activities);
      setLoading(false);
    })
  }, [])

  function handleSelectActivity(id: string) {
    setSelectedActivity(activities.find(x => x.id === id))
  }

  function handleCancelSelectActivity() {
    setSelectedActivity(undefined);
  }

  function handleFormOpen(id?: string) {
    id? handleSelectActivity(id) : handleCancelSelectActivity();
    setEditMode(true);
  }

  function handleFormClose() {
    setEditMode(false);
  }

  function handleCreateOrEditActivity(activty: Activity) {
    setSubmitting(true);

    if(activty.id) {
      agent.Activities.update(activty).then(() => {
        setActivities([...activities.filter(x => x.id !== activty.id), activty]);
        setSelectedActivity(activty);
        setEditMode(false);
        setSubmitting(false);
      })
    }
    else {
      activty.id = uuid();
      agent.Activities.create(activty).then(() => {
        setActivities([...activities, activty]);
        setSelectedActivity(activty);
        setEditMode(false);
        setSubmitting(false);
      })
    }
  }

  function handleDeletActivity(id: string)
  {
    setActivities([...activities.filter(x => x.id !== id)])
  }

  if (loading) return <LoadingComponnet content='Loading app' />

  return (
    <>
      <NavBar openForm={handleFormOpen}/>
      <Container style={{marginTop: '7em'}} >
        <ActivityDashboard 
          activites={activities}
          selectedActivity={selectedActivity}
          selectActivity={handleSelectActivity}
          cancelSelectActivity={handleCancelSelectActivity}
          editMode={editMode}
          openForm={handleFormOpen}
          closeForm={handleFormClose}
          createOrEdit={handleCreateOrEditActivity}
          deleteActivity={handleDeletActivity}
          submitting={submitting}
        />
      </Container>
    </>
  );
}

export default App;
