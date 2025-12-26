using AvaloniaRoutingTest.Services;

namespace AvaloniaRoutingTest.ViewModels;

public abstract class PageViewModelBase : ViewModelBase
{
    public INavigationService? NavigationService { get; set; }
    public int PageIndex { get; set; }  // 추가

    public abstract bool CanNavigateNext { get; protected set; }
    public abstract bool CanNavigatePrevious { get; protected set; }
}