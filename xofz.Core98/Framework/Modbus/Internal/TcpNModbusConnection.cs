namespace xofz.Framework.Modbus.Internal
{
    using System;
    using System.Net.Sockets;
    using global::Modbus.Device;
    using xofz.Internal;

    internal sealed class TcpNModbusConnection : IDisposable
    {
        static TcpNModbusConnection()
        {
            AppDomain.CurrentDomain.AssemblyResolve 
                += (sender, e) => EmbeddedAssemblyLoader.Load(e.Name);
        }

        public TcpNModbusConnection(string ip, int port)
        {
            var client = new TcpClient();
            client.BeginConnect(
                    ip,
                    port,
                    null,
                    null)
                .AsyncWaitHandle.WaitOne(
                    TimeSpan.FromSeconds(2));
            if (!client.Connected)
            {
                throw new Exception("Cannot connect to " + ip + ":" + port);
            }

            this.connection = ModbusIpMaster.CreateIp(client);
        }

        public ushort[] ReadHoldingRegisters(
            ushort startAddress, 
            ushort numberOfRegisters)
        {
            return this.connection.ReadHoldingRegisters(
                startAddress, 
                numberOfRegisters);
        }

        public ushort[] ReadInputRegisters(
            ushort startAddress, 
            ushort numberOfRegisters)
        {
            return this.connection.ReadInputRegisters(
                startAddress, 
                numberOfRegisters);
        }

        public bool[] ReadCoils(
            ushort startAddress, 
            ushort numberOfCoils)
        {
            return this.connection.ReadCoils(
                startAddress, 
                numberOfCoils);
        }

        public bool[] ReadInputs(
            ushort startAddress, 
            ushort numberOfInputs)
        {
            return this.connection.ReadInputs(
                startAddress, 
                numberOfInputs);
        }

        public void WriteSingleCoil(
            ushort address, 
            bool value)
        {
            this.connection.WriteSingleCoil(
                address,
                value);
        }

        public void WriteMultipleCoils(
            ushort startAddress, 
            bool[] values)
        {
            this.connection.WriteMultipleCoils(
                startAddress, 
                values);
        }

        public void WriteSingleHoldingRegister(
            ushort address, 
            ushort value)
        {
            this.connection.WriteSingleRegister(
                address, 
                value);
        }

        public void WriteMultipleHoldingRegisters(
            ushort address, 
            ushort[] values)
        {
            this.connection.WriteMultipleRegisters(
                address, 
                values);
        }

        public void Dispose()
        {
            this.connection?.Dispose();
        }

        private readonly ModbusIpMaster connection;
    }
}
