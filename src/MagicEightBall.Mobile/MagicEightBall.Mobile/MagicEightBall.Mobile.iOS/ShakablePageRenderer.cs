using MagicEightBall.Mobile;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ShakablePage), typeof(MagicEightBall.Mobile.iOS.ShakablePageRenderer))]

namespace MagicEightBall.Mobile.iOS
{
    public class ShakablePageRenderer: PageRenderer
    {
        public override bool CanBecomeFirstResponder
        {
            get
            {
                return true;
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            BecomeFirstResponder();
        }

        public override void MotionEnded(UIEventSubtype motion, UIEvent evt)
        {
            if (motion == UIEventSubtype.MotionShake)
            {
                ((ShakablePage)Element).OnShake();
            }
            base.MotionEnded(motion, evt);
        }
    }
}