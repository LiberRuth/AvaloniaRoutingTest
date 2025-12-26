using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Media;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaRoutingTest.ViewModels
{
    public enum SlideDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    public class CustomPageSlide : AvaloniaObject, IPageTransition
    {
        // AvaloniaProperty로 등록
        public static readonly StyledProperty<TimeSpan> DurationProperty =
            AvaloniaProperty.Register<CustomPageSlide, TimeSpan>(nameof(Duration), TimeSpan.FromMilliseconds(500));

        public static readonly StyledProperty<SlideDirection> DirectionProperty =
            AvaloniaProperty.Register<CustomPageSlide, SlideDirection>(nameof(Direction), SlideDirection.Left);

        public static readonly StyledProperty<Easing> EasingProperty =
            AvaloniaProperty.Register<CustomPageSlide, Easing>(nameof(Easing), new CubicEaseOut());

        public TimeSpan Duration
        {
            get => GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public SlideDirection Direction
        {
            get => GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        public Easing Easing
        {
            get => GetValue(EasingProperty);
            set => SetValue(EasingProperty, value);
        }

        public async Task Start(Visual? from, Visual? to, bool forward, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return;

            var tasks = new List<Task>();

            double fromStart = 0;
            double fromEnd = 0;
            double toStart = 0;
            double toEnd = 0;

            if (from != null || to != null)
            {
                var width = (from?.Bounds.Width ?? to?.Bounds.Width) ?? 0;
                var height = (from?.Bounds.Height ?? to?.Bounds.Height) ?? 0;

                switch (Direction)
                {
                    case SlideDirection.Left:
                        fromStart = 0;
                        fromEnd = -width;
                        toStart = width;
                        toEnd = 0;
                        break;
                    case SlideDirection.Right:
                        fromStart = 0;
                        fromEnd = width;
                        toStart = -width;
                        toEnd = 0;
                        break;
                    case SlideDirection.Up:
                        fromStart = 0;
                        fromEnd = -height;
                        toStart = height;
                        toEnd = 0;
                        break;
                    case SlideDirection.Down:
                        fromStart = 0;
                        fromEnd = height;
                        toStart = -height;
                        toEnd = 0;
                        break;
                }
            }

            if (from != null)
            {
                tasks.Add(AnimateAsync(from, fromStart, fromEnd, cancellationToken));
            }

            if (to != null)
            {
                to.IsVisible = true;
                tasks.Add(AnimateAsync(to, toStart, toEnd, cancellationToken));
            }

            await Task.WhenAll(tasks);
        }

        private async Task AnimateAsync(Visual visual, double from, double to, CancellationToken cancellationToken)
        {
            var animation = new Animation
            {
                Duration = Duration,
                Easing = Easing,
                FillMode = FillMode.Forward
            };

            var isHorizontal = Direction == SlideDirection.Left || Direction == SlideDirection.Right;

            visual.RenderTransform = new TranslateTransform();

            animation.Children.Add(new KeyFrame
            {
                Cue = new Cue(0d),
                Setters =
                {
                    new Setter
                    {
                        Property = isHorizontal ? TranslateTransform.XProperty : TranslateTransform.YProperty,
                        Value = from
                    }
                }
            });

            animation.Children.Add(new KeyFrame
            {
                Cue = new Cue(1d),
                Setters =
                {
                    new Setter
                    {
                        Property = isHorizontal ? TranslateTransform.XProperty : TranslateTransform.YProperty,
                        Value = to
                    }
                }
            });

            await animation.RunAsync(visual, cancellationToken);
        }
    }
}
