import { useState } from "react";
import TodoList from "../components/ToDoList";
import Filter from "../components/Filter";
import DashBoard from "../components/DashBoard";

function TodoPage(){
    const [tasks, setTasks] = useState([]);
    const [filter, setFilter] = useState('All');

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
        <div>
            <h1>To-Do Application</h1>
            <DashBoard getActiveTasksCount={getActiveTasksCount} getCompletedTasksCount={getCompletedTasksCount}/>
            <Filter filter={filter} setFilter={setFilter}/>
            <form onSubmit={addTask} className="add-todo-form">
                <input type="text" name="taskInput" className="add-todo" placeholder="Add a new task"/>
                <button type="submit">Add</button>
            </form>
            <TodoList tasks={filteredTasks} changeTaskCompletion={changeTaskCompletion} deleteTask={deleteTask} updateTaskText={updateTaskText}/>
        </div>
    )
}

export default TodoPage;