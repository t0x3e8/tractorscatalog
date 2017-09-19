
namespace Enceladus
{
    class SearchCriteria
    {
        public string Criterias { get; set; }
        public SearchError Error { get; set; }

        public SearchCriteria(string criterias, SearchError error)
        {
            this.Criterias = criterias;
            this.Error = error;
        }
    }
}
