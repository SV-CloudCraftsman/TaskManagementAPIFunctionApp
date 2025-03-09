using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace TaskManagementAPI
{
    public class TasksController
    {
        /// <summary>  
        /// List of tasks.  
        /// </summary> 
        public static List<Tasks> Tasks = new List<Tasks>();
        
        /// <summary>  
        /// Task ID counter.  
        /// </summary> 
        public static int Id = 1;
        private readonly ILogger<TasksController> _logger;

        /// <summary>  
        /// Initializes a new instance of the <see cref="TasksController"/> class.  
        /// </summary>  
        /// <param name="logger">The logger instance.</param> 
        public TasksController(ILogger<TasksController> logger)
        {
            _logger = logger;
        }

        /// <summary>  
        /// Creates a new task.  
        /// </summary>  
        /// <param name="req">The HTTP request data.</param>  
        /// <param name="context">The function context.</param>  
        /// <returns>The created task.</returns>  
        [Function("CreateTask")]
        public static async Task<IActionResult> CreateTask([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "tasks")] HttpRequestData req, FunctionContext context)
        {
            var task = await req.ReadFromJsonAsync<Tasks>();
            task.Id = Id++;
            Tasks.Add(task);
            return new OkObjectResult(task);
        }

        /// <summary>  
        /// Gets all tasks.  
        /// </summary>  
        /// <param name="req">The HTTP request data.</param>  
        /// <param name="context">The function context.</param>  
        /// <returns>The list of tasks.</returns>  
        [Function("GetAllTasks")]
        public static async Task<IActionResult> GetAllTasks([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tasks")] HttpRequestData req, FunctionContext context)
        {
            //var task = await req.ReadFromJsonAsync<Tasks>();
            return new OkObjectResult(Tasks);
        }

        /// <summary>  
        /// Gets a task by ID.  
        /// </summary>  
        /// <param name="req">The HTTP request data.</param>  
        /// <param name="id">The task ID.</param>  
        /// <param name="context">The function context.</param>  
        /// <returns>The task with the specified ID.</returns> 
        [Function("GetTaskById")]
        public static async Task<IActionResult> GetTaskById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tasks/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            var task = Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(task);
        }

        /// <summary>  
        /// Updates a task.  
        /// </summary>  
        /// <param name="req">The HTTP request data.</param>  
        /// <param name="id">The task ID.</param>  
        /// <param name="context">The function context.</param>  
        /// <returns>The updated task.</returns>  
        [Function("UpdateTaskById")]
        public static async Task<IActionResult> UpdateTaskById([HttpTrigger(AuthorizationLevel.Anonymous,"put", Route = "tasks/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            var task= await req.ReadFromJsonAsync<Tasks>();
            var existingTask = Tasks.FirstOrDefault(x => x.Id == id);
            if(existingTask==null)
            {
                return new NotFoundResult();
            }
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.IsCompleted = task.IsCompleted;
            return new OkObjectResult(existingTask);
        }

        /// <summary>  
        /// Deletes a task by ID.  
        /// </summary>  
        /// <param name="req">The HTTP request data.</param>  
        /// <param name="id">The task ID.</param>  
        /// <param name="context">The function context.</param>  
        /// <returns>The deleted task.</returns> 
        [Function("DeleteTaskById")]
        public static async Task<IActionResult> DeleteTaskById([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "tasks/{id}")] HttpRequestData req, int id, FunctionContext context)
        {
            var task = Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                return new NotFoundResult();
            }
            Tasks.Remove(task);
            return new OkObjectResult(task);
        }
    }
}
