function TodoItem({task, changeTaskCompletion, deleteTask}){
    return(
    <li>
        <input 
            type="checkbox"
            checked={task.completed}
            onChange={() => changeTaskCompletion(task.id)} 
        />
        <span style={{textDecoration: task.completed ? "line-through" : 'none'}}>{task.text}</span>
        <button onClick={() => deleteTask(task.id)}>DELETE</button>
    </li>
    );
}

export default TodoItem;