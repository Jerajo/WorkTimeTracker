using DryIoc;
using Prism.Common;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TimeTracker.Xamarin.Contracts;
using TimeTracker.Xamarin.Layout;
using Xamarin.Forms;

namespace TimeTracker.Xamarin.Services
{
    public class RegionNavigationService : IRegionNavigationService
    {
        #region Atributes

        private readonly IApplicationProvider _applicationProvider;
        private readonly global::Xamarin.Forms.Application _currentApplication;
        private readonly IContainer _containerProvider;

        #endregion

        public RegionNavigationService(IApplicationProvider applicationProvider,
            global::Xamarin.Forms.Application currentApplication,
            IContainer containerProvider)
        {
            _applicationProvider = applicationProvider;
            _currentApplication = currentApplication;
            _containerProvider = containerProvider;
        }

        #region Properties

        public static string CurrentRegionName { get; private set; } = "Tasks";
        public static Stack<string> NavigationHistory { get; } = new Stack<string>();
        public static Dictionary<string, View> PagesContainer { get; } = new Dictionary<string, View>();

        #endregion

        public NavigationResult NavigateBack(INavigationParameters parameters) =>
            PeekPreviousRegion(parameters).Result;

        public Task<NavigationResult> NavigateBackAsync(INavigationParameters parameters) =>
            PeekPreviousRegion(parameters);

        public NavigationResult NavigateTo(string path, INavigationParameters parameters) =>
            PushRegion(path, parameters).Result;

        public Task<NavigationResult> NavigateToAsync(string path, INavigationParameters parameters) =>
            PushRegion(path, parameters);

        private async Task<NavigationResult> PushRegion(string path, INavigationParameters parameters)
        {
            var result = new NavigationResult();
            try
            {
                View currentRegion = null;
                parameters = GetParams(path, parameters);

                if (PagesContainer.Count > 0)
                {
                    currentRegion = PagesContainer.Last().Value;
                    var canNavigate = await PageUtilities.CanNavigateAsync(currentRegion, parameters);
                    if (!canNavigate)
                    {
                        result.Exception = new Exception(NavigationException.IConfirmNavigationReturnedFalse);
                        return result;
                    }
                }

                View nextRegion = null;
                if (path.Contains('/'))
                {
                    foreach (var splitPath in path.Split(new[] { '/' },
                        StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (PagesContainer.Keys.Contains(splitPath))
                            continue;
                        nextRegion = _containerProvider.ResolveRegion(splitPath);
                        PagesContainer.Add(splitPath, nextRegion);
                    }
                }
                else if (PagesContainer.Keys.Contains(path))
                {
                    nextRegion = PagesContainer[path];
                }
                else
                {
                    nextRegion = _containerProvider.ResolveRegion(path);
                    PagesContainer.Add(path, nextRegion);
                }

                GetContentPresenter().Content = nextRegion;

                PageUtilities.OnNavigatedFrom(currentRegion, parameters);
                PageUtilities.OnNavigatedTo(nextRegion, parameters);

                CurrentRegionName = path;
                NavigationHistory.Push(CurrentRegionName);

                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                result.Exception = ex;
                return result;
            }
        }

        private async Task<NavigationResult> PeekPreviousRegion(INavigationParameters parameters)
        {
            var result = new NavigationResult();
            try
            {
                var currentRegion = PagesContainer[NavigationHistory.Pop()];

                var canNavigate = await PageUtilities.CanNavigateAsync(currentRegion, parameters);
                if (!canNavigate)
                {
                    result.Exception = new Exception(NavigationException.IConfirmNavigationReturnedFalse);
                    return result;
                }

                if (NavigationHistory.Count == 0)
                {
                    _currentApplication.Quit();
                    result.Success = true;
                    return result;
                }

                var previousRegion = PagesContainer[NavigationHistory.Peek()];
                GetContentPresenter().Content = previousRegion;

                PageUtilities.OnNavigatedFrom(currentRegion, parameters);
                PageUtilities.OnNavigatedTo(previousRegion, parameters);

                if (CurrentRegionName.Contains('/'))
                    PopRegion(currentRegion);
                CurrentRegionName = NavigationHistory.Peek();

                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                result.Exception = ex;
                return result;
            }
        }

        private ContentPresenter GetContentPresenter()
        {
            var mainPage = (MainLayout)_applicationProvider.MainPage;
            return mainPage.MainContent;
        }

        private void PopRegion(View currentRegion)
        {
            PagesContainer.Remove(CurrentRegionName);
            currentRegion.BindingContext = null;
            currentRegion = null;
            GC.Collect();
        }

        private INavigationParameters GetParams(string fullPath, INavigationParameters parameters)
        {
            var result = fullPath.Split('?');
            NavigationParameters queryParameters = null;
            if (result.Count() > 1)
            {
                queryParameters = new NavigationParameters(result[1]);
                queryParameters = (NavigationParameters)queryParameters.Concat(parameters);
            }

            return queryParameters;
        }
    }
}
