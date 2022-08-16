using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesDiscounts
{
    public class PageManager : IPageManager
    {
        int _numberOfPages { get; set; }
        int _page { get; set; }
        int _perPage { get; set; }
        int _numberOfObjects { get; set; }
        bool _configured { get; set; } = false;

        public void Configure(int NumberOfCardsPerPage, int NumberOfObjectsInCollection)
        {
            _page = 1;
            _perPage = NumberOfCardsPerPage;
            _numberOfObjects = NumberOfObjectsInCollection;
            _numberOfPages = _numberOfObjects % _perPage == 0 ?
                _numberOfObjects / _perPage :
                (_numberOfObjects / _perPage) + 1;
            _configured = true;
        }

        public int GetPages()
        {
            return _numberOfPages;
        }

        public int GetCurrentPage()
        {
            return _page;
        }

        public int GetCardsPerPage()
        {
            return _perPage;
        }

        public void MoveLeft()
        {
            if (!_configured)
                throw new Exception("Manager not configured. Configure manager first");
            if (_page == 1)
                return;
            _page--;
        }

        public void MoveRight()
        {
            if (!_configured)
                throw new Exception("Manager not configured. Configure manager first");
            if (_page == _numberOfPages)
                return;
            _page++;
        }

        internal void FirstPage()
        {
            _page = 1;
        }
    }
}
