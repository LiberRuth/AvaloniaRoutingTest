using AvaloniaRoutingTest.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace AvaloniaRoutingTest.Services;

public interface INavigationService
{
    void NavigateNext();
    void NavigatePrevious();
    void NavigateTo(int pageIndex);  // 추가
}