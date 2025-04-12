import { useState } from "react";
import TodoList from "../components/ToDoList";
import Filter from "../components/Filter";

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

    const filteredTasks = tasks.filter(task => {
        if(filter === 'Active') return !task.completed;
        if(filter === 'Completed') return task.completed;
        return true;
    })

    return(
        <div>
            <h1>To-Do Application</h1>
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