using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace serv
{
    public class SocketHandler
    {
        Socket _s;
        ArraySegment<byte> _seg;
        public SocketHandler(Socket s)
        {
          _s = s;  
          _seg = new ArraySegment<byte>(new byte[4]);
        }
        public async Task BeginReceive()
        {
            int count = 0;
            do
            {
                count = await _s.ReceiveAsync(_seg,SocketFlags.None);
            } while(count>0);
        }

        public async Task SendMessage(string v)
        {
            byte [] buff = System.Text.UTF8Encoding.UTF8.GetBytes(v);
            ArraySegment<byte> seg = new ArraySegment<byte>(buff);
            await _s.SendAsync(seg,SocketFlags.None);
        }
    }
}