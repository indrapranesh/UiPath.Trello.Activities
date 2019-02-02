using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using Manatee.Trello;


namespace Trello.Activities
{
    public class TrelloAuth : AsyncNativeActivity
    {
        [Category("Authentication")]
        [RequiredArgument]
        [Description("AppKey for Trello")]
        [DisplayName("AppKey")]
        public InArgument<string> AppKey { get; set; }

        [Category("Authentication")]
        [RequiredArgument]
        [Description("UserToken for Trello")]
        [DisplayName("UserToken")]
        public InArgument<string> UserToken { get; set; }

        [Browsable(false)]
        public ActivityAction<AuthenticationAgent> Body { get; set; }

        AuthenticationAgent authAgent;

        System.Action AuthenticateAsyncDelegate;

        public TrelloAuth()
        {
            Body = new ActivityAction<AuthenticationAgent>
            {
                Argument = new DelegateInArgument<AuthenticationAgent>("authAgent"),
                Handler = new Sequence
                {
                    DisplayName = "Trello Activities"
                }
            };
        }

        protected override IAsyncResult BeginExecute(NativeActivityContext context, AsyncCallback callback, object state)
        {
            string appKey = AppKey.Get(context);
            string userToken = UserToken.Get(context);

            authAgent = new AuthenticationAgent(appKey, userToken);

            if (Body != null)
            {
                context.ScheduleAction<AuthenticationAgent>(Body, authAgent, OnCompleted);
            }

            AuthenticateAsyncDelegate = new System.Action(AuthenticateAsync);
            return AuthenticateAsyncDelegate.BeginInvoke(callback, state);

        }

        void AuthenticateAsync()
        {

        }


        protected override void EndExecute(NativeActivityContext context, IAsyncResult result)
        {
            AuthenticateAsyncDelegate.EndInvoke(result);
        }
        void OnCompleted(NativeActivityContext context,
            ActivityInstance completedInstance)
        {
            // authAgent.reset
        }





    }
}
