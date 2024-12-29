public class SingleRandomizer
{
    private static SingleRandomizer instance;
    private static readonly object lockObject = new object();
    private Random random;
    private string filePath;

    private SingleRandomizer()
    {
        random = new Random();
        filePath = "15_1.txt";
        CreateNewFile();
    }

    private void CreateNewFile()
    {
        using (StreamWriter writer = File.CreateText(filePath))
        {
        }
    }

    public static SingleRandomizer Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new SingleRandomizer();
                    }
                }
            }
            return instance;
        }
    }

    public int GetNextRandomNumber()
    {
        lock (lockObject)
        {
            int randomNumber = random.Next();

            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(randomNumber);
            }

            return randomNumber;
        }
    }
}

class lab153
{
    static void Main()
    {
        var randomizer = SingleRandomizer.Instance;

        Parallel.For(0, 10, i =>
        {
            int randomNumber = randomizer.GetNextRandomNumber();
            Console.WriteLine($"Thread {Task.CurrentId}: {randomNumber}");
        });

        Console.WriteLine("15_1.txt");
    }
}
