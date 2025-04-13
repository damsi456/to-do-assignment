import { useState } from "react";

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
                    <button onClick={handleSave} className="save-btn">Save</button>
                    <button onClick={handleCancel} className="danger-btn">Close</button>
                </>
            : 
                <>
                <span style={{textDecoration: task.completed ? "line-through" : 'none',
                    opacity: task.completed ? 0.3 : 1}}
                onClick={() => setIsEditing(true)}>{task.text}</span> {/*should click to edit*/}
                <button onClick={() => deleteTask(task.id)} className="danger-btn">Delete</button>
                </>
            
        }
    </li>
    );
}

export default TodoItem;