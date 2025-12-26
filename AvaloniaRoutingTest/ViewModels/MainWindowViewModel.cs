using AvaloniaRoutingTest.Services;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace AvaloniaRoutingTest.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INavigationService
    {
        public MainWindowViewModel()
        {
            _CurrentPage = Pages[0];

            // 각 페이지에 NavigationService와 인덱스 주입
            for (int i = 0; i < Pages.Length; i++)
            {
                Pages[i].NavigationService = this;
                Pages[i].PageIndex = i;  // 인덱스 설정
            }

            IObservable<bool> canNavNext = this.WhenAnyValue(x => x.CurrentPage.CanNavigateNext);
            IObservable<bool> canNavPrev = this.WhenAnyValue(x => x.CurrentPage.CanNavigatePrevious);

            NavigateNextCommand = ReactiveCommand.Create(NavigateNext, canNavNext);
            NavigatePreviousCommand = ReactiveCommand.Create(NavigatePrevious, canNavPrev);
        }

        private readonly PageViewModelBase[] Pages =
        {
            new MainViewModel(),
            new SecondPageViewModel(),
            new ThirdPageViewModel()
        };

        private PageViewModelBase _CurrentPage;
        public PageViewModelBase CurrentPage
        {
            get { return _CurrentPage; }
            private set { this.RaiseAndSetIfChanged(ref _CurrentPage, value); }
        }

        private SlideDirection _currentDirection = SlideDirection.Left;
        public SlideDirection CurrentDirection
        {
            get => _currentDirection;
            private set => this.RaiseAndSetIfChanged(ref _currentDirection, value);
        }

        public ICommand NavigateNextCommand { get; }
        public void NavigateNext()
        {
            var index = CurrentPage.PageIndex + 1;
            if (index < Pages.Length)
            {
                CurrentDirection = SlideDirection.Left;
                CurrentPage = Pages[index];
                Debug.WriteLine($"다음 페이지로 이동: {CurrentPage.GetType().Name}");
            }
        }

        public ICommand NavigatePreviousCommand { get; }
        public void NavigatePrevious()
        {
            var index = CurrentPage.PageIndex - 1;
            if (index >= 0)
            {
                CurrentDirection = SlideDirection.Right;
                CurrentPage = Pages[index];
                Debug.WriteLine($"이전 페이지로 이동: {CurrentPage.GetType().Name}");
            }
        }

        public void NavigateTo(int pageIndex)
        {
            if (pageIndex >= 0 && pageIndex < Pages.Length)
            {
                // 현재 페이지보다 큰 인덱스면 Left, 작으면 Right
                CurrentDirection = pageIndex > CurrentPage.PageIndex
                    ? SlideDirection.Left
                    : SlideDirection.Right;

                CurrentPage = Pages[pageIndex];
                Debug.WriteLine($"페이지로 이동: {CurrentPage.GetType().Name}");
            }
        }
    }
}
