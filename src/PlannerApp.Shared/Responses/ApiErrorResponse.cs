namespace PlannerApp.Shared.Responses
{
    public class ApiErrorResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
        public Userinfo UserInfo { get; set; }
        public DateTime ExpireDate { get; set; }
    }

}
