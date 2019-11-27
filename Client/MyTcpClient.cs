using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Serilog;
using Serilog.Events;

namespace Client
{
    public class MyTcpClient
    {
        private readonly ILogger _logger;

        private TcpClient tcpClient = null;
        private NetworkStream tcpStream = null;
        private const int port = 59567;

        private static ILogger CreateLogger()
        {
            return new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();
        }

        public MyTcpClient() : this(CreateLogger()) { }

        public MyTcpClient(ILogger logger)
        {
            _logger = logger.ForContext<MyTcpClient>();
        }

        public void Connect()
        {
            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(IPAddress.Loopback, port);
                Log($"Connect: Starting TCP clients on port {port}...");
            }
            catch (Exception e)
            {
                Log(LogEventLevel.Error, $"Connect exception: {e}");
            }
        }

        public void Send(string message)
        {
            try
            {
                var buffer = Encoding.ASCII.GetBytes(message);

                if (tcpStream == null)
                    tcpStream = tcpClient.GetStream();

                tcpStream.Write(buffer, 0, buffer.Length);
                Log(LogEventLevel.Debug, $"Send: {message}");
            }
            catch (Exception e)
            {
                Log(LogEventLevel.Error, $"Send exception: {e}");
            }
        }

        public void Close()
        {
            try
            {
                Log(LogEventLevel.Debug, "Close: Closes the connection and releases all associated resources.");
                tcpClient.Close();
                tcpStream = null;
                tcpClient = null;
            }
            catch (Exception e)
            {
                Log(LogEventLevel.Error, $"Close exception: {e}");
            }
        }

        private void Log(string message)
        {
            _logger.Information(message);
        }

        private void Log(LogEventLevel level, string message)
        {
            _logger.Write(level, message);
        }
    }
}