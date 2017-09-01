namespace xofz.Framework.Modbus.Internal
{
    internal sealed class TcpController : Controller
    {
        public string Location { get; set; }

        public int SecondaryLocation { get; set; }

        ushort[] Controller.ReadHoldingRegisters(
            ushort startAddress, 
            ushort numberOfRegisters)
        {
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation))
            {
                return connection.ReadHoldingRegisters(
                    startAddress, 
                    numberOfRegisters);
            }
        }

        ushort[] Controller.ReadInputRegisters(
            ushort startAddress, 
            ushort numberOfRegisters)
        {
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation))
            {
                return connection.ReadInputRegisters(
                    startAddress,
                    numberOfRegisters);
            }
        }

        bool[] Controller.ReadCoils(
            ushort startAddress, 
            ushort numberOfCoils)
        {
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation))
            {
                return connection.ReadCoils(
                    startAddress,
                    numberOfCoils);
            }
        }

        bool[] Controller.ReadInputs(
            ushort startAddress, 
            ushort numberOfInputs)
        {
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation))
            {
                return connection.ReadInputs(
                    startAddress,
                    numberOfInputs);
            }
        }

        void Controller.WriteSingleCoil(
            ushort address, 
            bool value)
        {
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation))
            {
                connection.WriteSingleCoil(
                    address,
                    value);
            }
        }

        void Controller.WriteMultipleCoils(
            ushort startAddress,
            MaterializedEnumerable<bool> values)
        {
            var array = MEHelpers.ToArray(
                values);
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation))
            {
                connection.WriteMultipleCoils(
                    startAddress,
                    array);
            }
        }

        void Controller.WriteSingleHoldingRegister(
            ushort address, 
            ushort value)
        {
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation))
            {
                connection.WriteSingleHoldingRegister(
                    address,
                    value);
            }
        }

        void Controller.WriteMultipleHoldingRegisters(
            ushort startAddress, 
            MaterializedEnumerable<ushort> values)
        {
            var array = MEHelpers.ToArray(
                values);
            using (var connection = new TcpNModbusConnection(
                this.Location,
                this.SecondaryLocation))
            {
                connection.WriteMultipleHoldingRegisters(
                    startAddress,
                    array);
            }
        }
    }
}
