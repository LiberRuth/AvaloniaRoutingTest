using System;

namespace AvaloniaRoutingTest.ViewModels;

public class SecondPageViewModel : PageViewModelBase
{
    public string MeText { get; } = "2번째 페이지";


    public override bool CanNavigateNext
    {
        get => true;
        protected set => throw new NotSupportedException();
    }

    // You cannot go back from this page
    public override bool CanNavigatePrevious
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
}