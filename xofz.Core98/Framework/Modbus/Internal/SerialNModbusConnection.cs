namespace xofz.Framework.Modbus.Internal
{
    using System;
    using System.IO.Ports;
    using global::Modbus.Device;
    using xofz.Internal;

    internal sealed class SerialNModbusConnection : IDisposable
    {
        static SerialNModbusConnection()
        {
            AppDomain.CurrentDomain.AssemblyResolve 
                += (sender, e) => EmbeddedAssemblyLoader.Load(e.Name);
        }

        public SerialNModbusConnection(
            SerialPort port, 
            SerialMode mode)
        {
            if (!port.IsOpen)
            {
                port.Open();
            }

            this.connection = mode == SerialMode.ASCII
                ? ModbusSerialMaster.CreateAscii(port)
                : ModbusSerialMaster.CreateRtu(port);
        }

        public ushort[] ReadHoldingRegisters(
            ushort startAddress, 
            ushort numberOfRegisters)
        {
            return this.connection.ReadHoldingRegisters(
                1, 
                startAddress, 
                numberOfRegisters);
        }

        public ushort[] ReadInputRegisters(
            ushort startAddress, 
            ushort numberOfRegisters)
        {
            return this.connection.ReadInputRegisters(
                1, 
                startAddress, 
                numberOfRegisters);
        }

        public bool[] ReadCoils(
            ushort startAddress, 
            ushort numberOfCoils)
        {
            return this.connection.ReadCoils(
                1, 
                startAddress, 
                numberOfCoils);
        }

        public bool[] ReadInputs(
            ushort startAddress, 
            ushort numberOfInputs)
        {
            return this.connection.ReadInputs(
                1, 
                startAddress, 
                numberOfInputs);
        }

        public void WriteSingleCoil(
            ushort address, 
            bool value)
        {
            this.connection.WriteSingleCoil(
                1, 
                address, 
                value);
        }

        public void WriteMultipleCoils(
            ushort startAddress, 
            bool[] values)
        {
            this.connection.WriteMultipleCoils(
                1, 
                startAddress, 
                values);
        }

        public void WriteSingleHoldingRegister(
            ushort address, 
            ushort value)
        {
            this.connection.WriteSingleRegister(
                1, 
                address, 
                value);
        }

        public void WriteMultipleHoldingRegisters(
            ushort address, 
            ushort[] values)
        {
            this.connection.WriteMultipleRegisters(
                1, 
                address, 
                values);
        }

        public void Dispose()
        {
            this.connection?.Dispose();
        }

        private readonly ModbusSerialMaster connection;
    }
}
