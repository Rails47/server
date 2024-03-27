using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPAddress ipAd = IPAddress.Parse("192.168.100.3");
            TcpListener myList = new TcpListener(ipAd, 8001);

            myList.Start();

            Console.WriteLine("Сервер запущено на порту 8001...");
            Console.WriteLine("Локальна адреса: " + myList.LocalEndpoint);

            Socket s = myList.AcceptSocket();
            Console.WriteLine("Підключення встановлено з " + s.RemoteEndPoint);

            byte[] b = new byte[100];
            int k = s.Receive(b);
            Console.WriteLine("Отримано рядок від клієнта:");
            Console.WriteLine(Encoding.UTF8.GetString(b, 0, k));

            ASCIIEncoding asen = new ASCIIEncoding();
            s.Send(asen.GetBytes("Поточний час: " + DateTime.Now.ToString()));

            s.Close();
            myList.Stop();
        }
    }
}
