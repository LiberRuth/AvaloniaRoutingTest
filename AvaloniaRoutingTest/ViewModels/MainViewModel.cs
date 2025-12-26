using ReactiveUI;
using System;
using System.Windows.Input;

namespace AvaloniaRoutingTest.ViewModels;

public class MainViewModel : PageViewModelBase
{
    public string Greeting { get; } = "Welcome to Avalonia!";

    public string MeText { get; } = "3 페이지로 가기";

    public ICommand GoToNextPageCommand { get; }

    public MainViewModel()
    {
        GoToNextPageCommand = ReactiveCommand.Create(GoToNextPage);
    }

    private void GoToNextPage()
    {
        NavigationService?.NavigateTo(2);

        //NavigationService?.NavigateTo(PageIndex + 1);
    }

    public override bool CanNavigateNext
    {
        get => true;
        protected set => throw new NotSupportedException();
    }

    public override bool CanNavigatePrevious
    {
        get => false;
        protected set => throw new NotSupportedException();
    }
}