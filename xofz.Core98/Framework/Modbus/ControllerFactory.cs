namespace xofz.Framework.Modbus
{
    using System.IO.Ports;
    using xofz.Framework.Modbus.Internal;

    public class ControllerFactory
    {
        public virtual Controller NewTcpController(
            string hostnameOrIP,
            int port = 502)
        {
            return new TcpController
            {
                Location = hostnameOrIP,
                SecondaryLocation = port
            };
        }

        public virtual Controller NewSerialController(
            SerialPort port,
            SerialMode mode)
        {
            return new SerialController(
                port,
                mode);
        }
    }
}
