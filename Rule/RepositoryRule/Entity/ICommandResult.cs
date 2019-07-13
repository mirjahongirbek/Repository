namespace RepositoryRule.Entity
{
    public interface ICommandResult
    {
        bool Success { get; }
        string ErrorText { get; set; }
    }
}