function Filter({filter, setFilter}){
    return(
        <div className="filter-buttons">
            <button onClick={() => setFilter("All")} disabled={filter === 'All'}>All</button>
            <button onClick={() => setFilter("Active")} disabled={filter === 'Active'}>Active</button>
            <button onClick={() => setFilter("Completed")} disabled={filter === 'Completed'}>Completed</button>
        </div>
    )
}

export default Filter;
