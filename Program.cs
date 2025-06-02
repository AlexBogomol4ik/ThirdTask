using System;
using ThirdTask.Model;

namespace ThirdTask
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                new LogProcessor().Process(".\\input.log", ".\\output.txt", ".\\problems.txt");
                Console.WriteLine("Обработка успешно завершена");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadLine();
            }

            Console.ReadLine();

        }
    }
}
