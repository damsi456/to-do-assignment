import '../styles/TodoPage.css';
import axios from "axios";
import { useEffect, useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import TodoList from "../components/ToDoList";
import Filter from "../components/Filter";
import DashBoard from "../components/DashBoard";

function TodoPage(){
    const [tasks, setTasks] = useState([]);
    const [filter, setFilter] = useState('All');
    const { logout } = useAuth0();
    const { user, isAuthenticated } = useAuth0();

    // sends user data to backend if autheticated
    useEffect(() => {
        const syncUser = async () => {
            if (isAuthenticated && user) {
                try {
                    await axios.post("http://localhost:5297/api/users", {
                      username: user.name || user.nickname ,
                      email: user.email,
                      auth0Id: user.sub
                    });
                  } catch (err) {
                    console.error("Failed to sync user:", err);
                  }
            }
        };
        syncUser();
    }, [isAuthenticated, user]);

    const addTask = (e) => {
        e.preventDefault();
        const taskText = e.target.elements.taskInput.value;
        if(taskText){
            setTasks([...tasks, {id: Date.now(), text: taskText, completed: false}]);
            e.target.reset();
        }
    }

    const changeTaskCompletion = (id) => {
        setTasks(tasks.map(task => task.id === id ? {...task, completed: !task.completed} : task));
    }

    const updateTaskText = (id, newText) => {
        setTasks(tasks.map(task => task.id === id ? {...task, text: newText} : task))
    }

    const deleteTask = (id) => {
        setTasks(tasks.filter(task => task.id !== id));
    }

    // filtering tasks based on the selected filter in Filter component
    const filteredTasks = tasks.filter(task => {
        if(filter === 'Active') return task.completed === false;
        if(filter === 'Completed') return task.completed === true;
        return true;
    })

    const getActiveTasksCount = () => {
        const activeTasks = tasks.filter(task => task.completed === false);
        return activeTasks.length;
    }

    const getCompletedTasksCount = () => {
        const completedTasks = tasks.filter(task => task.completed === true);
        return completedTasks.length;
    }

    return(
        <div className="todos-container">
            <div className="todos-header">
                <h1>To-Do Application</h1>
                <button className="logout-btn" onClick={() => logout({ logoutParams: { returnTo: "http://localhost:5173/" } })}>
                    Log Out
                </button>
            </div>
            <DashBoard getActiveTasksCount={getActiveTasksCount} getCompletedTasksCount={getCompletedTasksCount}/>
            <Filter filter={filter} setFilter={setFilter}/>
            <form onSubmit={addTask} className="todo-form">
                <input type="text" name="taskInput" className="add-todo" placeholder="Add a new task"/>
                <button type="submit">Add</button>
            </form>
            <TodoList tasks={filteredTasks} changeTaskCompletion={changeTaskCompletion} deleteTask={deleteTask} updateTaskText={updateTaskText}/>
        </div>
    )
}

export default TodoPage;