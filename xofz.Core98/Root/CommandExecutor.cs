namespace xofz.Root
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class CommandExecutor
    {
        public CommandExecutor()
            : this(
                null)
        {
        }

        protected CommandExecutor(
            object locker)
            : this(
                null, 
                locker)
        {
        }

        protected CommandExecutor(
            ICollection<Command> executedCommands,
            object locker = null)
        {
            this.executedCommands = executedCommands
                ?? new XLinkedList<Command>();
            this.locker = locker
                ?? new object();
        }

        public virtual T Get<T>()
            where T : Command
        {
            lock (this.locker)
            {
                foreach (var command in this.executedCommands)
                {
                    if (command is T t)
                    {
                        return t;
                    }
                }
            }

            return default;
        }

        public virtual Lot<T> GetAll<T>()
            where T : Command
        {
            var commands = new XLinkedListLot<T>();
            lock (this.locker)
            {
                foreach (var command in this.executedCommands)
                {
                    if (command is T t)
                    {
                        commands.AddTail(t);
                    }
                }
            }

            return commands;
        }

        public virtual CommandExecutor Execute(
            Command command)
        {
            if (command == null)
            {
                return this;
            }

            command.Execute();
            lock (this.locker)
            {
                this.executedCommands.Add(command);
            }

            return this;
        }

        protected readonly ICollection<Command> executedCommands;
        protected readonly object locker;
    }
}
