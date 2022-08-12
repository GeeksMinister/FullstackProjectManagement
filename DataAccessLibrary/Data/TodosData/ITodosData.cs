public interface ITodoData
{
    Task<IEnumerable<Todo>> GetAllTasks();
    Task<IEnumerable<Todo>> GetEmployeeTasks(string emp_Id);
    Task<int> GetEmployeeTasksCount(string em_Id);
}