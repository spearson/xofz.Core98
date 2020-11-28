namespace xofz.Root
{
    using System.Collections.Generic;
    using xofz.Framework.Lots;

    public class CommandExecutor
    {
        public CommandExecutor()
            : this(
                new LinkedList<Command>(), 
                new object())
        {
        }

        protected CommandExecutor(
            ICollection<Command> executedCommands)
            : this(
                executedCommands, 
                new object())
        {
        }

        protected CommandExecutor(
            object locker)
            : this(
                new LinkedList<Command>(), 
                locker)
        {
        }

        protected CommandExecutor(
            ICollection<Command> executedCommands,
            object locker)
        {
            this.executedCommands = executedCommands
                ?? new LinkedList<Command>();
            this.locker = locker
                ?? new object();
        }

        public virtual T Get<T>()
            where T : Command
        {
            lock (this.locker)
            {
                foreach (var command in this.executedCommands
                                        ?? EnumerableHelpers.Empty<Command>())
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
            var commands = new LinkedListLot<T>();
            lock (this.locker)
            {
                foreach (var command in this.executedCommands
                                        ?? EnumerableHelpers.Empty<Command>())
                {
                    if (command is T t)
                    {
                        commands.AddLast(t);
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
                this.executedCommands?.Add(command);
            }

            return this;
        }

        protected readonly ICollection<Command> executedCommands;
        protected readonly object locker;
    }
}
