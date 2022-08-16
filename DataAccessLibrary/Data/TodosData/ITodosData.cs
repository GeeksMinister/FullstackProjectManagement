public interface ITodoData
{
    Task DeleteTask(string Id);
    Task<IEnumerable<Todo>> GetAllTasks();
    Task<IEnumerable<Todo>> GetEmployeeTasks(string emp_Id);
    Task<int> GetEmployeeTasksCount(string em_Id);
    Task<Todo> GetTaskById(string id);
    Task<Todo> InsertTask(Todo todo);
    Task UpdateTask(Todo todo);
}