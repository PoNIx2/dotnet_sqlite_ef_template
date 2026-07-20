namespace dotnet_sqlite_ef_template.Models;

public class TodoItem
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public bool IsDone { get; set; }
}