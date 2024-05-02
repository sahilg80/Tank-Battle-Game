using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Events
{
    public class EventService
    {
        public EventController OnClickGameJoin { get; private set; }
        public EventController OnPlayerGameJoined { get; private set; }
        public EventService()
        {
            OnClickGameJoin = new EventController();
            OnPlayerGameJoined = new EventController();
        }
    }
}
