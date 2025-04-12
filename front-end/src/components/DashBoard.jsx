function DashBoard({ getActiveTasksCount, getCompletedTasksCount }){
    return(
        <div className="dashboard">
            <p>{getActiveTasksCount()}</p>
            <p>{getCompletedTasksCount()}</p>
        </div>
    )
}

export default DashBoard;