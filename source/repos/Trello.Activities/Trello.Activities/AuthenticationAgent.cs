using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using Manatee.Trello;


namespace Trello.Activities
{
    public class AuthenticationAgent
    {
        private string AppKey;
        private string UserToken;

        public readonly TrelloAuthorization trelloAuthorization;

        public AuthenticationAgent(string AppKey, string UserToken)
        {
            this.AppKey = AppKey;
            this.UserToken = UserToken;

            trelloAuthorization = new TrelloAuthorization
            {
                AppKey = this.AppKey,
                UserToken = this.UserToken
            };
        }

    }
}
