import { use, useState } from "react";

function TodoItem({task, changeTaskCompletion, deleteTask, updateTaskText}){
    const [isEditing, setIsEditing] = useState(false);
    const [newText, setNewText] = useState(task.text);

    const handleSave = () => {
        updateTaskText(task.id, newText);
        setIsEditing(false);
    }

    const handleCancel = () => {
        setNewText(task.text);
        setIsEditing(false);
    }
    
    return(
    <li>
        <input 
            type="checkbox"
            checked={task.completed}
            onChange={() => changeTaskCompletion(task.id)} 
        />{
            isEditing ? 
                <>
                    <input type="text" value={newText} onChange={(e) => setNewText(e.target.value)}/>
                    <button onClick={handleSave}>Save</button>
                    <button onClick={handleCancel}>Close</button>
                </>
            : 
                <>
                <span style={{textDecoration: task.completed ? "line-through" : 'none'}}
                onClick={() => setIsEditing(true)}>{task.text}</span> {/*should click to edit*/}
                <button onClick={() => deleteTask(task.id)}>DELETE</button>
                </>
            
        }
    </li>
    );
}

export default TodoItem;