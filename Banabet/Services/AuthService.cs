using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banabet.Services
{
    public class AuthService
    {
        
        private const string AuthStateKey = "AuthState";
        public async Task<bool> IsAutheticated()
        {
            await Task.Delay(500);

            var authState = Preferences.Default.Get<bool>(AuthStateKey, false);

            return authState;
        }

        public void Login()
        {
            DatabaseManager.mvm.FetchData();
            Preferences.Default.Set<bool>(AuthStateKey, true);
        }
        public void Logout()
        {
            DatabaseManager.ClearCurrentSession();
            Preferences.Default.Remove(AuthStateKey);
        }
    }
}
