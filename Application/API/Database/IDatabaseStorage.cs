using System.Collections.Generic;
using System;

namespace Enceladus.Api
{
    public interface IDatabaseStorage
    {
        Tractor Get(int id);
        IAsyncResult BeginGet(AsyncCallback callback, object state, int tracorId);
        Tractor EndGet(IAsyncResult result);
        bool IsTractorInCache(int tractorIndex);

        IList<TractorSearchResult> Search(string criteria, int start, int count);
        IAsyncResult BeginSearch(AsyncCallback callback, object state, string criteria, int start, int count);
        IList<TractorSearchResult> EndSearch(IAsyncResult result);

        void CreateIndex(string indexName, string columnName);
        void DropIndex(string indexName);
    }
}
