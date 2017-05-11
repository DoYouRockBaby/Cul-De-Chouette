using Core.Action;
using Core.ThrowingTrigger;

namespace Core
{
    public class Rules
    {
        public virtual AbstractTrigger[] ThrowingTriggers { get; set; }
        public virtual AbstractAction[] Actions { get; set; }
    }
}
