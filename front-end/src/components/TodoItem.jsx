import { useState } from "react";

function TodoItem({task, changeTaskCompletion, deleteTask, updateTaskText}){
    const [isEditing, setIsEditing] = useState(false);
    const [newText, setNewText] = useState(task.title);

    const handleSave = () => {
        updateTaskText(task.id, newText);
        setIsEditing(false);
    }

    const handleCancel = () => {
        setNewText(task.title);
        setIsEditing(false);
    }
    
    return(
    <li>
        <input 
            type="checkbox"
            checked={task.isCompleted}
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
                <span style={{textDecoration: task.isCompleted ? "line-through" : 'none',
                    opacity: task.isCompleted ? 0.3 : 1}}
                onClick={() => setIsEditing(true)}>{task.title}</span> {/*should click to edit*/}
                <button onClick={() => deleteTask(task.id)} className="danger-btn">Delete</button>
                </>
            
        }
    </li>
    );
}

export default TodoItem;