namespace GamesDiscounts
{
    public interface IPageManager
    {
        void Configure(int NumberOfCardsPerPage, int NumberOfObjectsInCollection);
        int GetCurrentPage();
        int GetPages();
        void MoveLeft();
        void MoveRight();
    }
}