using System;
using System.Collections.Generic;
using System.Text;

namespace Enceladus.Api.UI
{
    public class Resizer
    {
        IList<IResizableClient> clients;
        private bool isSuspended = false;
        public void Suspend()
        {
            this.isSuspended = true;
        }

        public void Release()
        {
            this.isSuspended = false;
            this.UpdateClients();
        }

        public Resizer()
        {
            this.clients = new List<IResizableClient>();
        }

        public void AddClient(IResizableClient client)
        {
            if (!this.clients.Contains(client))
            {
                this.clients.Add(client);
                client.InformResizer = this.UpdateClients;
            }

            if (this.isSuspended == false)
                this.UpdateClients();
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateClients()
        {
            // for two clients matrix looks like this:
            // _________________________________________
            // | false | false | false | false | false |
            // | false | false | false | false | false |
            // |_______________________________________|
            // |  0   |   1  |   2   |    3   |   4    |
            // |_______________________________________|
            // last row they are values which are hidden behind the column index

            var matrix = new bool[this.clients.Count, 5];

            for (int i = 0; i < clients.Count; i++)
            {
                matrix[i, 0] = true; // tiny; * tiny is always
                matrix[i, 1] = clients[i].MaximalExpectedFontSize >= 2; // small
                matrix[i, 2] = clients[i].MaximalExpectedFontSize >= 3; // normal
                matrix[i, 3] = clients[i].MaximalExpectedFontSize >= 4; // big
                matrix[i, 4] = clients[i].MaximalExpectedFontSize >= 5; // huge
            }

            int interceptionIndex = this.GetInterceptionIndex(matrix, 5, this.clients.Count); 

            foreach (IResizableClient client in clients)
            {
                // we need to +1 because the range is from 1 to 5, and we deal with index only
                client.ApplyFontSize(interceptionIndex + 1);
            }
        }

        private int GetInterceptionIndex(bool[,] matrix, int columnsNumber, int rowsNumber)
        {
            int interceptionIndex = 0; 

            for (int j = 0; j < columnsNumber; j++)
            {
                bool allAgreed = false;
                for (int i = 0; i < rowsNumber; i++)
                {
                    allAgreed = matrix[i, j];

                    if (!allAgreed)
                        break;
                }

                if (allAgreed)
                    interceptionIndex = j;
            }

            return interceptionIndex;
        }
    }
}
