using System;

namespace AvaloniaRoutingTest.ViewModels;

public class ThirdPageViewModel : PageViewModelBase
{
    public string Greeting { get; } = "3번째 페이지";

    public override bool CanNavigateNext
    {
        get => false;
        protected set => throw new NotSupportedException();
    }

    // You cannot go back from this page
    public override bool CanNavigatePrevious
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
}