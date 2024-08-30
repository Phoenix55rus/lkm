using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    // Импорт функции для клика мышью из Windows API
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

    private const int MOUSEEVENTF_LEFTDOWN = 0x02;
    private const int MOUSEEVENTF_LEFTUP = 0x04;

    static void Main(string[] args)
    {
        // Имя файла, откуда будет считываться время
        string timeFilePath = "time.txt";

        // Считывание времени из файла
        TimeSpan targetTime;
        try
        {
            string timeFromFile = File.ReadAllText(timeFilePath).Trim();
            targetTime = TimeSpan.Parse(timeFromFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при чтении или парсинге времени: " + ex.Message);
            return;
        }

        while (true)
        {
            // Текущее время
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            // Проверка времени
            if (currentTime >= targetTime)
            {
                // Клик мышью
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                break;
            }

            // Ожидание 10 секунд перед следующей проверкой
            Thread.Sleep(10000);
        }
    }
}
