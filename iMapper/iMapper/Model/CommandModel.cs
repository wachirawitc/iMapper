using System;

namespace iMapper.Model
{
    public class CommandModel
    {
        public int CommandId { get; set; }
        public Guid MenuGroup { get; set; }
        public EventHandler Event { get; set; }

        public CommandModel(int commandId, Guid menuGroup, EventHandler eventHandler)
        {
            CommandId = commandId;
            MenuGroup = menuGroup;
            Event = eventHandler;
        }
    }
}