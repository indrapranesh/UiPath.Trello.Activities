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

    public class CreateList : TrelloActivity
    {
        public string boardidinput;
        System.Action CreateListDelegate;


        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> BoardId { get; set; }

        [Category("Output")]
        public OutArgument<string> Token { get; set; }

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, AsyncCallback callback, object state)
        {
            LoadAuthentication(context);
            boardidinput = BoardId.Get(context);

            CreateListDelegate = new System.Action(_CreateLists);

            return CreateListDelegate.BeginInvoke(callback, state);



            /*  var board = new Board(boardid, auth);
              //board.Name = "UiPath Board";
              board.Lists.Add("Uiboard");
              board.Lists.Refresh();
              var list = board.Lists.First();
              list.Cards.Add("UIpath card");
              */

            // ITrelloFactory factory = new TrelloFactory();
        }




        void _CreateLists()
        {
            SendCreateList(boardidinput).Wait();
        }
        protected override void EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            CreateListDelegate.EndInvoke(result);
        }
    }
}
