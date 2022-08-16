public interface ITodoClientData
{
    [Get("/Task")]
    Task<List<Todo>> GetAllTasks();

    [Get("/Task/{id}")]
    Task<Todo> GetTaskById(string id);

    [Get("/Task/Employee/{EmployeeId}")]
    Task<List<Todo>> GetEmployeeTasks(string employeeId);

    [Get("/Task/Employee/Count/{EmployeeId}")]
    Task<int> GetEmployeeTasksCount(string employeeId);

    [Post("/Task")]
    Task<Todo> InsertTask(TodoDto todoDto);

    [Put("/Task")]
    Task<Todo> UpdateTask(TodoDto todoDto);

    [Delete("/Task")]
    Task<Todo> DeleteTask(string id);
}


