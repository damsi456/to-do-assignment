import TodoItem from './TodoItem.jsx';

function TodoList({tasks, changeTaskCompletion, deleteTask, updateTaskText}){
    return (
        <div className="todo-list">
            <h2>My Tasks</h2>
            <ul>
                {tasks.map(task => (
                    <TodoItem
                    key={task.id}
                    task={task}
                    changeTaskCompletion={changeTaskCompletion}
                    deleteTask={deleteTask}
                    updateTaskText={updateTaskText}
                    />
                ))}
            </ul>
        </div>
    )
}

export default TodoList;