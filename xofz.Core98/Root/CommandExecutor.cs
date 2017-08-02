namespace xofz.Root
{
    using System.Collections.Generic;

    public class CommandExecutor
    {
        public CommandExecutor()
        {
            this.executedCommands = new List<Command>(0x1000);
        }

        public virtual T Get<T>() where T : Command
        {
            foreach (var command in this.executedCommands)
            {
                if (command is T)
                {
                    return (T)command;
                }
            }

            return default(T);
        }

        public virtual CommandExecutor Execute(Command command)
        {
            command.Execute();
            this.executedCommands.Add(command);
            return this;
        }

        private readonly IList<Command> executedCommands;
    }
}
