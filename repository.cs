using System;
using System.IO;
using Newtonsoft.Json;

public interface IRepository
{
    void Save(string data);
}

public class FileRepository : IRepository
{
    private readonly string filename;

    public FileRepository(string filename)
    {
        this.filename = filename;
    }

    public void Save(string data)
    {
        File.WriteAllText(filename, data + Environment.NewLine);
    }
}

public class JsonRepository : IRepository
{
    private readonly string filename;

    public JsonRepository(string filename)
    {
        this.filename = filename;
    }

    public void Save(string data)
    {
        var jsonFormatted = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(filename, jsonFormatted + Environment.NewLine);
    }
}

public class MyLogger
{
    private readonly IRepository[] repositories;

    public MyLogger(params IRepository[] repositories)
    {
        this.repositories = repositories;
    }

    public void Log(string message)
    {
        foreach (var repository in repositories)
        {
            repository.Save(message);
        }
    }
}

class lab152
{
    static void Main()
    {
        var fileRepo = new FileRepository("lab152/log.txt");
        var jsonRepo = new JsonRepository("lab152/log.json");

        var logger = new MyLogger(fileRepo, jsonRepo);

        Console.Write("Enter a message for the log:");
        string logMessage = Console.ReadLine();

        logger.Log(logMessage);

        Console.WriteLine("The log is written to files");
    }
}
