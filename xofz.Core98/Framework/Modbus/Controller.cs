namespace xofz.Framework.Modbus
{
    public interface Controller
    {
        string Location { get; set; }

        int SecondaryLocation { get; set; }

        ushort[] ReadHoldingRegisters(
            ushort startAddress, 
            ushort numberOfRegisters);

        ushort[] ReadInputRegisters(
            ushort startAddress, 
            ushort numberOfRegisters);

        bool[] ReadCoils(
            ushort startAddress, 
            ushort numberOfCoils);

        bool[] ReadInputs(
            ushort startAddress, 
            ushort numberOfInputs);

        void WriteSingleCoil(
            ushort address, 
            bool value);

        void WriteMultipleCoils(
            ushort startAddress, 
            MaterializedEnumerable<bool> values);

        void WriteSingleHoldingRegister(
            ushort address, 
            ushort value);

        void WriteMultipleHoldingRegisters(
            ushort startAddress, 
            MaterializedEnumerable<ushort> values);
    }
}
