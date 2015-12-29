using System;
using System.Windows;
using System.Windows.Controls;

namespace LogoFX.Samples.Client.Presentation.Shell.Controls
{
    public class SmartPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            double height = availableSize.Height;
            if (double.IsNaN(height) || double.IsInfinity(height))
            {
                throw new ArgumentOutOfRangeException("availableSize");
            }

            double top = 0;
            double left = 0;
            double maxWidth = 0;

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(availableSize);
                Size desiredSize = child.DesiredSize;
                maxWidth = Math.Max(maxWidth, desiredSize.Width);
                top += desiredSize.Height;

                if (top + desiredSize.Height > height)
                {
                    top = 0;
                    left += maxWidth;
                    maxWidth = 0;
                }
            }

            return new Size(left + maxWidth, height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double top = 0;
            double left = 0;
            double maxWidth = 0;
            double height = finalSize.Height;

            foreach (UIElement child in InternalChildren)
            {
                Size desiredSize = child.DesiredSize;
                child.Arrange(new Rect(new Point(left, top), desiredSize));
                maxWidth = Math.Max(maxWidth, desiredSize.Width);
                top += desiredSize.Height;
                if (top + desiredSize.Height > height)
                {
                    top = 0;
                    left += maxWidth;
                    maxWidth = 0;
                }
            }

            return finalSize;
        }
    }
}
