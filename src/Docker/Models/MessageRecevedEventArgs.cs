using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docker.Models
{
    public class MessageRecevedEventArgs : EventArgs
    {
        public Message? Message;

        public MessageRecevedEventArgs(Message? message)
        {
            Message = message;
        }
    }
}
