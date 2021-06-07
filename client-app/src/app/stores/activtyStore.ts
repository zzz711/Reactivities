import { makeAutoObservable, runInAction } from 'mobx';
import agent from '../api/agent';
import { Activity } from '../models/activity';
import {v4 as uuid} from 'uuid';

export default class ActivityStore {
  activities: Activity[] = [];
  selectedActivity: Activity | undefined = undefined;
  editMode = false;
  loading = false;
  loadingInital = false;

  constructor() {
    makeAutoObservable(this);
  }

  loadActivities = async () => {
    this.setLoadingInital(true);
    try {
      const activities = await agent.Activities.list();

      activities.forEach((activity) => {
        activity.date = activity.date.split('T')[0];
        this.activities.push(activity);
      });
      this.setLoadingInital(false);
    } catch (error) {
      console.log(error);
      this.setLoadingInital(false);
    }
  }

  setLoadingInital = (state: boolean) => {
    this.loadingInital = state;
  }

  selectActivity = (id: string) => {
      this.selectedActivity = this.activities.find(a => a.id === id);
  }

  cancelSelectedActivity = () => {
      this.selectedActivity = undefined;
  }

  openForm = (id? :string) => {
      id ? this.selectActivity(id) : this.cancelSelectedActivity();
      this.editMode = true;
  }

  closeForm = () => {
      this.editMode = false;
  }

  createActivty = async (activity : Activity) => {
    this.loading = true;
    activity.id = uuid();

    try {
        await agent.Activities.create(activity);
        runInAction(() => {
            this.activities.push(activity);
            this.selectedActivity = activity;
            this.editMode = false;
            this.loading = false;
        })
    } catch (error) {
        console.log(error);
        runInAction(() => {
            this.loading = false;
        })
    }
  }

  updateActivity = async (actiivty: Activity) => {
      this.loading = false;
      try {
        await agent.Activities.update(actiivty);

        runInAction(() => {
            this.activities = [...this.activities.filter(a => a.id !== actiivty.id), actiivty];            
            this.selectedActivity = actiivty;
            this.editMode = false;
            this.loading = false;
        })

      } catch (error) {
          console.log(error);
          runInAction(() => {
              this.loading = false;
          })
      }
  }

  deleteActivity = async(id: string) => {
    this.loading = true;
    try {
      await agent.Activities.delete(id);
      runInAction(() => {
        this.activities = [...this.activities.filter(a => a.id !== id)];
        if(this.selectedActivity?.id === id) {
          this.cancelSelectedActivity();
        }
        this.loading = false;
      })
    }catch(error) {
      console.log(error);
      runInAction(() => {
        this.loading = false;
      })
    }
  }

}
