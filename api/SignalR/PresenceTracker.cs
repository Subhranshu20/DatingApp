using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.SignalR
{
    public class PresenceTracker
    {
        private static readonly Dictionary<string,List<string>> OnLineUsers=new Dictionary<string, List<string>>();
        public Task<bool> UserConnected(string username , string connectionID)
        {
            bool isOnine=false;
            lock (OnLineUsers)
            {
                if(OnLineUsers.ContainsKey(username))
                {
                    OnLineUsers[username].Add(connectionID);
                }
                else{
                    OnLineUsers.Add(username,new List<string>{connectionID});
                    isOnine=true;
                }
            }
            // return Task.CompletedTask;
             return Task.FromResult(isOnine);
        }
        public Task<bool> UserDisconnected(string username , string connectionID)
        {
            bool isOffine=false;
            lock (OnLineUsers)
            {
                if(!OnLineUsers.ContainsKey(username)) return Task.FromResult(isOffine);
                OnLineUsers[username].Remove(connectionID);
                if (OnLineUsers[username].Count == 0)
                {
                    OnLineUsers.Remove(username);
                    isOffine=true;
                }
                
            }
            return Task.FromResult(isOffine);
        }
        public Task<string[]> GetOnlineUsers()
        {
            string[] onlineUsers;
            lock (OnLineUsers)
            {
                onlineUsers=OnLineUsers.OrderBy( x => x.Key).Select(k => k.Key).ToArray();
            }
            return Task.FromResult(onlineUsers);

        }
        public Task<List<string>> GetConnectionsForUser(string username)
        {
            List<string> connectionIds;
            lock(OnLineUsers)
            {
                 connectionIds=OnLineUsers.GetValueOrDefault(username);
            }
            return Task.FromResult(connectionIds);
        }
    }
}