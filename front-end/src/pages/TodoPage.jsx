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
    const [uid, setUid] = useState(); 
    const { logout } = useAuth0();
    const { user, isAuthenticated } = useAuth0();

    // Send user data to backend if autheticated
    useEffect(() => {
        const syncUser = async () => {
            if (isAuthenticated && user) {
                try {
                    var response = await axios.post("http://localhost:5297/api/users", {
                      username: user.name || user.nickname || user.email,
                      email: user.email,
                      auth0Id: user.sub
                    });
                    // debug
                    console.log(response);
                    setUid(response.data.id); // To be used to send requests
                } catch (err) {
                    console.error("Failed to send user:", err);
                }
            }
        };
        syncUser();
    }, [isAuthenticated, user]);

    // Fetch user tasks based on the filter
    useEffect(() => {
        if (!uid) return;
        const fetchTasks = async () => {
            try {
                if (filter === "All") {
                    const res = await axios.get(`http://localhost:5297/api/tasks/uid/${uid}`);
                    setTasks(res.data);
                } else if (filter === "Active"){
                    const res = await axios.get(`http://localhost:5297/api/tasks/filter/${uid}/false`);
                    setTasks(res.data);
                } else {
                    const res = await axios.get(`http://localhost:5297/api/tasks/filter/${uid}/true`);
                    setTasks(res.data);
                }
            } catch (err) {
                console.error("Failed to fetch tasks:", err);
            }
        };
        fetchTasks();
    }, [uid, filter]);

    const addTask = async (e) => {
        e.preventDefault();
        const taskText = e.target.elements.taskInput.value;
        if(taskText && uid){
            try {
                const res = await axios.post("http://localhost:5297/api/tasks", {
                    title: taskText,
                    userId: uid
                });
                setTasks([...tasks, res.data]);
                e.target.reset();
            } catch (err) {
                console.error("Failed to add task:", err);
            }
        }
    }

    const changeTaskCompletion = async (id) => {
        const task = tasks.find(task => task.id === id);
        try {
            console.log("Task object:", task);
            await axios.put(`http://localhost:5297/api/tasks/${id}`, {
                title: task.title,
                isCompleted: !task.isCompleted
            });
            console.log("Task object:", task);
            // fetch again after updating task item
            var res = await axios.get(`http://localhost:5297/api/tasks/uid/${uid}`);
            setTasks(res.data);
        } catch (err) {
            console.error("Failed while updating task status:", err);
        }
    }

    const updateTaskText = async (id, newText) => {
        const task = tasks.find(t => t.id === id);
        try {
            await axios.put(`http://localhost:5297/api/tasks/${id}`, {
                title: newText,
                isCompleted: task.isCompleted
            });
            // fetch again after updating task item
            var res = await axios.get(`http://localhost:5297/api/tasks/uid/${uid}`);
            setTasks(res.data);
        } catch (err) {
            console.error("Failed while updating task title:", err);
        }
    }

    const deleteTask = async (id) => {
        try {
            await axios.delete(`http://localhost:5297/api/tasks/${id}`);
            // fetch again after deleting task 
            var res = await axios.get(`http://localhost:5297/api/tasks/uid/${uid}`);
            setTasks(res.data);
        } catch (err) {
            console.error("Failed while deleting task:", err);
        }
    }

    const getActiveTasksCount = () => {
        const activeTasks = tasks.filter(task => task.isCompleted === false);
        return activeTasks.length;
    }

    const getCompletedTasksCount = () => {
        const completedTasks = tasks.filter(task => task.isCompleted === true);
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
            <TodoList tasks={tasks} changeTaskCompletion={changeTaskCompletion} deleteTask={deleteTask} updateTaskText={updateTaskText}/>
        </div>
    )
}

export default TodoPage;