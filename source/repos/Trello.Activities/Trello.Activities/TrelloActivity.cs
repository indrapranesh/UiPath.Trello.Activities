using System;
using System.Activities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Manatee.Trello;

namespace Trello.Activities
{


    public abstract class TrelloActivity : AsyncCodeActivity
    {
        protected AuthenticationAgent authAgent;

        protected void LoadAuthentication(AsyncCodeActivityContext context)
        {
            var property = context.DataContext.GetProperties()["authAgent"];
            if (property.GetValue(context.DataContext) == null)
            {
                throw new Exception("DocuSign activities must be within DocuSign Context activity");
            }
            authAgent = (AuthenticationAgent)property.GetValue(context.DataContext);
        }

        protected async Task SendCreateList(string boardid)
        {
            var board = new Board(boardid, authAgent.trelloAuthorization);
            board.Name = "UiPath Board";
            await board.Lists.Add("Testboard");
            await board.Lists.Refresh();

            var list = new List("5b89145a0f375f8c9b740ceb", authAgent.trelloAuthorization);
            var card = await list.Cards.Add("FirstCard");
          /*  card.Name = "Renamed Card";
            card.Description = "THis is the first card";
            await card.CheckLists.Add("checklist");
            await list.Cards.Refresh();
            var card2 = await list.Cards.Add("SecondCard");
            await card2.Delete();
            */
        }

        /*protected async Task SendRestRequest(DocusignResponse restResponse, HttpMethod method, string path, object body, Dictionary<string, string> query = null)
        {
            HttpResponseMessage response = await HttpAgent.SendRestRequest(authAgent, method, path, body, query, true);
            string responseContent = await response.Content.ReadAsStringAsync();
            restResponse.Initialize(response, responseContent);
            if (restResponse.NeedsRefresh)
            {
                authAgent.RefreshAuthToken().Wait();
                response = await HttpAgent.SendRestRequest(authAgent, method, path, body);
                responseContent = await response.Content.ReadAsStringAsync();
                restResponse.Initialize(response, responseContent);
            }
        }*/
    }
}