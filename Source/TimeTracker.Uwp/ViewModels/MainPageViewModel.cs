using TimeTracker.Application.Contracts;

namespace TimeTracker.Uwp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ICommandFactory _commandFactory;
        private readonly IQueryFactory _queryFactory;

        public MainPageViewModel(ICommandFactory commandFactory,
            IQueryFactory queryFactory)
        {
            _commandFactory = commandFactory;
            _queryFactory = queryFactory;
        }

        #region Properties

        public int CurrentTabNumber { get; set; }

        #endregion

        #region Commands



        #endregion

        #region Queries



        #endregion

        #region Page Events



        #endregion

        #region Auxiliary Methods



        #endregion
    }
}
