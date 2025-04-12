function DashBoard({ getActiveTasksCount, getCompletedTasksCount }){
    return(
        <div className="dashboard">
            <p><b>No. of Active Tasks: </b>{getActiveTasksCount()}</p>
            <p><b>No. of Completed Tasks: </b>{getCompletedTasksCount()}</p>
        </div>
    )
}

export default DashBoard;