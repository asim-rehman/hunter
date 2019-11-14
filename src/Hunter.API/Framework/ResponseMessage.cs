namespace Hunter.API.Framework
{
    public sealed class ResponseMessage
    {

        public static string Http500 { get; } = "500 Internal Server Error";
        public static string ItemNotFound { get; } = "Item Not Found";
        public static string ValidationErrors { get; } = "One or More Validation Errors Occurred";
        public static string ChangesSaved { get; } = "Changes Saved";
        public static string DeletedSuccessfully { get; } = "Deleted Successfully";

    }
}
