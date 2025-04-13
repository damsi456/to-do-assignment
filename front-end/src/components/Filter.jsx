function Filter({filter, setFilter}){
    return(
        <div className="filter-buttons">
            <button onClick={() => setFilter("All")} className={filter === 'All' ? 'active' : ''}>All</button>
            <button onClick={() => setFilter("Active")} className={filter === 'Active' ? 'active' : ''}>Active</button>
            <button onClick={() => setFilter("Completed")} className
            ={filter === 'Completed' ? 'active' : ''}>Completed</button>
        </div>
    )
}

export default Filter;
